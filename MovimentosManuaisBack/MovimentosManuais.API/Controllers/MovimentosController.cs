using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovimentosManuais.Domain.ViewModels;
using MovimentosManuais.Service;

namespace MovimentosManuais.API.Controllers
{
    [ApiController]

    public class MovimentosController : ControllerBase
    {
        private readonly MovimentoService _movimentoService;

        public MovimentosController(MovimentoService movimentoService)
        {
            _movimentoService = movimentoService;
        }


        [HttpGet]
        [Route("v1/Movimentos/ListaTodosMovimentosManuais")]
        public ResultViewModel ListaTodosMovimentosManuais()
        {
            return _movimentoService.ListaTodosMovimentosManuais();
        }

        [HttpGet]
        [Route("v1/Movimentos/ListaTodosProdutos")]
        public ResultViewModel ListaTodosProdutos()
        {
            return _movimentoService.ListaTodosProdutos();
        }

        [HttpGet]
        [Route("v1/Movimentos/ListaTodosProdutoCosif")]
        public ResultViewModel ListaTodosProdutoCosif()
        {
            return _movimentoService.ListaTodosProdutoCosif();
        }

        [HttpGet]
        [Route("v1/Movimentos/ListaProdutoCosifPorProduto/{produto}")]
        public ResultViewModel ListaProdutoCosifPorProduto(int produto)
        {
            return _movimentoService.ListaProdutoCosifPorProduto(produto);
        }

        [HttpPost]
        [Route("v1/Movimentos/InsereNovoMovimentoManual")]
        public ResultViewModel InsereNovoMovimentoManual([FromBody]MovimentoManualViewModel model)
        {
            return _movimentoService.InsereNovoMovimentoManual(model);
        }
    }
}