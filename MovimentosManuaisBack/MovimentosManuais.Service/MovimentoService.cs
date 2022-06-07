using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovimentosManuais.Data.Repositories;
using MovimentosManuais.Domain.Entitites;
using MovimentosManuais.Domain.ViewModels;

namespace MovimentosManuais.Service
{
    public class MovimentoService
    {
        private readonly ProdutoRepository _produtoRepository;
        private readonly ProdutoCosifRepository _produtoCosifRepository;
        private readonly MovimentoManualRepository _movimentoManualRepository;

        public MovimentoService(ProdutoRepository produtoRepository,
                                ProdutoCosifRepository produtoCosifRepository,
                                MovimentoManualRepository movimentoManualRepository)
        {
            _produtoRepository = produtoRepository;
            _produtoCosifRepository = produtoCosifRepository;
            _movimentoManualRepository = movimentoManualRepository;
        }
    

        public ResultViewModel ListaTodosMovimentosManuais()
        {
            try
            {
                var data = _movimentoManualRepository.GetAllNoTracking(MovimentoManual.Includes)
                                                     .Select(x => new MovimentoViewModel
                                                     {
                                                         CodClassificacao = x.ProdutoCosif?.COD_CLASSIFICACAO,
                                                         Ano = x.DAT_ANO,
                                                         Mes = x.DAT_MES,
                                                         DesProduto = x.Produto?.DES_PRODUTO,
                                                         NumLancamento = x.NUM_LANCAMENTO,
                                                         Valor = x.VAL_VALOR
                                                     })
                                                     .OrderByDescending(x => x.Ano)
                                                     .ThenByDescending(x => x.Mes)
                                                     .ThenBy(x => x.NumLancamento)
                                                     .ToList();

                return new ResultViewModel
                {
                    Data = data,
                    Message = "Ok",
                    Success = true
                };
            }
            catch (System.Exception ex)
            {
                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false
                };
            }   
        }

        public ResultViewModel ListaTodosProdutos()
        {
            try
            {
                var data = _produtoRepository.GetNoTracking(x => x.STA_STATUS == true)
                                             .Select(x => new ResultValuePair
                                             {
                                                 Codigo = x.COD_PRODUTO,
                                                 Descricao = x.DES_PRODUTO
                                             })
                                             .OrderBy(x => x.Descricao)
                                             .ToList();

                return new ResultViewModel
                {
                    Data = data,
                    Message = "Ok",
                    Success = true
                };
            }
            catch (System.Exception ex)
            {
                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false
                };
            }   
        }
    
        public ResultViewModel ListaTodosProdutoCosif()
        {
            try
            {
                var data = _produtoCosifRepository.GetNoTracking(x => x.STA_STATUS == true)
                                                  .Select(x => new ResultValuePair
                                                  {
                                                      Codigo = x.COD_COSIF,
                                                      Descricao = x.COD_CLASSIFICACAO
                                                  })
                                                  .OrderBy(x => x.Descricao)
                                                  .ToList();

                return new ResultViewModel
                {
                    Data = data,
                    Message = "Ok",
                    Success = true
                };
            }
            catch (System.Exception ex)
            {
                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false
                };
            }   
        }
    
        public ResultViewModel ListaProdutoCosifPorProduto(int produto)
        {
            try
            {
                var data = _produtoCosifRepository.GetNoTracking(x => x.COD_PRODUTO == produto && x.STA_STATUS == true)
                                                  .Select(x => new ResultValuePair
                                                  {
                                                      Codigo = x.COD_COSIF,
                                                      Descricao = x.COD_CLASSIFICACAO
                                                  })
                                                  .OrderBy(x => x.Descricao)
                                                  .ToList();

                return new ResultViewModel
                {
                    Data = data,
                    Message = "Ok",
                    Success = true
                };
            }
            catch (System.Exception ex)
            {
                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false
                };
            }   
        }
    
        public ResultViewModel InsereNovoMovimentoManual(MovimentoManualViewModel model)
        {
            try
            {
                var entity = new MovimentoManual
                {
                    COD_COSIF = model.COD_COSIF,
                    COD_PRODUTO = model.COD_PRODUTO,
                    COD_USUARIO = model.COD_USUARIO,
                    DAT_ANO = model.DAT_ANO,
                    DAT_MES = model.DAT_MES,
                    DAT_MOVIMENTO = DateTime.Now,
                    DES_DESCRICAO = model.DES_DESCRICAO,
                    NUM_LANCAMENTO = model.NUM_LANCAMENTO,
                    VAL_VALOR = model.VAL_VALOR
                };

                _movimentoManualRepository.Add(entity);
                _movimentoManualRepository.Commit();

                var movimentos = ListaTodosMovimentosManuais();

                return new ResultViewModel
                {
                    Data = movimentos.Data,
                    Message = "Ok",
                    Success = true
                };
            }
            catch (System.Exception ex)
            {
                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false
                };
            }
        }
    }
}