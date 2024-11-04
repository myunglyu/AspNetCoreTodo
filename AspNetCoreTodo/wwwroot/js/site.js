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
});

function markCompleted(checkbox) {

    var form = checkbox.closest('form');
    form.submit();
}

function markUndo(checkbox) {

    var form = checkbox.closest('form');
    form.submit();
}

function delItem(button) {

    var form = button.closest('form');
    form.submit();
}