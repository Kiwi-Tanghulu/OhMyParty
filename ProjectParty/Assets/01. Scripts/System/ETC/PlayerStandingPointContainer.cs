using OMG.Extensions;
using OMG.Lobbies;
using UnityEngine;

namespace OMG
{
    public class PlayerStandingPointContainer : MonoBehaviour
    {
        [SerializeField] private Transform[] calculatePlayerScorePoints;

        public Transform GetStandingPoint(ulong clientID)
        {
            int index = Lobby.Current.PlayerDatas.
                Find(out Lobbies.PlayerData foundPlayer, x => x.clientID == clientID);

            return calculatePlayerScorePoints[index];
        }
    }
}