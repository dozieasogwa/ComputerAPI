using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace HelloWorld.Data
{
    public class DataContextDapper
    {

       // private IConfiguration _config;
         private string _connectionString;
          public  DataContextDapper (IConfiguration config)
          {
            //_config = config;
            _connectionString = config.GetConnectionString("DefaultConection");
        }

           public IEnumerable<T> LoadData<T> (string sql)
           {
            IDbConnection dbconnectiion = new SqlConnection(_connectionString);
             return dbconnectiion.Query<T>(sql);
           }

            public T LoadDataSingle<T> (string sql)
           {
            IDbConnection dbconnectiion = new SqlConnection(_connectionString);
             return dbconnectiion.QuerySingle<T>(sql);
           }

             public bool ExecuteSql (string sql)
           {
            IDbConnection dbconnectiion = new SqlConnection(_connectionString);
             return (dbconnectiion.Execute(sql) > 0);
           }

             public int ExecuteSqlWithRowCount (string sql)
           {
            IDbConnection dbconnectiion = new SqlConnection(_connectionString);
             return dbconnectiion.Execute(sql);
           }  

    }
}
