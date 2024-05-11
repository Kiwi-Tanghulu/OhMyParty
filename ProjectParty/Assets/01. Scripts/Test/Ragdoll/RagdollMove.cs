using OMG.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Ragdoll
{
    public class RagdollMove : MonoBehaviour
    {
        [SerializeField] PlayInputSO input;
        [SerializeField] Rigidbody hip;
        Vector3 dir;
        Vector3 prevDir;

        private float speed;

        private PhysicsAnimator anim;

        private void Awake()
        {
            InputManager.ChangeInputMap(InputMapType.Play);

            input.OnMoveEvent += SetMoveDir;

            anim = GetComponent<PhysicsAnimator>();
        }

        private void Update()
        {
            //transform.position += prevDir * speed * Time.deltaTime;

            //if (dir != Vector3.zero)
            //    speed += Time.deltaTime * 5f;
            //else
            //    speed -= Time.deltaTime * 5f;

            //anim.SetCopyMotionWeight(speed / 5f);

            //speed = Mathf.Clamp(speed, 0f, 5f);
        }

        private void SetMoveDir(Vector2 input)
        {
            dir = new Vector3(input.x, 0f, input.y);

            if (dir != Vector3.zero)
                prevDir = dir;
        }
    }
}