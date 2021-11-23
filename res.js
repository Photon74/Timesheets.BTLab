const absencesList = document.querySelector('.absences-list');

let key;

for (key in content) {
    absencesList.innerHTML += `
            <div class="card mt-4 col-md-6 bg-light bg-gradient border-primary">
                <div class="card-body">
                    <label for="reason">Причина отсутствия</label>
                    <p class="card-text" id="reason">${content[key].reason}</p>
                    <a href="#" type="button" class="btn btn-primary btn-sm">Изменить</a>
                    <a href="#" type="button" class="btn btn-danger btn-sm">Удалить</a>
                </div>
        </div>
    `
}