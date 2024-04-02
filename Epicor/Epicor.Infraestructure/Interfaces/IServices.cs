

using Epicor.Infraestructure.Helpers;

namespace Epicor.Infraestructure.Interfaces
{
    public  interface IServices<T> where T : class
    {
        Task<List<T>> GetTotalsAsync(FiltersParams filters = null);
        Task<List<T>> GetTotalsByResponsableAsync(FiltersParams filters = null);
        Task<List<T>> GetTotalsByRangeDayseAsync(FiltersParams filters = null);
        Task<List<T>> GetTotalsByStatuseAsync(FiltersParams filters = null);
    }
}
