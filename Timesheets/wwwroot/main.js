// const absencesList = document.querySelector('.absences-list');

// const url = 'https://localhost:44348/api/Absence';

async function getResponse() {
    let response = await fetch('/api/Absence', {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    let content = await response.text()
    console.log(content);


}

getResponse();