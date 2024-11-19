// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function() {

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
        showEditForm(e.target);
    });
    $('.save-button').on('click', function(e) {
        saveEditForm(e.target);
    });
    $('.cancel-button').on('click', function(e) {
        hideEditForm(e.target);
    });
});

window.onload = function() {

    var todoItems = document.getElementsByClassName('todo-item');
    var checkboxes = document.getElementsByClassName('done-checkbox');
    var editrows = document.getElementsByClassName('edititem-row');
    var editforms = document.getElementsByClassName('edititem-form');
    var buttons = document.getElementsByClassName('edit-button');
    var buttons2 = document.getElementsByClassName('cancel-button');
    var buttons3 = document.getElementsByClassName('save-button');
    for (var i = 0; i < todoItems.length; i++) {
        todoItems[i].setAttribute('id', `todoitem${i}`);

        if (checkboxes[i]){
            checkboxes[i].setAttribute('data-form-id', `todoitem${i}`)
        }
        if (editrows[i]){
            // editrows[i].style.display='none'; // TagHelper is used to hide the form
            editrows[i].id=`editrow${i}`
        }
        if (editforms[i]){
            editforms[i].id=`editform${i}`
        }
        if (buttons[i]){
            buttons[i].setAttribute('data-row-id', `editrow${i}`);
        }
        if (buttons2[i]){
            buttons2[i].setAttribute('data-row-id', `editrow${i}`);
        }
        if (buttons3[i]){
            buttons3[i].setAttribute('data-form-id', `editform${i}`);
            buttons3[i].setAttribute('data-row-id', `editrow${i}`);
        }
    }

}

function showEditForm(button) {
    var rowId = button.getAttribute('data-row-id');
    var row = document.getElementById(rowId);
    
    if (row.style.display == 'none') {
        row.style.display = '';
        button.style.display = 'none';
    }
}

function hideEditForm(button) {
    var rowId = button.getAttribute('data-row-id');
    var row = document.getElementById(rowId);
    var showButton = document.querySelector(`[data-row-id='${rowId}']`);
    
    if (row.style.display == '') {
        row.style.display = 'none';
        showButton.style.display = '';
    }
}

function saveEditForm(button) {

    var rowId = button.getAttribute('data-row-id');
    var row = document.getElementById(rowId);
    var showButton = document.querySelector(`[data-row-id='${rowId}']`);

    if (row.style.display == '') {
        row.style.display = 'none';
        showButton.style.display = '';
    }

    var formId = button.getAttribute('data-form-id');
    var form = document.getElementById(formId);
    form.submit();
}

function markCompleted(checkbox) {
    var row = checkbox.closest('tr');
    row.classList.add('done');
    var formId = checkbox.getAttribute('data-form-id');
    var form = document.getElementById(formId);
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