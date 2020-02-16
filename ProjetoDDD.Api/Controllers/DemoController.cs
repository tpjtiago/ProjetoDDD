using Crosscutting.Domain.Bus;
using Crosscutting.Domain.Controller;
using ProjetoDDD.Application.Interfaces;
using ProjetoDDD.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ProjetoDDD.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/demo")]
    [ApiController]
    public class DemoController : ApiController
    {
        private readonly IDemoServiceApp _demoServiceApp;

        public DemoController(
            IDemoServiceApp demoServiceApp,
            IMediatorHandler mediator) : base(mediator)
        {
            _demoServiceApp = demoServiceApp;
        }

        /// <summary>
        /// Efetua a consulta paginada dos registros cadastrados
        /// </summary>
        /// <param name="page">Página atual</param>
        /// <param name="size">Quantidade de registros por página</param>
        /// <param name="orderProperty">Propriedade para ordenação</param>
        /// <param name="orderCrescent">Ordem dos registros</param>
        /// <param name="filterProperty">Propriedade para filtro</param>
        /// <param name="filterValue">Valor da propridade de filtro</param>
        /// <returns>Lista paginada de registros</returns>
        [HttpGet]
        public async Task<IActionResult> Get(int page = 1, int size = 20,
            string orderProperty = "Id", string orderCrescent = "true",
            string filterProperty = null, string filterValue = null)
        {
            var demos = await _demoServiceApp.GetPagedDemos(page, size, orderProperty,
                Convert.ToBoolean(orderCrescent), filterProperty, filterValue);

            return Response(demos);
        }

        /// <summary>
        /// Efetua a consulta de um registro pelo seu código
        /// </summary>
        /// <param name="id">Código do registro</param>
        /// <returns>Registro encontrado</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var demo = await _demoServiceApp.GetDemo(Guid.Parse(id));

            return Response(demo);
        }

        /// <summary>
        /// Efetua o cadastro de um novo registro
        /// </summary>
        /// <param name="model">Dados do registro</param>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DemoViewModel model)
        {
            await _demoServiceApp.Save(model);

            return Response();
        }

        /// <summary>
        /// Efetua a atualização de um registro
        /// </summary>
        /// <param name="model">Dados do registro</param>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] DemoViewModel model)
        {
            await _demoServiceApp.Save(model, update: true);

            return Response();
        }

        /// <summary>
        /// Efetua a exclusão de um registro
        /// </summary>
        /// <param name="id">Código do registro</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _demoServiceApp.Remove(Guid.Parse(id));

            return Response();
        }
    }
}