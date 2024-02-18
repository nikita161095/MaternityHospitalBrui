using System.Collections.Generic;

namespace MaternityHospitalBrui.Repository
{
    public interface IPagedEnumerable<out T> : IEnumerable<T>
    {
        int TotalCount { get; }
        int PageCount { get; }
    }
}
