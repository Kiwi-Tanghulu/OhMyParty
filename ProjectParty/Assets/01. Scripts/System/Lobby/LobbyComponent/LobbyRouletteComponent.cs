using DG.Tweening;
using OMG.UI;
using System;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Lobbies
{
    //rpc ssage
    public class LobbyRouletteComponent : LobbyComponent
    {
        [SerializeField] private MinigameRouletteContainer rouletteUI;
        [SerializeField] private float stopDelay;
        private Action onStopAction;

        public override void Init(Lobby lobby)
        {
            base.Init(lobby);

            rouletteUI.OnRouletteStopEvent.AddListener(RouletteUI_OnRouletteStopEvent);
        }

        public void Play()
        {
            rouletteUI.Show();
        }

        public void Stop(Action onStopAction)
        {
            this.onStopAction = onStopAction;
            rouletteUI.StopRoulette();
        }

        private void RouletteUI_OnRouletteStopEvent()
        {
            AlignSlotClientRpc(rouletteUI.SlotList.IndexOf(rouletteUI.SelectedSlot));
        }

        [ClientRpc]
        private void AlignSlotClientRpc(int focusSlotIndex)
        {
            Sequence seq = rouletteUI.AlignSlotTween(focusSlotIndex);
            seq.AppendInterval(stopDelay);
            seq.AppendCallback(() => onStopAction?.Invoke());
            seq.Play();
        }
    }
}
