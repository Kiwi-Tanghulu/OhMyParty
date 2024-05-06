using OMG.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollMove : MonoBehaviour
{
    [SerializeField] PlayInputSO input;
    [SerializeField] Rigidbody hip;
    Vector3 dir;

    private void Awake()
    {
        InputManager.ChangeInputMap(InputMapType.Play);

        input.OnMoveEvent += SetMoveDir;
    }

    private void Update()
    {
        transform.position += dir * 3f * Time.deltaTime;
    }

    private void SetMoveDir(Vector2 dir)
    {
        this.dir = new Vector3(dir.x, 0f, dir.y);
    }
}
