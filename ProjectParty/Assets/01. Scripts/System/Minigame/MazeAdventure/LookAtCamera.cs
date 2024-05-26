using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private enum Mode
    {
        LookAt,
        LookAtInverted, //* 반전 시켜 보기
        CameraForward,
        CameraForwardInverted, //* 반전 시켜 보기
    }

    [SerializeField] private Mode mode;
    [SerializeField] private bool continuousUpdate;
    private Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void Start()
    {
        SetObjectLookAt();
        if (!continuousUpdate)
            enabled = false;
    }
    private void LateUpdate()
    {
        SetObjectLookAt();
    }

    private void SetObjectLookAt()
    {
        switch (mode)
        {
            case Mode.LookAt:
                transform.LookAt(mainCam.transform);
                break;
            case Mode.LookAtInverted:
                //* 카메라 방향을 알아내서 그 방향 만큼 돌려줘서 반전시키기
                Vector3 dirFromCamera = transform.position - mainCam.transform.position;
                transform.LookAt(transform.position + dirFromCamera);
                break;
            case Mode.CameraForward:
                //* 카메라 방향으로 Z축 (앞뒤)을 바꿔주기
                transform.forward = mainCam.transform.forward;
                break;
            case Mode.CameraForwardInverted:
                //* 카메라 방향으로 Z축 (앞뒤)을 바꿔주고 반전시키기
                transform.forward = -mainCam.transform.forward;
                break;
            default:

                break;
        }
    }
}
