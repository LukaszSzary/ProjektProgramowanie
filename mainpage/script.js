fetch('https://localhost:7280/api/lokale').then(resp => resp.json())
.then(data => {
    console.log()
const listContainer = document.getElementById('items_list');
for(let i =0; i <= data.length; i++){
    const div = document.createElement('div');
    div.classList.add('item');
    const nazwaSpan = document.createElement('span');
    nazwaSpan.textContent = ` ${data[i].nazwa} `
    const miastoSpan = document.createElement('span');
    miastoSpan.textContent = ` ${data[i].miasto}`;
    div.appendChild(nazwaSpan);
    div.appendChild(miastoSpan);
    listContainer.appendChild(div);
}
}) .catch(error => {
    console.log('Wystąpił błąd:', error);
  });

    
