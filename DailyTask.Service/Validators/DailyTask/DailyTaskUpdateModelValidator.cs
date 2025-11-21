using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyTask.Service.Services.DailyTask.Models;
using FluentValidation;

namespace DailyTask.Service.Validators.DailyTask
{
    public class DailyTaskUpdateModelValidator : AbstractValidator<DailyTaskUpdateModel>
    {
        public DailyTaskUpdateModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");
            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");
        }
    }
}
