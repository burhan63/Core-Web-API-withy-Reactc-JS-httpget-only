using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Dynamic;

namespace CRUD_App_with_React.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUsersController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AppUsersController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetAppUsers()
        {
            string connStr = _configuration.GetConnectionString("OrclDB");
            List<dynamic> appUsers = new();

            using (OracleConnection con = new OracleConnection(connStr))
            {
                con.Open();

                string Query = "SELECT * FROM APP_USERS";
                OracleCommand cmd = new OracleCommand(Query, con);
                OracleDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    dynamic row = new ExpandoObject();

                    for(int i = 0; i< dr.FieldCount; i++)
                    {
                        ((IDictionary<string, object>)row)[
                        dr.GetName(i)
                        ] = dr.IsDBNull(i) ? null : dr.GetValue(i);
                    }
                    appUsers.Add(row);

                }
            }            

            return Ok(appUsers);
        }
    }
}
