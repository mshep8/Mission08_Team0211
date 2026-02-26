using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mission08_Team0211.Models;

public class ToDoTask
{
    [Key]
    public int ToDoTaskId { get; set; }

    [Required]
    [StringLength(256)]
    public string Task { get; set; } = string.Empty;

    public DateTime? DueDate { get; set; }

    [Required]
    [Range(1, 4)]
    public int Quadrant { get; set; }

    [Required]
    public int CategoryId { get; set; }

    [ForeignKey(nameof(CategoryId))]
    public Category? Category { get; set; }

    public bool Completed { get; set; }
}
