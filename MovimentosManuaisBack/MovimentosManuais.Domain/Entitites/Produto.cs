using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovimentosManuais.Domain.Entitites
{
    public class Produto
    {
        public Produto()
        {
            ProdutoCosif = new HashSet<ProdutoCosif>();
            MovimentoManual = new HashSet<MovimentoManual>();
        }

        public int COD_PRODUTO { get; set; }
        public string DES_PRODUTO { get; set; }
        public bool STA_STATUS { get; set; }

        public virtual ICollection<ProdutoCosif> ProdutoCosif { get; set; }
        public virtual ICollection<MovimentoManual> MovimentoManual { get; set; }

        public static string[] Includes => new string[] { "ProdutoCosif", "MovimentoManual" };
    }
}