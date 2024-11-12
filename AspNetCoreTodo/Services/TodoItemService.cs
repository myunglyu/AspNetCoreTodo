using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AspNetCoreTodo.Data;
using AspNetCoreTodo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreTodo.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ApplicationDbContext _context;

        public TodoItemService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TodoItem[]> GetIncompleteItemsAsync(IdentityUser user)
        {
            var items = await _context.Items
                .Where(x => x.IsDone == false && x.UserId == user.Id)
                .ToArrayAsync();
            return items;
        }

        public async Task<TodoItem[]> GetCompleteItemsAsync(IdentityUser user)
        {
            var items = await _context.Items
                .Where(x => x.IsDone == true && x.UserId == user.Id)
                .ToArrayAsync();
            return items;
        }

        public async Task<TodoItem> GetEditItemAsync(Guid id, IdentityUser user)
        {
            var item = await _context.Items
                .Where(x => x.Id == id && x.UserId == user.Id)
                .FirstOrDefaultAsync();
            return item;
        }

        public async Task<bool> AddItemAsync(TodoItem newItem, IdentityUser user)
        {
            if (newItem == null)
            {
                throw new ArgumentNullException(nameof(newItem));
            }

            newItem.Id = Guid.NewGuid();
            newItem.IsDone = false;
            if ( newItem.DueAt == null) { newItem.DueAt = DateTimeOffset.Now.DateTime.AddDays(3); }
            newItem.UserId = user.Id;

            _context.Items.Add(newItem);

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<bool> EditItemAsync(TodoItem newItem, IdentityUser user)
        {
            var item = await _context.Items
                .Where(x => x.Id == newItem.Id && x.UserId == user.Id)
                .SingleOrDefaultAsync();

            if (item == null) return false;

            item.Title = newItem.Title;
            if (newItem.DueAt != null) {item.DueAt = newItem.DueAt;}

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<bool> MarkDoneAsync(Guid id, IdentityUser user)
        {
            var item = await _context.Items
                .Where(x => x.Id == id && x.UserId == user.Id)
                .SingleOrDefaultAsync();

            if (item == null) return false;

            item.IsDone = true;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }
        public async Task<bool> MarkUndoAsync(Guid id, IdentityUser user)
        {
            var item = await _context.Items
                .Where(x => x.Id == id && x.UserId == user.Id)
                .SingleOrDefaultAsync();

            if (item == null) return false;

            item.IsDone = false;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }
        public async Task<bool> DelItemAsync(Guid id, IdentityUser user)
        {
            var item = await _context.Items
                .Where(x => x.Id == id && x.UserId == user.Id)
                .SingleOrDefaultAsync();

            if (item == null) return false;

            _context.Items.Remove(item);

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }
    }
}