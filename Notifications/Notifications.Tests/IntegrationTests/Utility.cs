
using System;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;

namespace Notifications.Tests.IntegrationTests
{
    public static class Utility
    {
        public static void CreateSeededTestDatabase()
        {
            //var connectionString = ConfigurationManager.ConnectionStrings["MasterDbConnection"].ConnectionString;

            //var path = Environment.CurrentDirectory.Replace("bin\\Debug", "Sql\\instnwnd.sql");
            //var file = new FileInfo(path);
            //var script = file.OpenText().ReadToEnd();

            //using (var connection = new SqlConnection(connectionString))
            //{
            //    var server = new Server(new ServerConnection(connection));
            //    server.ConnectionContext.ExecuteNonQuery(script);
            //}
        }
    }
}