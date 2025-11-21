using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyTask.DataAccess.Repositories;
using DailyTask.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace DailyTask.DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Domain.Entities.DailyTask> DailyTasks { get; }

        Task SaveAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
