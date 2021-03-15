﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using WebAPI.Models;

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

        //Função que possibilita inserir valores na tabela que quisermos.
        [HttpPost]
        public JsonResult Post(Employee employee)
        {
            //Daqui pra baixo estamos criando a conexão com a tabela do banco de dados que queremos inserir as informações
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon"); //EmployeeAppCon está definido no appsettings.json
            SqlConnection myCon = new SqlConnection(sqlDataSource);
            //aqui determinamos quais valores iremos inserir na tabela:
            string query = @"insert into dbo.Employee values
                            ('" + employee.EmployeeName + @"', 
                            '" + employee.Department + @"', 
                            '" + employee.DateOfJoining + @"', 
                            '" + employee.PhotoFileName + @"')";
            //-------------------------------------------
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

            //aqui retornamos um aviso de sucesso na inserção dos dados
            return new JsonResult("Added Succesfully");
            //-----------------------------------------
        }

        //Função que possibilita atualizar valores na tabela que quisermos.
        [HttpPut]
        public JsonResult Put(Employee employee)
        {
            //Daqui pra baixo estamos criando a conexão com a tabela do banco de dados que queremos alterar as informações
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon"); //EmployeeAppCon está definido no appsettings.json
            SqlConnection myCon = new SqlConnection(sqlDataSource);
            //aqui determinamos quais valores iremos alterar na tabela:
            string query = @"
                            update dbo.Employee set
                            EmployeeName = '" + employee.EmployeeName + @"',
                            Department = '" + employee.Department + @"',
                            DateOfJoining = '" + employee.DateOfJoining + @"',
                            PhotoFileName = '" + employee.PhotoFileName + @"'
                            where EmployeeID = " + employee.EmployeeID + @"
                            ";
            //-------------------------------------------
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

            //aqui retornamos um aviso de sucesso no update dos dados
            return new JsonResult("Updated Succesfully");
            //------------------------------------------
        }

        //Função que possibilita deletar a item tabela que quisermos -> quando queremos passar a informação pela url precisamos adicionar esse ("{info}")
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            //Daqui pra baixo estamos criando a conexão com a tabela do banco de dados que remover um item
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon"); //EmployeeAppCon está definido no appsettings.json
            SqlConnection myCon = new SqlConnection(sqlDataSource);
            //aqui determinamos qual item da tabela iremos remover
            string query = @"
                            delete from dbo.Employee
                            where EmployeeID = " + id + @"
                            ";
            //-------------------------------------------
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

            //aqui retornamos um aviso de sucesso na remoção de item da tabela
            return new JsonResult("Deleted Succesfully");
            //------------------------------------------
        }
    }
}
