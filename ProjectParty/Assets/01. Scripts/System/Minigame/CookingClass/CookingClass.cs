using System;
using System.Collections.Generic;
using OMG.NetworkEvents;
using UnityEngine;

namespace OMG.Minigames.CookingClass
{
    public class CookingClass : PlayableMinigame
    {
        [SerializeField] Dispenser dispenserPrefab = null;
        [SerializeField] float dispenserCount = 2;
        private LinkedList<Dispenser> dispensers = null;

        protected override void OnGameInit()
        {
            base.OnGameInit();
        }

        protected override void OnGameStart()
        {
            base.OnGameStart();

            if(IsHost == false)
                return;

            dispensers = new LinkedList<Dispenser>();
            for(int i = 0; i < dispenserCount; ++i)
            {
                Dispenser instance = Instantiate(dispenserPrefab, transform.position, Quaternion.identity);
                instance.Init(this);
                instance.NetworkObject.Spawn();
                dispensers.AddLast(instance);
            }

            (cycle as TimeAttackCycle).StartCycle();
        }

        protected override void OnGameFinish()
        {
            base.OnGameFinish();

            if(IsHost == false)
                return;

            foreach(Dispenser instance in dispensers)
                instance.Release();
        }

        protected override void OnGameRelease()
        {
            base.OnGameRelease();

            if(IsHost == false)
                return;

            foreach(Dispenser instance in dispensers)
                instance.NetworkObject.Despawn();
        }
    }
}
