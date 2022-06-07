using System;

namespace MovimentosManuais.Domain.ViewModels
{
    public class MovimentoManualViewModel
    {
        public int NUM_LANCAMENTO { get; set; }
        public int DAT_MES { get; set; }
        public int DAT_ANO { get; set; }
        public int? COD_PRODUTO { get; set; }
        public int? COD_COSIF { get; set; }
        public string DES_DESCRICAO { get; set; }
        public string COD_USUARIO { get; set; }
        public decimal VAL_VALOR { get; set; }
    }
}