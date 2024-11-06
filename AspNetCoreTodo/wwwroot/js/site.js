// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function() {

    // Wire up all of the checkboxes to run markCompleted()
    $('.done-checkbox').on('click', function(e) {
        markCompleted(e.target);
    });
    $('.undo-checkbox').on('click', function(e) {
        markUndo(e.target);
    });
    $('.del-item').on('click', function(e) {
        delItem(e.target);
    });
    $('.edit-button').on('click', function(e) {
        editItem(e.target);
    });
});

var due = document.getElementById('due-input');
due.value = new Date;

window.onload = function() {
    var editforms = document.getElementsByClassName('edititem-form');
    for (var i = 0; i < editforms.length; i++) {
        editforms[i].style.display='none';
    }
}

function markCompleted(checkbox) {
    var row = checkbox.closest('tr');
    row.classList.add('done');
    var form = checkbox.closest('form.todo-item');
    form.submit();
}

function markUndo(checkbox) {
    var row = checkbox.closest('tr');
    row.classList.add('todo');
    var form = checkbox.closest('form.done-item');
    form.submit();
}

function delItem(button) {
    var form = button.closest('form');
    form.submit();
}