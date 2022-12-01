using Microsoft.EntityFrameworkCore;
using MyWebApp.Models;

namespace MyWebApp.Interface
{
    public interface ICustomPagination
    {
        List<TEntity> GetByRawOrderPagiList<TEntity>(DbSet<TEntity> entities,
             StateModel stateModel,int perPage, string[] queryList)
                where TEntity : class;
    }
}
