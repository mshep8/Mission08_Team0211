using System.ComponentModel.DataAnnotations;

namespace Mission08_Team0211.Models;

public class Category
{
    [Key]
    public int CategoryId { get; set; }

    [Required]
    [StringLength(256)]
    public string CategoryName { get; set; } = string.Empty;

    public List<ToDoTask> ToDoTasks { get; set; } = [];
}
