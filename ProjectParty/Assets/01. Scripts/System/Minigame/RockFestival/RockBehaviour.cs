using OMG.Items;
using Unity.Netcode;

namespace OMG.Minigames.RockFestival
{
    public class RockBehaviour : NetworkItemBehaviour
    {
        public ulong HolderID { get; private set; } = ulong.MaxValue;

        public void Hold(ulong holderID)
        {
            HolderID = holderID;
            HoldServerRpc(holderID);
        }

        [ServerRpc(RequireOwnership = false)]
        private void HoldServerRpc(ulong holderID, ServerRpcParams param = default)
        {
            if(HolderID != ulong.MaxValue && param.Receive.SenderClientId != HolderID)
                return;

            HoldClientRpc(holderID);
        }

        [ClientRpc]
        private void HoldClientRpc(ulong holderID)
        {
            HolderID = holderID;
        }
    }
}
