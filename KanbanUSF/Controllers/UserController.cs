using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using KanbanUSF.Models;
using HttpGPPD;

namespace KanbanUSF.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private HttpServices HttpServices = new HttpServices();
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            //aqui determinamos quais elementos da tabela queremos extrair:
            string query = @"SELECT Id, Nome, Senha FROM [dbo].[User]";
            //aqui chamamos a função GET do HttpGPPD enviando a query
            return new JsonResult(HttpServices.Get(query, _configuration));
        }

        [HttpPost]
        public JsonResult Post(User user)
        {
            //aqui determinamos quais valores iremos inserir na tabela:
            string query = @"INSERT INTO dbo.Nome, dbo.Senha VALUES
                            ('" + user.Nome + @"'),
                            ('" + user.Senha + @"')";
            //aqui chamamos a função POST do HttpGPPD enviando a query
            return new JsonResult(HttpServices.Post(query, _configuration));
        }

        [HttpPut]
        public JsonResult Put(User user)
        {
            //aqui determinamos quais valores iremos alterar na tabela:
            string query = @"
                            UPDATE dbo.User SET
                            Name = '" + user.Nome + @"'
                            where DepartmentID = " + user.Id + @"
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
                            DELETE FROM dbo.User
                            where Id = " + id + @"
                            ";
            //aqui chamamos a função DELETE do HttpGPPD enviando a query
            return new JsonResult(HttpServices.Delete(query, _configuration));
        }
    }
}
