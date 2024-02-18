using MaternityHospitalBrui.DB.Entities;
using MaternityHospitalBrui.Filters;
using MaternityHospitalBrui.States;
using System.Linq;

namespace MaternityHospitalBrui.DB.Repositories.Users
{
    internal class PatientRepository : BaseRepository<PatientState, PatientFilter, Patient>
    {
        public PatientRepository(MaternityHospitalBruiContext dbContext) : base(dbContext)
        {

        }
        protected override IQueryable<Patient> BuildFilter(IQueryable<Patient> query, PatientFilter filter)
        {
            if (filter == null)
            {
                return query;
            }

            if (filter.Param == "eq")
            {
                query = query.Where(x => x.BirthDate.Date == filter.BirthDate.Date);
            }

            if (filter.Param == "ne")
            {
                query = query.Where(x => x.BirthDate.Date != filter.BirthDate.Date);
            }

            return query;
        }

    }
}
