using System.Collections.Generic;
using OMG.Extensions;
using OMG.Interacting;
using OMG.Items;
using OMG.Timers;
using UnityEngine;

namespace OMG.Minigames.SafetyZone
{
    public class ItemBox : MonoBehaviour, IInteractable
    {
        [SerializeField] float cooltime = 3f;
        [SerializeField] List<HoldableItem> prefabs = new List<HoldableItem>();

        private Timer timer = null;
        private bool isCooldown = false;

        private void Awake()
        {
            timer = GetComponent<Timer>();
        }

        private void Start()
        {
            timer.ResetTimer();
            isCooldown = false;
        }

        public bool Interact(Component performer, bool actived, Vector3 point = default)
        {
            if(isCooldown)
                return false;

            isCooldown = true;
            timer.SetTimer(cooltime, () => isCooldown = false);

            HoldableItem item = Instantiate(prefabs.PickRandom());
            item.Init();

            IHolder hand = performer.GetComponent<IHolder>();
            if(hand == null)
                return false;

            hand.Hold(item);

            return true;
        }
    }
}

