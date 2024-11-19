using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.VisualBasic;

namespace AspNetCoreTodo.Models;

public class TodoItem
{
    public Guid Id { get; set; }
    
    public bool IsDone { get; set; }

    public string? UserId { get; set; }

    [Required]
    public string Title { get; set; }

    public DateTimeOffset DueAt { get; set; }
}