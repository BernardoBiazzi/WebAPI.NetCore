using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace WebAPI.Controllers
{
    //aqui definimos a rota que caso acessemos pelo httpget extrairemos/inseriremos as informações
    [Route("api/[controller]")]
    //quando a rota está como [controller] ela vai entender qual é de acordo com o nome do controller, nesse caso seria a mesma coisa que "api/department"
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //Função que possobilita extrairmos as informações da tabela que quisermos.
        [HttpGet]
        public JsonResult Get()
        {
            //Daqui pra baixo estamos criando a conexão com a tabela do banco de dados que queremos extrair as informações
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon"); //EmployeeAppCon está definido no appsettings.json
            SqlConnection myCon = new SqlConnection(sqlDataSource);
            //aqui determinamos quais elementos da tabela queremos extrair:
            string query = @"select EmployeeID, EmployeeName, Department, DateOfJoining, PhotoFileName from dbo.Employee";
            //------------------------------------------------------------------------------------------------------------
            SqlCommand myCommand = new SqlCommand(query, myCon);

            SqlDataReader myReader;
            DataTable table = new DataTable();

            using (myCon)
            {
                myCon.Open();
                using (myCommand)
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            //aqui retornamos as informações em formato JSON
            return new JsonResult(table);
            //--------------------------
        }
    }
}
