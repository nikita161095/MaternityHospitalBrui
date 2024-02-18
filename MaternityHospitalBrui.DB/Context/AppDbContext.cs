using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MaternityHospitalBrui.DB.Repositories;
using MaternityHospitalBrui.DB.Repositories.Users;
using MaternityHospitalBrui.Filters;
using MaternityHospitalBrui.Repository;
using MaternityHospitalBrui.States;
using System;
using System.Threading.Tasks;

namespace MaternityHospitalBrui.DB.Context
{
    internal class AppDbContext : IAppDbContext, IDisposable
    {
        public IRepository<PatientState, PatientFilter> Patient { get; }
        public IRepository<NameState, NameFilter> Name { get; }

        protected virtual MaternityHospitalBruiContext DbContext { get; }
        protected virtual IDbContextTransaction Transaction { get; set; }

        public AppDbContext(MaternityHospitalBruiContext dbContext)
        {
            DbContext = dbContext;

            Patient = new PatientRepository(DbContext);
            Name = new NameRepository(dbContext);
        }

        public async Task<IAppDbContext> BeginTransaction()
        {
            if (Transaction == null)
            {
                Transaction = await DbContext.Database.BeginTransactionAsync();
            }
            return this;
        }

        public async Task RollBack()
        {
            await Transaction.RollbackAsync();
            Dispose();
        }

        public async virtual Task SaveChanges()
        {
            await DbContext?.SaveChangesAsync();
        }

        public async virtual Task CommitTransaction()
        {
            if (Transaction != null)
            {
                await Transaction.CommitAsync();
                Transaction.Dispose();
            }
            Transaction = null;
        }

        public async virtual Task SaveChangesAndCommitTransaction()
        {
            await SaveChanges();
            await CommitTransaction();
        }

        public void Dispose()
        {
            Transaction?.Dispose();
            DbContext?.Dispose();
        }
    }
}