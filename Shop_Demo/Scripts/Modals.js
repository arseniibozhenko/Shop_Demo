"use strict"

$(document).ready(() => {

    //Удаление элементов
    $(".delete_button").click((event) => {
        ////Отмена стандартного действия
        //event.preventDefault();
        //let this_button = $(event.target);
        ////Получение href
        //let href = this_button.attr("href");
        ////Получение id 
        //let id = this_button.parent().parent().children().eq(0).html();

        ////Добавление в модальное окно
        //$("#modal_window .modal-body").append(`${id} ?`);
        //$(".modal-footer .delete_modal_button").attr("href", href);

        //Модальное окно
        $("#delete_modal_window").modal("show");
    });

    //Добавление элементов
    $(".add_button").click((event) => {
        //Модальное окно
        $("#add_modal_window").modal("show");
    });

    //Редактирование элементов
    $(".edit_button").click((event) => {
        //Модальное окно
        $("#add_modal_window").modal("show");
    });

    //Добавление элементов
    $(".filter_button").click((event) => {
        //Модальное окно
        $("#filter_modal_window").modal("show");
    });
});