using NaughtyAttributes;
using UnityEngine;
using Zenject;

namespace PocketZone.SaveLoad
{
    public class SaveLoadService : MonoBehaviour
    {
        private IStorable[] _storables;

        [Inject]
        private void Constructor(IStorable[] storables) => _storables = storables;

        [Button("Load Game")]
        public void LoadGame()
        {
            foreach (IStorable storable in _storables)
                storable.Load();
        }

        [Button("Save Game")]
        public void SaveGame()
        {
            foreach (IStorable storable in _storables)
                storable.Save();
        }
    }
}