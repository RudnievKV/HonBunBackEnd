using HonbunNoAnkiApi.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace HonbunNoAnkiApi.Services.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepo UserRepo { get; }
        IWordDefinitionRepo WordDefinitionRepo { get; }
        IStageRepo StageRepo { get; }
        IWordCollectionRepo WordCollectionRepo { get; }
        IWordRepo WordRepo { get; }
        IDictionaryWordRepo DictionaryWordRepo { get; }
        IDictionaryKanjiRepo KanjiRepo { get; }
        IMeaningRepo MeaningRepo { get; }
        IReadingRepo ReadingRepo { get; }
        IMeaningValueRepo MeaningValueRepo { get; }
        Task<int> SaveChangesAsync();
    }
}
