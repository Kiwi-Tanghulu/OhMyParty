using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBoxing : MonoBehaviour
{
    private void Start()
    {
        int i = 0;
        object iRef = i; // �ڽ� => ���� ���� �����ϰ� �װ� ����
        i += 10;
        int b = (int)iRef; // ��ڽ� => �ڽ̵� ������ ���������� �ٲ�
        Debug.Log(b);
    }
}
