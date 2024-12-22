using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskEntity = TaskManager.Models.Task; // Alias kullanımı

namespace TaskManager.Models
{
    [Table("Tasks")]
    public class Task
    {
           public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Category { get; set; }
            public DateTime DueDate { get; set; }
            public bool IsCompleted { get; set; }
            public int UserId { get; set; }


    }
}