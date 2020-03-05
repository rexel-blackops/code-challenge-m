// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


window.onload = taskTrace();

function taskTrace() {
    var path = window.location.pathname;
    switch (path) {
    case "/":
        $('#nav-home').addClass("task-selected");
        break;
    case "/Home/Task1":
        $('#nav-task1').addClass("task-selected");
        break;
    case "/Home/Task2":
        $('#nav-task2').addClass("task-selected");
        break;
    case "/Home/Task3":
        $('#nav-task3').addClass("task-selected");
        break;
    case "/Home/Task4":
        $('#nav-task4').addClass("task-selected");
        break;
    default:
    }
}