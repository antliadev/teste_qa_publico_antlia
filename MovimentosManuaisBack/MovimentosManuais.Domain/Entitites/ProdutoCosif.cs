using System.Collections.Generic;
using Newtonsoft.Json;

namespace MovimentosManuais.Domain.Entitites
{
    public class ProdutoCosif
    {
        public ProdutoCosif()
        {
            MovimentoManual = new HashSet<MovimentoManual>();
        }
        
        public int COD_COSIF { get; set; }
        public int COD_PRODUTO { get; set; }
        public string COD_CLASSIFICACAO { get; set; }
        public bool STA_STATUS { get; set; }

        [JsonIgnore]
        public virtual Produto Produto { get; set; }

        public virtual ICollection<MovimentoManual> MovimentoManual { get; set; }

        public static string[] Includes => new string[] { "Produto", "MovimentoManual" };
    }
}