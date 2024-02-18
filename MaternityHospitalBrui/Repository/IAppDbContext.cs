using MaternityHospitalBrui.Filters;
using MaternityHospitalBrui.States;

namespace MaternityHospitalBrui.Repository
{
    public interface IAppDbContext
    {
        IRepository<PatientState, PatientFilter> Patient { get; }
        IRepository<NameState, NameFilter> Name { get; }

        Task<IAppDbContext> BeginTransaction();

        Task RollBack();
        Task SaveChanges();
        Task CommitTransaction();
        Task SaveChangesAndCommitTransaction();
    }
}