'use strict';

let id;
let key;
let reason;
let date;
let discounted;
let absence = {
    id: 0,
    reason: 1,
    startDate: '',
    duration: 1,
    description: '',
};
let deleteBtns = document.querySelectorAll('.delete-btn');
let editBtns = document.querySelectorAll('.edit-btn');
const showFormBtn = document.querySelector('.show-form-btn');
const absencesList = document.querySelector('.absences-list');
const AbsenceForm = document.querySelector('.add-absence-form');


showFormBtn.addEventListener('click', ShowForm);

const renderResponce = (absenceRecords) => {
    absencesList.innerHTML = '';
    for (key in absenceRecords) {
        if (absenceRecords[key].reason === 0) {
            reason = 'Отпуск';
        } else if (absenceRecords[key].reason === 1) {
            reason = 'Больничный';
        } else {
            reason = 'Прогул';
        }
        date = new Date(absenceRecords[key].startDate).toLocaleDateString("ru", { day: 'numeric', month: 'long', year: 'numeric' });
        discounted = absenceRecords[key].discounted === true ? 'да' : 'нет';
        absencesList.innerHTML += `
            <div class="card mt-2 mb-2 col-3 bg-light bg-gradient border-primary">
                <div class="card-body">
                    <h5 class="id card-title" id="${absenceRecords[key].id}">ID: ${absenceRecords[key].id}</h5>
                    <h6 class="reason card-subtitle mb-2 text-muted" id="${absenceRecords[key].reason}">Причина отсутствия: ${reason}</h6>
                    <h7 class="date card-text text-muted" id="${absenceRecords[key].startDate}">Дата начала: ${date}</h7><br>
                    <h7 class="duration card-text text-muted" id="${absenceRecords[key].duration}">Продолжительность (раб.дней): ${absenceRecords[key].duration}</h7><br>
                    <h7 class="discounted card-text text-muted" id="${absenceRecords[key].discounted}">Учтено при оплате: ${discounted}</h7><br>
                    <p class="description card-text" id="${absenceRecords[key].description}">Комментарий: ${absenceRecords[key].description}</p>
                    <a href="#" type="button" class="edit-btn btn btn-primary btn-sm">Изменить</a>
                    <a href="#" type="button" class="delete-btn btn btn-danger btn-sm">Удалить</a>
                </div>
            </div>
            `
    }
}

// сброс формы
function FormReset () {
    AbsenceForm.reset();
    AbsenceForm.elements["id"].value = 0;
}

// Получение всех записей - GET
async function GetAllAbsences() {
    let response = await fetch('/api/Absence', {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    if (response.ok === true) {
        let absenceRecords = await response.json();
        renderResponce(absenceRecords);
    }
    InitButtons();
}

// Получение одной записи - GET
//async function GetAbsenceById(id) {
//    let response = await fetch('/api/Absence/' + id, {
//        method: "GET",
//        headers: { "Accept": "application/json" }
//    });
//    if (response.ok === true) {
//        absence = await response.json();
//    }
//}

// Создание новой записи
// Метод - POST
async function CreateAbsence(absence) {
    const response = await fetch('/api/Absence', {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify(absence),
    });
    if (response.ok === true) {
        GetAllAbsences();
    }
}

// Показать форму
function ShowForm() {
    FormReset();
    AbsenceForm.classList.remove('hidden');
    showFormBtn.classList.add('hidden');
}

// Удалить запись
async function DeleteAbsence(event) {
    id = event.target.parentNode.children[0].id;
    const response = await fetch('/api/Absence/' + id, {
        method: "DELETE",
        headers: { "Accept": "application/json" }
    });
    if (response.ok === true) {
        GetAllAbsences();
    }
}

// Редактировать запись
async function EditAbsence(id, absence) {
    const response = await fetch('/api/Absence/' + id, {
        method: 'PUT',
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify(absence),
    });
    if (response.ok === true) {
        GetAllAbsences();
    }
}

// Заполнить форму
function FillForm(event) {
    ShowForm();
    AbsenceForm.elements['id'].value = event.target.parentNode.children[0].id;
    AbsenceForm.elements['reason'].value = event.target.parentNode.children[1].id;
    AbsenceForm.elements['start-date'].value = event.target.parentNode.children[2].id.split('T')[0];
    AbsenceForm.elements['duration'].value = event.target.parentNode.children[4].id;
    AbsenceForm.elements['discounted'].checked = event.target.parentNode.children[6].id;
    AbsenceForm.elements['description'].innerText = event.target.parentNode.children[8].id;
}

// Сохранить запись в переменную
function SaveAbsence() {
    absence.reason = +document.getElementById('reason').value;
    absence.startDate = document.getElementById('start-date').value;
    absence.duration = +document.getElementById('duration').value;
    absence.discounted = document.getElementById('discounted').checked;
    absence.description = document.getElementById('description').value;
}

//Инициализация кнопок
function InitButtons() {
    deleteBtns = document.querySelectorAll('.delete-btn');
    deleteBtns.forEach(button => button.addEventListener('click', DeleteAbsence));
    editBtns = document.querySelectorAll('.edit-btn');
    editBtns.forEach(button => button.addEventListener('click', FillForm));
}

AbsenceForm.addEventListener('submit', e => {
    e.preventDefault();
    const form = AbsenceForm;
    const id = form.elements['id'].value;
    if (id == 0) {
        SaveAbsence();
        CreateAbsence(absence);
        AbsenceForm.elements['description'].innerText = '';
        GetAllAbsences();
    } else {
        SaveAbsence();
        EditAbsence(id, absence);
        AbsenceForm.elements['description'].innerText = '';
        GetAllAbsences();
    }

    AbsenceForm.classList.toggle('hidden');
    showFormBtn.classList.remove('hidden');
})

AbsenceForm.addEventListener('reset', e => {
    AbsenceForm.elements['description'].innerText = '';
    AbsenceForm.classList.toggle('hidden');
    showFormBtn.classList.remove('hidden');
})

GetAllAbsences();
