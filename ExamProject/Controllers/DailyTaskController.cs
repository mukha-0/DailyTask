using Microsoft.AspNetCore.Mvc;
using DailyTask.Service.Services.DailyTask;
using DailyTask.Service.Services.DailyTask.Models;
using DailyTask.Domain.Enums;
using DailyTask.DataAccess.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace ExamProject.Controllers
{
    public class DailyTaskController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DailyTaskController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var tasks = _unitOfWork.DailyTasks
                .SelectAllAsQueryable(d => !d.IsDeleted);

            var model = tasks.Select(d => new DailyTaskGetModel
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description,
                Status = d.Status,
                StartTime = d.StartTime,
                EndTime = d.EndTime
            }).ToList();

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var task = _unitOfWork.DailyTasks
                .SelectAllAsQueryable(d => d.Id == id && !d.IsDeleted).FirstOrDefaultAsync().Result;

            if (task == null) return NotFound();

            var model = new DailyTaskGetModel
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                Status = task.Status,
                StartTime = task.StartTime,
                EndTime = task.EndTime
            };

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DailyTaskUpdateModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var task = new DailyTask.Domain.Entities.DailyTask
            {
                Name = model.Name,
                Description = model.Description,
                Status = TaskStatuss.Pending,
                StartTime = DateTime.SpecifyKind(model.StartTime, DateTimeKind.Utc),
                EndTime = DateTime.SpecifyKind(model.EndTime, DateTimeKind.Utc),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                DeletedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            _unitOfWork.DailyTasks.Insert(task);
            await _unitOfWork.SaveAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            var task = _unitOfWork.DailyTasks
                .SelectAllAsQueryable(d => d.Id == id && !d.IsDeleted).FirstOrDefaultAsync().Result;

            if (task == null) return NotFound();

            ViewBag.TaskId = id;

            var model = new DailyTaskUpdateModel
            {
                Name = task.Name,
                Description = task.Description,
                StartTime = task.StartTime,
                EndTime = task.EndTime
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, DailyTaskUpdateModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var task = await _unitOfWork.DailyTasks.SelectAsync(d => d.Id == id && !d.IsDeleted);
            if (task == null) return NotFound();

            task.Name = model.Name;
            task.Description = model.Description;
            task.StartTime = DateTime.SpecifyKind(model.StartTime, DateTimeKind.Utc);
            task.EndTime = DateTime.SpecifyKind(model.EndTime, DateTimeKind.Utc);

            await _unitOfWork.SaveAsync();

            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> UpdateStatus(int id)
        {
            var task = await _unitOfWork.DailyTasks.SelectAsync(d => d.Id == id && !d.IsDeleted);
            if (task == null)
                return NotFound();

            var model = new DailyTaskChangeStatusModel
            {
                Id = task.Id,
                Status = task.Status
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(DailyTaskChangeStatusModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var task = await _unitOfWork.DailyTasks.SelectAsync(d => d.Id == model.Id && !d.IsDeleted);
            if (task == null)
                return NotFound();

            task.Status = model.Status;
            task.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.SaveAsync();
            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _unitOfWork.DailyTasks.SelectAsync(d => d.Id == id && !d.IsDeleted);
            if (task == null) return NotFound();

            var model = new DailyTaskGetModel
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                Status = task.Status,
                StartTime = task.StartTime,
                EndTime = task.EndTime
            };

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _unitOfWork.DailyTasks.SelectAsync(d => d.Id == id && !d.IsDeleted);
            if (task == null) return NotFound();

            task.IsDeleted = true;
            task.DeletedAt = DateTime.UtcNow;
            _unitOfWork.DailyTasks.Update(task);
            await _unitOfWork.SaveAsync();

            return RedirectToAction(nameof(Index));
        }

    }

}

