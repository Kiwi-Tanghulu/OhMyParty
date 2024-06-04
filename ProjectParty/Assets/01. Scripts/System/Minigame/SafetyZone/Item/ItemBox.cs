using System.Collections.Generic;
using OMG.Extensions;
using OMG.Interacting;
using OMG.Items;
using UnityEngine;

namespace OMG.Minigames.SafetyZone
{
    public class ItemBox : MonoBehaviour, IInteractable
    {
        [SerializeField] List<HoldableItem> prefabs = new List<HoldableItem>();

        public bool Interact(Component performer, bool actived, Vector3 point = default)
        {
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

