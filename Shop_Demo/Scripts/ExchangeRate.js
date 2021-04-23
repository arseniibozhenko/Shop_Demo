"use strict"

$(document).ready(() => {

    $.getJSON("https://api.privatbank.ua/p24api/pubinfo?json&exchange&coursid=5", function (data) {
        let rows = "";
        for (let item of data) {
            rows += `<tr>
                        <td>${item.ccy}</td>
                        <td>${Number(item.buy)}</td>
                        <td>${Number(item.sale)}</td>
                      </tr>`;
        }

        $(".currencyExchangeRate table tbody").html(rows);
    });
});