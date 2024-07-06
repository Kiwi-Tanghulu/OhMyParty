using OMG.Extensions;
using UnityEngine;

namespace OMG.Minigames.Utility
{
    public class SpawnPositionTable : MonoBehaviour
    {
        [SerializeField] SpawnPosition[] spawnPositions = null;

        public SpawnPosition GetPosition()
        {
            SpawnPosition position = null;
            do 
                position = spawnPositions.PickRandom();
            while(position.Used == true);

            return position;
        }
    }
}
