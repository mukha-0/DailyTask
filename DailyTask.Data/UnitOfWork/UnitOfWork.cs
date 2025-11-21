using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DailyTask.DataAccess.Contexts;
using DailyTask.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace DailyTask.DataAccess.UnitOfWork
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork
    {
        public IRepository<Domain.Entities.DailyTask> DailyTasks => new Repository<Domain.Entities.DailyTask>(context);

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await context.Database.BeginTransactionAsync();
        }

        public void Dispose()
        {
            context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
