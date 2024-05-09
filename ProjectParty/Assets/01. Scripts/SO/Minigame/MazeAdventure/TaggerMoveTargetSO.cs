using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Minigames.MazeAdventure
{
    [CreateAssetMenu(menuName = "SO/Minigame/MazeAdventure/TaggerMoveTarget")]
    public class TaggerMoveTargetSO : ScriptableObject
    {
        public List<Vector3> moveTargetList;
    }
}
