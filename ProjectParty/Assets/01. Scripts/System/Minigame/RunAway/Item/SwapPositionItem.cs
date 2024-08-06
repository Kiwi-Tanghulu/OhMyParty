using UnityEngine;
using OMG.Lobbies;
using OMG.Player;

namespace OMG.Items
{
    public class SwapPositionItem : PlayerItem
    {
        public override void OnActive()
        {
            base.OnActive();

            if (Lobby.Current.PlayerDatas.Count < 2)
                return;

            int randomIndex = 0;
            do
            {
                randomIndex = UnityEngine.Random.Range(0, Lobby.Current.PlayerDatas.Count);
            }
            while(Lobby.Current.PlayerContainer[randomIndex].OwnerClientId == ownerPlayer.OwnerClientId);

            PlayerController targetPlayer = Lobby.Current.PlayerContainer[randomIndex];
            Vector3 ownerPos = ownerPlayer.transform.position;
            ownerPlayer.GetCharacterComponent<CharacterMovement>().Movement.Teleport(targetPlayer.transform.position);
            targetPlayer.GetCharacterComponent<CharacterMovement>().Movement.Teleport(ownerPos);
        }
    }
}
