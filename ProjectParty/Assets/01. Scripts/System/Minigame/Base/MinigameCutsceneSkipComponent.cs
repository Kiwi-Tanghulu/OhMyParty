using OMG.Extensions;
using OMG.Inputs;
using OMG.NetworkEvents;
using NetworkEvent = OMG.NetworkEvents.NetworkEvent;
using Unity.Netcode;
using UnityEngine;
using System;

namespace OMG.Minigames
{
    public class MinigameCutsceneSkipComponent : NetworkBehaviour
    {
        [SerializeField] UIInputSO input = null;
        private NetworkEvent<UlongParams> onSkipReadyEvent = new NetworkEvent<UlongParams>("SkipReady");
        private NetworkEvent onSkipEvent = new NetworkEvent("Skip");

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
            onSkipReadyEvent.AddListener(HandleSkipReady);
            onSkipEvent.AddListener(HandleSkip);
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            onSkipReadyEvent.Register(NetworkObject);
            onSkipEvent.Register(NetworkObject);
        }

        public override void OnNetworkDespawn()
        {
            base.OnNetworkDespawn();
            onSkipReadyEvent.Unregister();
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
            onSkipReadyEvent?.Broadcast(NetworkManager.Singleton.LocalClientId, false);
        }

        private void HandleSkipReady(UlongParams clientID)
        {
            minigame.CutscenePanel.SetSkip(clientID);

            if(IsHost == false)
                return;

            minigame.PlayerDatas.ChangeData(i => i.clientID == clientID, player => {
                player.isSkipCutscene = true;
                return player;
            });

            TrySkip();
        }

        private void HandleSkip(NoneParams unused)
        {
            cutscene.SkipCutscene();
            input.OnSpaceEvent -= HandleSkipInput;
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

            onSkipEvent?.Broadcast();
        }
    }
}
