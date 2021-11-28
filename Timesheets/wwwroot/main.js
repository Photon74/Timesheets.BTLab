'use strict';

let key;
let reason;
let date;
let discounted;

const addAbsenceBtn = document.querySelector('.add-absence-btn');
const absencesList = document.querySelector('.absences-list');
const addAbsenceForm = document.querySelector('.add-absence-form');
const url = '/api/Absence';

const renderResponce = (absenceRecords) => {
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
            <div class="card mt-2 mb-2 col-4 bg-light bg-gradient border-primary">
                <div class="card-body">
                    <h5 class="card-title">ID: ${absenceRecords[key].id}</h5>
                    <h6 class="card-subtitle mb-2 text-muted" id="reason">Причина отсутствия: ${reason}</h6>
                    <h7 class="card-text text-muted">Дата начала: ${date}</h7><br>
                    <h7 class="card-text text-muted">Продолжительность (раб.дней): ${absenceRecords[key].duration}</h7><br>
                    <h7 class="card-text text-muted">Учтено при оплате: ${discounted}</h7><br>
                    <p class="card-text">Комментарий: ${absenceRecords[key].description}</p>
                    <a href="#" type="button" class="btn btn-primary btn-sm">Изменить</a>
                    <a href="#" type="button" class="btn btn-danger btn-sm">Удалить</a>
                </div>
            </div>
            `
    }
}

// сброс формы
let formReset = function () {
    const form = document.querySelector('.add-absence-form');
    form.reset();
    // form.elements["id"].value = 0;
}

// Получение всех записей
async function getResponse() {
    let response = await fetch(url, {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    if (response.ok === true) {
        let absenceRecords = await response.json();
        renderResponce(absenceRecords);
    }
}
getResponse();

// Создание новой записи
// Метод - POST
async function CreateAbsence() {
    const response = await fetch(url, {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            reason: 1,
            startDate: 2,
            duration: 3,
            discounted: 4,
            description: 5,
        })
    });
    if (response.ok === true) {

    }
}

addAbsenceBtn.addEventListener('click', AddAbsence);

function AddAbsence() {
    addAbsenceForm.classList.toggle('hidden');
}