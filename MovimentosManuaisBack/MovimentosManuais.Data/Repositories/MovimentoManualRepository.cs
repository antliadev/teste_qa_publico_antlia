using MovimentosManuais.Data.Support;
using MovimentosManuais.Domain.Entitites;

namespace MovimentosManuais.Data.Repositories
{
    public class MovimentoManualRepository : Repository<MovimentoManual>
    {
        private readonly DataContext _context;

        public MovimentoManualRepository(DataContext context) : base(context)
        {
            _context = context;
        }
        
    }
}