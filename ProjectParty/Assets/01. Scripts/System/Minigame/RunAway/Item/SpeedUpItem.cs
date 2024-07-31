using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Items
{
    public class SpeedUpItem : PlayerItem
    {
        enum ApplyType
        {
            Additive, 
            Multiply
        }

        [SerializeField] private float increaseAmount;
        [SerializeField] private ApplyType applyType;

        public override void OnActive()
        {
            base.OnActive();

            switch(applyType)
            {
                case ApplyType.Additive:
                    //
                    break;
                case ApplyType.Multiply:
                    //
                    break;
            }
        }
    }
}
