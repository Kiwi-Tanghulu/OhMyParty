using OMG.Extensions;
using OMG.Inputs;
using OMG.NetworkEvents;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Minigames
{
    public class MinigameCutsceneSkipComponent : NetworkBehaviour
    {
        [SerializeField] UIInputSO input = null;
        private NetworkEvent<UlongParams> onSkipEvent = new NetworkEvent<UlongParams>("SkipEvent");

        private Minigame minigame = null;
        private MinigameCutscene cutscene = null;
    
        private bool skipCutscene = false;

        private void Awake()
        {
            minigame = GetComponent<Minigame>();
            cutscene = GetComponent<MinigameCutscene>();
        }

        private void Start()
        {
            input.OnSpaceEvent += HandleSkipInput;
            onSkipEvent.AddListener(HandleSkip);
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            onSkipEvent.Register(NetworkObject);
        }

        public override void OnNetworkDespawn()
        {
            base.OnNetworkDespawn();
            onSkipEvent.Unregister();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            input.OnSpaceEvent -= HandleSkipInput;
        }
        
        private void HandleSkipInput()
        {
            if(MinigameManager.Instance.MinigamePaused)
                return;

            if(skipCutscene)
                return;

            skipCutscene = true;
            onSkipEvent?.Broadcast(NetworkManager.Singleton.LocalClientId);
        }

        private void HandleSkip(UlongParams clientID)
        {
            Debug.Log($"Client {clientID} has been skip");

            if(IsHost == false)
                return;

            minigame.PlayerDatas.ChangeData(i => i.clientID == clientID, player => {
                player.isSkipCutscene = true;
                return player;
            });

            TrySkip();
        }

        private void TrySkip()
        {
            bool skip = true;
            foreach(PlayerData player in minigame.PlayerDatas)
            {
                skip &= player.isSkipCutscene;
                if(skip == false)
                    return;
            }

            cutscene.SkipCutscene();
            input.OnSpaceEvent -= HandleSkipInput;
        }
    }
}
