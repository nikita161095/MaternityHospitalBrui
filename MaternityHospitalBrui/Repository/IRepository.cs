using MaternityHospitalBrui.Filters;
using MaternityHospitalBrui.Repository;
using MaternityHospitalBrui.States;
using System;
using System.Threading.Tasks;

namespace MaternityHospitalBrui
{
  public interface IRepository<T, F>: IDisposable
    where T : class 
    where F : class {
    Task<PagedEnumerable<T>> GetAll(Paging paging = null, F filter = null);
    Task<T> Get(int id);
    Task<int> GetCount(F filter = null);
    Task<PagedEnumerable<TViewModel>> GetAll<TViewModel>(Paging paging = null, F filter = null)
      where TViewModel: class;
    Task<TViewModel> Get<TViewModel>(int id)
      where TViewModel: class;

    Task<T> Create(T item);
    Task<T> Update(int id, T item);
    Task<T> Delete(int id);
    T[] OutCreateId();
        //Task Join(IRepository<CompanyNameState, CompanyNameFilter> companyNames, Func<object, object> value1, Func<object, object> value2, Func<object, object, object> value3);
    }
}