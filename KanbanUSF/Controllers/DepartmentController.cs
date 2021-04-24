using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using KanbanUSF.Models;
using HttpGPPD;

namespace KanbanUSF.Controllers
{
    //aqui definimos a rota que caso acessemos pelo httpget extrairemos/inseriremos as informações
    [Route("api/[controller]")]
    //quando a rota está como [controller] ela vai entender qual é de acordo com o nome do controller, nesse caso seria a mesma coisa que "api/department"
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private HttpServices HttpServices = new HttpServices();
        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            //aqui determinamos quais elementos da tabela queremos extrair:
            string query = @"select DepartmentId, DepartmentName from dbo.Department";
            //aqui chamamos a função GET do HttpGPPD enviando a query
            return new JsonResult(HttpServices.Get(query, _configuration));
        }

        [HttpPost]
        public JsonResult Post(Department dep)
        {
            //aqui determinamos quais valores iremos inserir na tabela:
            string query = @"insert into dbo.Department values
                            ('" + dep.DepartmentName + @"')";
            //aqui chamamos a função POST do HttpGPPD enviando a query
            return new JsonResult(HttpServices.Post(query, _configuration));
        }

        [HttpPut]
        public JsonResult Put(Department dep)
        {
            //aqui determinamos quais valores iremos alterar na tabela:
            string query = @"
                            update dbo.Department set
                            DepartmentName = '" + dep.DepartmentName + @"'
                            where DepartmentID = "+ dep.DepartmentID + @"
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
                            delete from dbo.Department
                            where DepartmentID = " + id + @"
                            ";
            //aqui chamamos a função DELETE do HttpGPPD enviando a query
            return new JsonResult(HttpServices.Delete(query, _configuration));
        }
    }
}
