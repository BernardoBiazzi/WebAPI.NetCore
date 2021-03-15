using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using WebAPI.Models;
using HttpGPPD;

namespace WebAPI.Controllers
{
    //aqui definimos a rota que caso acessemos pelo httpget extrairemos/inseriremos as informações
    [Route("api/[controller]")]
    //quando a rota está como [controller] ela vai entender qual é de acordo com o nome do controller, nesse caso seria a mesma coisa que "api/department"
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private HttpServices HttpServices = new HttpServices();
        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            //aqui determinamos quais elementos da tabela queremos extrair:
            string query = @"select EmployeeID, EmployeeName, Department, DateOfJoining, PhotoFileName from dbo.Employee";
            //aqui chamamos a função GET do HttpGPPD enviando a query
            return new JsonResult(HttpServices.Get(query, _configuration));
        }

        [HttpPost]
        public JsonResult Post(Employee employee)
        {
            //aqui determinamos quais valores iremos inserir na tabela:
            string query = @"insert into dbo.Employee values
                            ('" + employee.EmployeeName + @"', 
                            '" + employee.Department + @"', 
                            '" + employee.DateOfJoining + @"', 
                            '" + employee.PhotoFileName + @"')";
            //aqui chamamos a função POST do HttpGPPD enviando a query
            return new JsonResult(HttpServices.Post(query, _configuration));
        }

        [HttpPut]
        public JsonResult Put(Employee employee)
        {
            //aqui determinamos quais valores iremos alterar na tabela:
            string query = @"
                            update dbo.Employee set
                            EmployeeName = '" + employee.EmployeeName + @"',
                            Department = '" + employee.Department + @"',
                            DateOfJoining = '" + employee.DateOfJoining + @"',
                            PhotoFileName = '" + employee.PhotoFileName + @"'
                            where EmployeeID = " + employee.EmployeeID + @"
                            ";
            //aqui chamamos a função PUT do HttpGPPD enviando a query
            return new JsonResult(HttpServices.Put(query, _configuration));
        }

        //quando queremos passar a informação pela url precisamos adicionar esse ("{info}")
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            //aqui determinamos qual item da tabela iremos remover
            string query = @"
                            delete from dbo.Employee
                            where EmployeeID = " + id + @"
                            ";
            //aqui chamamos a função DELETE do HttpGPPD enviando a query
            return new JsonResult(HttpServices.Delete(query, _configuration));
        }
    }
}
