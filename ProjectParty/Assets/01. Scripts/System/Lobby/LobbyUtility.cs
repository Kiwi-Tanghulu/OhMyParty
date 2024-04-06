using Unity.Netcode;

namespace OMG.Lobbies
{
    public partial class Lobby
    {
        public void SetActive(bool active)
        {
            SetActiveServerRpc(active);
        }

        [ServerRpc(RequireOwnership = false)]
        private void SetActiveServerRpc(bool active)
        {
            SetActiveClientRpc(active);
        }

        [ClientRpc]
        private void SetActiveClientRpc(bool active)
        {
            gameObject.SetActive(active);
        }
    }
}
