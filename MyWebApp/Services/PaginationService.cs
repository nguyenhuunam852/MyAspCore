using Microsoft.EntityFrameworkCore;
using MyWebApp.Interface;
using MyWebApp.Models;

#nullable disable

namespace MyWebApp.Services
{
    public class PaginationService : ICustomPagination
    {
        private DBContext _dBContext;

        public PaginationService(DBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public List<TEntity> GetByRawOrderPagiList<TEntity>(DbSet<TEntity> entities,
            StateModel stateModel, int perPage, string[]? queryList) 
             where TEntity : class
        {
            var tableName = _dBContext.GetTableName<TEntity>();

            var sqlScripts = @"select * from {0}";

            List<string> queriesLike = new List<string>() ;

            if(queryList!=null && !string.IsNullOrEmpty(stateModel.FilterParam))
            {
                foreach(string field in queryList)
                {
                    queriesLike.Add(string.Format("{0} Like N'%{1}%'", field, stateModel.FilterParam));
                }
                var queriesString = string.Join(" or ", queriesLike);

                sqlScripts = string.Join(" ", sqlScripts,"where", queriesString);
            }

            sqlScripts = string.Join(" ", sqlScripts, "ORDER BY {1}");
            sqlScripts = string.Join(" ", sqlScripts, (stateModel.IsDesc) ? "DESC" : "ASC");
            sqlScripts = string.Join(" ", sqlScripts, "offset {2} rows");
            sqlScripts = string.Join(" ", sqlScripts, "FETCH NEXT {3} rows only");

            string skipParam = (perPage * stateModel.Page).ToString();
            string takeParam = perPage.ToString();

            sqlScripts = string.Format(sqlScripts, tableName, stateModel.SortBy, skipParam, takeParam);

            return entities.FromSqlRaw(sqlScripts).ToList();
        }
    }
}
