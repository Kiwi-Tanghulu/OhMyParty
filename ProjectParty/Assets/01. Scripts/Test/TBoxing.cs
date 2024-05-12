using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBoxing : MonoBehaviour
{
    private void Start()
    {
        int i = 0;
        object iRef = i; // 박싱 => 힙에 값을 복사하고 그걸 참조
        i += 10;
        int b = (int)iRef; // 언박싱 => 박싱된 참조를 값형식으로 바꿈
        Debug.Log(b);
    }
}
