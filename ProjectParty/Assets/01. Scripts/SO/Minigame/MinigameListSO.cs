using System.Collections.Generic;
using UnityEngine;

namespace OMG.Minigames
{
    [CreateAssetMenu(menuName = "SO/Minigame/MinigameList")]
    public class MinigameListSO : ScriptableObject
    {
        [SerializeField] List<MinigameSO> minigameList = null;

        public MinigameSO this[int index] => minigameList[index];
        public int Count => minigameList.Count;
    }
}
