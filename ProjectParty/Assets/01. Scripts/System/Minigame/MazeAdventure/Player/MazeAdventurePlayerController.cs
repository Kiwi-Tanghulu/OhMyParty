using OMG.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace OMG.Minigames.MazeAdventure
{
    public class MazeAdventurePlayerController : PlayerController
    {
        private bool isDead;
        [SerializeField] private UnityEvent deadEvent;
        public override void Init(ulong ownerId)
        {
            base.Init(ownerId);
            isDead = false;
        }
        protected override void Update()
        {
            if(!isDead)
            base.Update();
        }

        public void PlayerDead()
        {
            deadEvent.Invoke();

        }
    }
}
