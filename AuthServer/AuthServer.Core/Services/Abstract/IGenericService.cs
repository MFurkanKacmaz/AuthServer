using SharedLibrary.DTOs;
using System.Linq.Expressions;

namespace AuthServer.Core.Services.Abstract
{
    public interface IGenericService<T,TDto> where T : class where TDto : class
    {
        Task<Response<TDto>> GetByIdAsync(int id);
        Task<Response<IEnumerable<TDto>>> GetAllAsync();
        Task<Response<IEnumerable<TDto>>> Where(Expression<Func<T, bool>> predicate);
        Task <Response<TDto>> AddAsync(TDto entity);
        Task<Response<NoDataDto>> Delete(int id);
        Task<Response<NoDataDto>> UpdateAsync(TDto entity,int id);
    }
}
