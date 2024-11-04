using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTodo.Models;
using AspNetCoreTodo.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreTodo.Controllers;

public class TodoController : Controller
{
    private readonly ITodoItemService _todoItemService;

    public TodoController(ITodoItemService todoItemService)
    {
        _todoItemService = todoItemService;
    }

    public async Task<IActionResult> Index()
    {
        var items = await _todoItemService.GetIncompleteItemsAsync();
        var completetItems = await _todoItemService.GetCompleteItemsAsync();

        var model = new TodoViewModel()
        {
            Items = items,
            CompleteItems = completetItems
        };

        return View(model);
    }

    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddItem(TodoItem newItem)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("Index");
        }

        var successful = await _todoItemService.AddItemAsync(newItem);
        if (!successful)
        {
            return BadRequest("Could not add item.");
        }

        return RedirectToAction("Index");
    }

    [ValidateAntiForgeryToken]
    public async Task<IActionResult> MarkDone(Guid id)
    {
        if (id == Guid.Empty)
        {
            return RedirectToAction("Index");
        }

        var successful = await _todoItemService.MarkDoneAsync(id);
        if (!successful)
        {
            return BadRequest("Could not mark item as done.");
        }

        return RedirectToAction("Index");
    }
    
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> MarkUndo(Guid id)
    {
        if (id == Guid.Empty)
        {
            return RedirectToAction("Index");
        }

        var successful = await _todoItemService.MarkUndoAsync(id);
        if (!successful)
        {
            return BadRequest("Could not mark item as undone.");
        }

        return RedirectToAction("Index");
    }

    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DelItem(Guid id)
    {
        if (id == Guid.Empty)
        {
            return RedirectToAction("Index");
        }

        var successful = await _todoItemService.DelItemAsync(id);
        if (!successful)
        {
            return BadRequest("Could not delete the item.");
        }

        return RedirectToAction("Index");
    }
}