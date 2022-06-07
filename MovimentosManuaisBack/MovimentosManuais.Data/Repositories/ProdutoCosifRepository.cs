using MovimentosManuais.Data.Support;
using MovimentosManuais.Domain.Entitites;

namespace MovimentosManuais.Data.Repositories
{
    public class ProdutoCosifRepository : Repository<ProdutoCosif>
    {
        private readonly DataContext _context;

        public ProdutoCosifRepository(DataContext context) : base(context)
        {
            _context = context;
        }
        
    }
}