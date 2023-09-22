using HonbunNoAnkiApi.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace HonbunNoAnkiApi.Services.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepo UserRepo { get; }
        IMeaningReadingRepo MeaningReadingRepo { get; }
        IStageRepo StageRepo { get; }
        IWordCollectionRepo WordCollectionRepo { get; }
        IWordRepo WordRepo { get; }
        IDictionaryWordRepo DictionaryWordRepo { get; }
        IDictionaryKanjiRepo KanjiRepo { get; }
        Task<int> SaveChangesAsync();
    }
}
