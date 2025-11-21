using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyTask.Service.Services.DailyTask.Models;
using FluentValidation;

namespace DailyTask.Service.Validators.DailyTask
{
    public class DailyTaskCreateModelValidator : AbstractValidator<DailyTaskCreateModel>
    {
        public DailyTaskCreateModelValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThanOrEqualTo(0);
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");
            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");
            RuleFor(x => x.StartTime)
                .LessThan(x => x.EndTime).WithMessage("StartTime must be earlier than EndTime.");
            RuleFor(x => x.EndTime)
                .GreaterThan(x => x.StartTime).WithMessage("EndTime must be later than StartTime.");
        }
    }
}
