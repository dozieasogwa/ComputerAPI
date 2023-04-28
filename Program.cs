using System;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using Dapper;
using HelloWorld.Data;
using HelloWorld.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.Sql;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HelloWorld
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

            DataContextDapper dapper = new DataContextDapper(config);


            // Console.WriteLine(rightNow.ToString());

            // Computer myComputer = new Computer() 
            // {
            //     Motherboard = "Z690",
            //     HasWifi = true,
            //     HasLTE = false,
            //     ReleaseDate = DateTime.Now,
            //     Price = 943.87m,
            //     VideoCard = "RTX 2060"
            // };

            //     string sql = @"INSERT INTO TutorialAppSchema.Computer (
            //         Motherboard,
            //         HasWifi,
            //         HasLTE,
            //         ReleaseDate,
            //         Price,
            //         VideoCard
            //     ) VALUES ('" + myComputer.Motherboard 
            //             + "','" + myComputer.HasWifi
            //             + "','" + myComputer.HasLTE
            //             + "','" + myComputer.ReleaseDate.ToString("yyyy-MM-dd")
            //             + "','" + myComputer.Price.ToString("0.00", CultureInfo.InvariantCulture)
            //             + "','" + myComputer.VideoCard
            //     + "')\n";

            //    //File.WriteAllText("log.txt",  "\n" + sql +"\n");

            //     using StreamWriter openFile = new("log.txt", append: true);
            //     openFile.WriteLine("\n" + sql +"\n");

            //     openFile.Close();

            string computerJson = File.ReadAllText("Computers.json");

            //Console.WriteLine(computerJson);

            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            IEnumerable<Computer>? computersNewtonSoft = JsonConvert.DeserializeObject<IEnumerable<Computer>>(computerJson);

            IEnumerable<Computer>? computersSystem = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Computer>>(computerJson, options);

            if (computersNewtonSoft != null)

            foreach (Computer computer in computersNewtonSoft)
            {

             string sql = @"INSERT INTO TutorialAppSchema.Computer (
                    Motherboard,
                    HasWifi,
                    HasLTE,
                    ReleaseDate,
                    Price,
                    VideoCard
                ) VALUES ('" + EscapeSingleQuote(computer.Motherboard) 
                        + "','" + computer.HasWifi
                        + "','" + computer.HasLTE
                        + "','" + computer.ReleaseDate//.ToString("yyyy-MM-dd")
                        + "','" + computer.Price//.ToString("0.00", CultureInfo.InvariantCulture)
                        + "','" + EscapeSingleQuote(computer.VideoCard)
                + "')";

                    dapper.ExecuteSql(sql);
                }

            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }; 

            string computersCopyNewtonsoft = JsonConvert.SerializeObject(computersNewtonSoft, settings);

             File.WriteAllText("computersCopyNewtonsoft.txt",  computersCopyNewtonsoft);


              string computersCopySystem = System.Text.Json.JsonSerializer.Serialize(computersSystem, options);

             File.WriteAllText("computersCopySystem.txt",  computersCopySystem);
        }

            static string EscapeSingleQuote(string input)
            {
            string Output = input.Replace("'", "''");

            return Output;
            }
    }
}