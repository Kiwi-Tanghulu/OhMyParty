using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player
{
    public class RunAwayPlayerController : PlayerController
{
        public void AlignPos()
        {
            Vector3 pos = transform.position;
            pos.z = -2.5f;
            GetCharacterComponent<CharacterMovement>().Movement.Teleport(pos);
        }

        public override void Respawn(Vector3 pos, Vector3 rot)
        {
            base.Respawn(pos, rot);
            AlignPos();
        }
    }
}
