// const absencesList = document.querySelector('.absences-list');

// const url = 'https://localhost:44348/api/Absence';

async function getResponse() {
    let response = await fetch('http://localhost:5000/api/Absence');
    let content = await response.text()
    console.log(content);

 
}

getResponse();