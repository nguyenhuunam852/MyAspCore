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

        public Tuple<int,List<TEntity>> GetByRawOrderPagiList<TEntity>(DbSet<TEntity> entities,
            StateModel stateModel, int perPage, string[]? queryList) 
             where TEntity : class
        {
            var tableName = _dBContext.GetTableName<TEntity>();

            var sqlScripts = @"select Count(*) from {0}";

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

            sqlScripts = string.Format(sqlScripts, tableName);

            var countEntities = _dBContext.Database.SqlQueryRaw<int>(sqlScripts).AsEnumerable().FirstOrDefault();

            sqlScripts = sqlScripts.Replace("Count(*)", "*");

            sqlScripts = string.Join(" ", sqlScripts, "ORDER BY {0}");
            sqlScripts = string.Join(" ", sqlScripts, (stateModel.IsDesc) ? "DESC" : "ASC");
            sqlScripts = string.Join(" ", sqlScripts, "offset {1} rows");
            sqlScripts = string.Join(" ", sqlScripts, "FETCH NEXT {2} rows only");

            string skipParam = (perPage * stateModel.Page).ToString();
            string takeParam = perPage.ToString();

            sqlScripts = string.Format(sqlScripts, stateModel.SortBy, skipParam, takeParam);

            int pages = ((countEntities) % perPage == 0) ? countEntities / perPage : countEntities / perPage + 1;

            return new Tuple<int, List<TEntity>> (pages, entities.FromSqlRaw(sqlScripts).ToList());
        }
    }
}
