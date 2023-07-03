function buildLokal(e) {
    return `
    <a class='result' href="lokal.html?id=${e.lokaleId}">
        <section class='result-headers' onclick>
            <h2>${e.nazwa}</h2>
            <h1>${e.miasto + " " + e.adres}</h1>
        </section>
        <table class='result-table'>
            <tbody>
            <tr>
                <td>Kuchnia:</td>
                <td class='result-table-value'>${e.kuchnia}</td>
            </tr>
            <tr>
                <td>Średnia opinia:</td>
                <td class='result-table-value'>6/10</td>
            </tr>
            <tr>
                <td>Średnia cena:<br></td>
                <td class='result-table-value'>25,40zł</td>
            </tr>
            </tbody>
        </table>
    </a>`;
}

function buildDanie(e) {
    return `
    <div class="result-dish">
        <table class="result-dish-table">
            <tbody>
                <tr class="dish-headers">
                    <td>${e.nazwa}</td>
                    <td class="dish-price">${e.cena}zł</td>
                </tr>
                <tr>
                    <td class="dish-desc" colspan="2">${e.opis}</td>
                </tr>
            </tbody>
        </table>
    </div>
    `
}

function buildOpinia(e) {
    return `
    <div class="result-opinion">
        <table class="result-opinion-table">
            <tbody>
                <tr>
                    <td class="opinion-user" colspan="2">${e.autor}</td>
                </tr>
                <tr>
                    <td class="opinion-rating">${e.ocena}/10</td>
                    <td class="opinion-date">${e.dataWystawienia.substr(0, 10)}</td>
                </tr>
                <tr>
                    <td class="opinion-desc" colspan="2">${e.opinia}</td>
                </tr>
            </tbody>
        </table>
    </div>
    `
}