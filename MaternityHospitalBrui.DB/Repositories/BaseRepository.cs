using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MaternityHospitalBrui.DB.Common;
using MaternityHospitalBrui.DB.Common.Mappings;
using MaternityHospitalBrui.Exceptions;
using MaternityHospitalBrui.Repository;

namespace MaternityHospitalBrui.DB.Repositories
{
    internal abstract class BaseRepository<T, F, TEntity> : IRepository<T, F>
        where T : class
        where F : class
        where TEntity : class, IEntity
    {
        protected virtual IMapper Mapper { get; } = ConfiguratorMapping.GetMapperConfiguration().CreateMapper();
        protected virtual MaternityHospitalBruiContext DbContext { get; }
        protected virtual IDbContextTransaction Transaction { get; set; }

        protected BaseRepository(MaternityHospitalBruiContext dbContext)
        {
            DbContext = dbContext;
        }

        public async virtual Task<T> Create(T item)
        {
            var entityItem = Mapper.Map<TEntity>(item);
            var res = await DbContext.Set<TEntity>().AddAsync(entityItem);
            return Mapper.Map<T>(res.Entity);
        }

        public async virtual Task<T> Delete(int id)
        {
            var res = await DbContext.Set<TEntity>().FindAsync(id);
            if (res == null)
            {
                throw new NotFoundException(typeof(T).Name, id);
            }
            var removed = DbContext.Set<TEntity>().Remove(res);
            return Mapper.Map<T>(removed.Entity);
        }

        public async virtual Task<T> Get(int id)
        {
            var res = await Get<T>(id);
            return res;
        }

        public async virtual Task<PagedEnumerable<T>> GetAll(Paging paging = null, F filter = null)
        {
            var res = await GetAll<T>(paging, filter);
            return res;
        }

        public async virtual Task<TViewModel> Get<TViewModel>(int id)
        where TViewModel : class
        {
            var query = DbContext.Set<TEntity>().Where(x => x.Id == id);
            var resultList = await query.ProjectTo<TViewModel>(Mapper.ConfigurationProvider).ToListAsync();
            var res = resultList.FirstOrDefault();
            if (res == null)
            {
                throw new NotFoundException(typeof(T).Name, id);
            }

            return res;
        }

        public async virtual Task<PagedEnumerable<TViewModel>> GetAll<TViewModel>(Paging paging = null, F filter = null)
        where TViewModel : class
        {
            var pageCount = int.MaxValue;
            var totalCount = int.MaxValue;

            var query = DbContext.Set<TEntity>().AsQueryable();
            query = BuildFilter(query, filter);
            if (paging != null)
            {
                totalCount = await query.CountAsync();
                var actualPageCount = paging.PageSize > 0 ? Math.Ceiling(totalCount / (decimal)paging.PageSize) : 0;
                pageCount = (int)Math.Max(actualPageCount, 1);
                query = query.Skip((paging.PageNo - 1) * paging.PageSize);
                query = query.Take(paging.PageSize);
                if (!IsOrdered(query))
                {
                    query = query.OrderBy(x => x.Id);
                }
            }

            var mappedList = await query.ProjectTo<TViewModel>(Mapper.ConfigurationProvider).ToListAsync();
            return new PagedEnumerable<TViewModel>(mappedList, totalCount, pageCount);
        }

        public async virtual Task<int> GetCount(F filter = null)
        {
            var query = DbContext.Set<TEntity>().AsQueryable();
            query = BuildFilter(query, filter);

            var count = await query.CountAsync();
            return count;
        }

        public async virtual Task<T> Update(int id, T item)
        {
            var res = await DbContext.Set<TEntity>().FindAsync(id);
            if (res == null)
            {
                throw new NotFoundException(typeof(T).Name, id);
            }
            var entityItem = Mapper.Map<T, TEntity>(item, res);
            var resUpd = DbContext.Set<TEntity>().Attach(entityItem);
            return Mapper.Map<TEntity, T>(resUpd.Entity);
        }

        public virtual T[] OutCreateId()
        {
            var entities = DbContext.ChangeTracker.Entries<TEntity>();
            return Mapper.Map<T[]>(entities.Select(x => x.Entity));
        }

        public void Dispose()
        {
            DbContext?.Dispose();
        }

        private bool IsOrdered(IQueryable<TEntity> queryable)
        {
            return queryable is IOrderedQueryable<TEntity>;
        }

        protected virtual IQueryable<TEntity> BuildFilter(IQueryable<TEntity> query, F filter)
        {
            return query;
        }
    }
}