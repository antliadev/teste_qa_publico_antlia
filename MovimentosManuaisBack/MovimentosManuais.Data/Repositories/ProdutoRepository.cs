using MovimentosManuais.Data.Support;
using MovimentosManuais.Domain.Entitites;

namespace MovimentosManuais.Data.Repositories
{
    public class ProdutoRepository : Repository<Produto>
    {
        private readonly DataContext _context;

        public ProdutoRepository(DataContext context) : base(context)
        {
            _context = context;
        }
        
    }
}