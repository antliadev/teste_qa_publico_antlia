namespace MovimentosManuais.Domain.ViewModels
{
    public class MovimentoViewModel
    {
        public int Mes { get; set; }
        public int Ano { get; set; }
        public int NumLancamento { get; set; }
        public string DesProduto { get; set; }
        public string CodClassificacao { get; set; }
        public decimal Valor { get; set; }        
    }
}