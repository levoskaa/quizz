using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;

namespace Quizz.Game.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GamesController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public GamesController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpPost]
        public void CreateGame()
        {
            string sql = "INSERT INTO Games (Name) Values (@Name);";
            var connstr = configuration.GetConnectionString("Default");
            using (var connection = new SqlConnection(connstr))
            {
                var affectedRows = connection.Execute(sql, new { Name = "Game 1" });

                Console.WriteLine(affectedRows);
            }
        }
    }
}