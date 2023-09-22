using HonbunNoAnkiApi.DBContext;
using HonbunNoAnkiApi.Repositories;
using HonbunNoAnkiApi.Repositories.Interfaces;
using HonbunNoAnkiApi.Services.Interfaces;
using System.Threading.Tasks;

namespace HonbunNoAnkiApi.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyDBContext _context;
        public UnitOfWork(MyDBContext context)
        {
            _context = context;
            UserRepo = new UserRepo(_context);
            MeaningReadingRepo = new MeaningReadingRepo(_context);
            StageRepo = new StageRepo(_context);
            WordCollectionRepo = new WordCollectionRepo(_context);
            WordRepo = new WordRepo(_context);
        }
        public IUserRepo UserRepo { get; private set; }
        public IMeaningReadingRepo MeaningReadingRepo { get; private set; }
        public IStageRepo StageRepo { get; private set; }
        public IWordCollectionRepo WordCollectionRepo { get; private set; }
        public IWordRepo WordRepo { get; private set; }
        public IDictionaryWordRepo DictionaryWordRepo { get; private set; }
        public IDictionaryKanjiRepo KanjiRepo { get; private set; }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public async Task DisposeAsync()
        {
            await _context.DisposeAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
