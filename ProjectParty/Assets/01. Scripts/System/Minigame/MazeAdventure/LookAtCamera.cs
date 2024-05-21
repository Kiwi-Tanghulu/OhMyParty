using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private enum Mode
    {
        LookAt,
        LookAtInverted, //* ���� ���� ����
        CameraForward,
        CameraForwardInverted, //* ���� ���� ����
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
                //* ī�޶� ������ �˾Ƴ��� �� ���� ��ŭ �����༭ ������Ű��
                Vector3 dirFromCamera = transform.position - mainCam.transform.position;
                transform.LookAt(transform.position + dirFromCamera);
                break;
            case Mode.CameraForward:
                //* ī�޶� �������� Z�� (�յ�)�� �ٲ��ֱ�
                transform.forward = mainCam.transform.forward;
                break;
            case Mode.CameraForwardInverted:
                //* ī�޶� �������� Z�� (�յ�)�� �ٲ��ְ� ������Ű��
                transform.forward = -mainCam.transform.forward;
                break;
            default:

                break;
        }
    }
}
