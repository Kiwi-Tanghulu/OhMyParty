using OMG.Items;
using UnityEngine;

namespace OMG.Minigames.MazeAdventure
{
    public class MakeWall : MazeAdventureItem
    {
        [SerializeField] private float makeLength;
        [SerializeField] private float makeHeight;

        private MakeWallItemBehaviour makeWallItemBehaviour;

        public override void Init(Transform playerTrm)
        {
            base.Init(playerTrm);
            makeWallItemBehaviour = itemBehaviour as MakeWallItemBehaviour;
        }
        public override void OnActive()
        {
            if (IsOwner)
            {
                Vector3 makePos = playerTrm.position + playerTrm.forward * makeLength + playerTrm.up * makeHeight;
                Vector3 lookDirection = playerTrm.forward;
                makeWallItemBehaviour.MakeWallServerRPC(makePos, lookDirection);
            }
        }
    }
}
