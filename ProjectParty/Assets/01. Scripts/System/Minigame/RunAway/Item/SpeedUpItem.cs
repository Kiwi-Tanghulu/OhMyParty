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
        [SerializeField] private float disableTime;
        [SerializeField] private ApplyType applyType;

        private CharacterStat playerStat;

        public override void Init()
        {
            base.Init();

            playerStat = ownerPlayer.GetCharacterComponent<CharacterStat>();
        }

        public override void OnActive()
        {
            base.OnActive();

            switch(applyType)
            {
                case ApplyType.Additive:
                    playerStat.AddModifier(CharacterStatType.MaxMoveSpeed, increaseAmount, disableTime);
                    break;
                case ApplyType.Multiply:
                    playerStat.AddModifier(CharacterStatType.MaxMoveSpeed,
                        playerStat.GetStat(CharacterStatType.MaxMoveSpeed).Value * (increaseAmount - 1 / increaseAmount),
                        disableTime);
                    break;
            }
        }
    }
}
