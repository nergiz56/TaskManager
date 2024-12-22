using FluentValidation;
using TaskEntity = TaskManager.Models.Task;

public class TaskValidator : AbstractValidator<TaskEntity>
{
    public TaskValidator()
    {
        RuleFor(task => task.Title)
            .NotEmpty().WithMessage("Başlık boş olamaz.")
            .Length(3, 100).WithMessage("Başlık 3 ile 100 karakter arasında olmalıdır.");

        RuleFor(task => task.Description)
            .NotEmpty().WithMessage("Açıklama boş olamaz.")
            .Length(10, 500).WithMessage("Açıklama 10 ile 500 karakter arasında olmalıdır.");

        RuleFor(task => task.Category)
            .NotEmpty().WithMessage("Kategori boş olamaz.");

        RuleFor(task => task.DueDate)
            .GreaterThanOrEqualTo(DateTime.Now).WithMessage("Son teslim tarihi geçmiş olamaz.");
    }
}