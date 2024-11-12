using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTodo.Models;
using AspNetCoreTodo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreTodo.Controllers;

[Authorize]
public class TodoController : Controller
{
    private readonly ITodoItemService _todoItemService;
    private readonly UserManager<IdentityUser> _userManager;

    public TodoController(ITodoItemService todoItemService, UserManager<IdentityUser> userManager)
    {
        _todoItemService = todoItemService;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index(string sortOrder)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null) return Challenge();

        ViewData["DateSortParm"] = String.IsNullOrEmpty(sortOrder) ? "due_desc" : "";
        ViewData["NameSortParm"] = sortOrder == "title" ? "title_desc" : "title";
        
        var items = from i in await _todoItemService.GetIncompleteItemsAsync(user)
            select i;
            switch (sortOrder)
            {
                case "title":
                    items = items.OrderBy(i => i.Title);
                    break;
                case "title_desc":
                    items = items.OrderByDescending(i => i.Title);
                    break;
                case "due_desc":
                    items = items.OrderByDescending(i => i.DueAt);
                    break;
                default:
                    items = items.OrderBy(i => i.DueAt);
                    break;
            }
        items = items.ToArray();

        var completetItems = await _todoItemService.GetCompleteItemsAsync(user);

        var model = new TodoViewModel()
        {
            Items = (TodoItem[])items,
            CompleteItems = completetItems
        };

        return View(model);
    }

    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditItemPartial(Guid id)
    {
        var user = await _userManager.GetUserAsync(User);
        
        if (user == null) return Challenge();

        var item = await _todoItemService.GetEditItemAsync(id, user);
        if (item == null)
        {
            return NotFound();
        }
        return PartialView("EditItemPartial", item);
    }

    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddItem(TodoItem newItem)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null) return Challenge();

        if (!ModelState.IsValid)
        {
            return RedirectToAction("Index");
        }

        var successful = await _todoItemService.AddItemAsync(newItem, user);
        if (!successful)
        {
            return BadRequest("Could not add item.");
        }

        return RedirectToAction("Index");
    }

    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditItem(TodoItem newItem)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null) return Challenge();

        if (newItem.Id == Guid.Empty)
        {
            return RedirectToAction("Index");
        }
        await _todoItemService.EditItemAsync(newItem, user);

        // var successful = await _todoItemService.EditItemAsync(newItem, user);
        // if (!successful)
        // {
        //     return RedirectToAction("Index");
        // }

        return RedirectToAction("Index");
    }

    [ValidateAntiForgeryToken]
    public async Task<IActionResult> MarkDone(Guid id)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null) return Challenge();

        if (id == Guid.Empty)
        {
            return RedirectToAction("Index");
        }

        var successful = await _todoItemService.MarkDoneAsync(id, user);
        if (!successful)
        {
            return BadRequest("Could not mark item as done.");
        }

        return RedirectToAction("Index");
    }

    [ValidateAntiForgeryToken]
    public async Task<IActionResult> MarkUndo(Guid id)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null) return Challenge();
        
        if (id == Guid.Empty)
        {
            return RedirectToAction("Index");
        }

        var successful = await _todoItemService.MarkUndoAsync(id, user);
        if (!successful)
        {
            return BadRequest("Could not mark item as undone.");
        }

        return RedirectToAction("Index");
    }

    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DelItem(Guid id)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null) return Challenge();

        if (id == Guid.Empty)
        {
            return RedirectToAction("Index");
        }

        var successful = await _todoItemService.DelItemAsync(id, user);
        if (!successful)
        {
            return BadRequest("Could not delete the item.");
        }

        return RedirectToAction("Index");
    }
}