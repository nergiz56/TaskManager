using System.Security.Claims;
using TaskManager.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskEntity = TaskManager.Models.Task; // Task için alias tanımlandı
using System.ComponentModel.DataAnnotations; // RequiredAttribute için
using System.Linq; // LINQ işlemleri için

namespace TaskManager.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TasksController(ApplicationDbContext context)
        {
            _context = context;
        }




        [HttpPost]
        public IActionResult AddTask([FromBody] TaskEntity task)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized(new { Message = "Kullanıcı doğrulaması başarısız!" });
            }

            task.UserId = int.Parse(userIdClaim);

            _context.Tasks.Add(task);
            _context.SaveChanges();

            return Ok(new { Message = "Görev başarıyla eklendi!", Task = task });
        }


       [HttpGet("category/{category}")]
       public IActionResult GetTasksByCategory(string category)
       {
           // Kullanıcının kimliğini al
           var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

           if (string.IsNullOrEmpty(userIdClaim))
           {
               return Unauthorized(new { Message = "Kullanıcı doğrulaması başarısız!" });
           }

           var userId = int.Parse(userIdClaim);

           // Kullanıcının kategorideki görevlerini filtrele
           var filteredTasks = _context.Tasks
               .Where(t => t.UserId == userId && t.Category != null && t.Category.ToLower() == category.ToLower())
               .ToList();

           if (!filteredTasks.Any())
           {
               return NotFound(new { Message = $"'{category}' kategorisinde görev bulunamadı." });
           }

           return Ok(filteredTasks);
       }

        [Authorize]
        [HttpGet("my-tasks")]
        public IActionResult GetMyTasks()
        {
            // Kullanıcının kimliğini al
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized(new { Message = "Kullanıcı doğrulaması başarısız!" });
            }

            var userId = int.Parse(userIdClaim);
            var tasks = _context.Tasks.Where(t => t.UserId == userId).ToList();

            return Ok(tasks);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id);

            if (task == null)
            {
                return NotFound(new { Message = "Görev bulunamadı!" });
            }

            // Kullanıcı doğrulaması
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null || task.UserId != int.Parse(userIdClaim))
            {
                return Unauthorized(new { Message = "Bu görevi silme yetkiniz yok!" });
            }

            _context.Tasks.Remove(task);
            _context.SaveChanges();

            return Ok(new { Message = "Görev başarıyla silindi!" });
        }
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, [FromBody] TaskEntity updatedTask)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id);

            if (task == null)
            {
                return NotFound(new { Message = "Görev bulunamadı!" });
            }

            // Kullanıcı doğrulaması
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null || task.UserId != int.Parse(userIdClaim))
            {
                return Unauthorized(new { Message = "Bu görevi güncelleme yetkiniz yok!" });
            }

            // Mevcut görevi güncelle
            task.Title = updatedTask.Title;
            task.Description = updatedTask.Description;
            task.Category = updatedTask.Category;
            task.DueDate = updatedTask.DueDate;
            task.IsCompleted = updatedTask.IsCompleted;

            _context.Tasks.Update(task);
            _context.SaveChanges();

            return Ok(new { Message = "Görev başarıyla güncellendi!", Task = task });
        }
    }
}