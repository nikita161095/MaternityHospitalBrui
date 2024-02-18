using MaternityHospitalBrui.DB.Entities;
using MaternityHospitalBrui.Filters;
using MaternityHospitalBrui.States;
using System.Linq;

namespace MaternityHospitalBrui.DB.Repositories.Users
{
    internal class NameRepository : BaseRepository<NameState, NameFilter, Name>
    {
        public NameRepository(MaternityHospitalBruiContext dbContext) : base(dbContext)
        {

        }
        protected override IQueryable<Name> BuildFilter(IQueryable<Name> query, NameFilter filter)
        {
            if (filter == null)
            {
                return query;
            }

            //if (!string.IsNullOrEmpty(filter?.Company))
            //{
            //    query = query.Where(x => x.Company == filter.Company);
            //}

            //if (!string.IsNullOrEmpty(filter?.CompanyType) && !string.IsNullOrEmpty(filter?.Company))
            //{
            //    query = query.Where(x => x.CompanyType == filter.CompanyType && x.Company == filter.Company);
            //}

            return query;
        }

    }
}
