using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MovimentosManuais.Domain.Entitites
{
    public class MovimentoManual
    {
        public int COD_MOVIMENTO_MANUAL { get; set; }
        public int NUM_LANCAMENTO { get; set; }
        public int DAT_MES { get; set; }
        public int DAT_ANO { get; set; }
        public int? COD_PRODUTO { get; set; }
        public int? COD_COSIF { get; set; }
        public string DES_DESCRICAO { get; set; }
        public DateTime DAT_MOVIMENTO { get; set; }
        public string COD_USUARIO { get; set; }
        public decimal VAL_VALOR { get; set; }

        [JsonIgnore]
        public virtual Produto Produto { get; set; }

        [JsonIgnore]
        public virtual ProdutoCosif ProdutoCosif { get; set; }

        public static string[] Includes => new string[] { "Produto", "ProdutoCosif" };
    }
}