using OMG.Lobbies;
using UnityEngine;

namespace OMG.Skins
{
    public class LobbySkin : Skin
    {
        [SerializeField] Transform minigameSpotTrm = null;

        public override void Init()
        {
            base.Init();

            Transform minigameSpot = Lobby.Current.transform.Find("MinigameSpot");
            minigameSpot.position = minigameSpotTrm.position;
            minigameSpot.rotation = minigameSpotTrm.rotation;
        }

        public override void Release()
        {
            base.Release();
        }
    }
}
