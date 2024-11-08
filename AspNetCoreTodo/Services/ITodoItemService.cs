using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCoreTodo.Models;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreTodo.Services
{
    public interface ITodoItemService
    {
        Task<TodoItem[]> GetIncompleteItemsAsync(IdentityUser user);
        Task<TodoItem[]> GetCompleteItemsAsync(IdentityUser user);
        Task<TodoItem> GetEditItemAsync(Guid id, IdentityUser user);
        Task<bool> AddItemAsync(TodoItem newItem, IdentityUser user);
        Task<bool> EditItemAsync(TodoItem newItem, IdentityUser user);
        Task<bool> MarkDoneAsync(Guid id, IdentityUser user);
        Task<bool> MarkUndoAsync(Guid id, IdentityUser user);       
        Task<bool> DelItemAsync(Guid id, IdentityUser user);
    }
}