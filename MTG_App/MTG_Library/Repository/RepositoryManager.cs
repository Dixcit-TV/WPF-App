using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTG_Library.Helpers;

namespace MTG_Library.Repository
{
    public partial class RepositoryManager
    {
        private static RepositoryManager _instance;
        public static RepositoryManager Instance => _instance ??= new RepositoryManager();

        private readonly Dictionary<RepositoryType, ICardRepository> _repositories;

        public ICardRepository CurrentRepository { get; private set; }

        public RepositoryManager()
        {
            _repositories = new Dictionary<RepositoryType, ICardRepository>
            {
                { RepositoryType.OfflineJson, new OfflineCardRepository() }
                , { RepositoryType.OnlineAPI, new OnlineApiCardRepositoy() }
            };
        }

        public void SwitchRepository(RepositoryType type)
        {
            if (_repositories.Keys.Contains(type))
                CurrentRepository = _repositories[type];
            else
                CurrentRepository = _repositories[0];
        }
    }
}
