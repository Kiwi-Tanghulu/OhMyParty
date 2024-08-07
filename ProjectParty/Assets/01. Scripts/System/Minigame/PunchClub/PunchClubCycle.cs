using UnityEngine;
using OMG.Player;

namespace OMG.Minigames.PunchClub
{
    public class PunchClubCycle : DeathmatchCycle
    {
        protected override void OnPlayerDead(ulong clientID)
        {
            base.OnPlayerDead(clientID);
            
            if(IsHost == false)
                return;

            PlayerController player = (minigame as PlayableMinigame).PlayerDictionary[clientID];
            (minigame as PlayableMinigame).PlayerDictionary.Remove(clientID);
            player.NetworkObject.Despawn();
            Debug.Log(player.name);
        }
    }
}
