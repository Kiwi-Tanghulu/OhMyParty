using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CharExtexsion
{
    public static bool IsKorean(this char ch)
    {
        if ((0xAC00 <= ch && ch <= 0xD7A3) || (0x3131 <= ch && ch <= 0x318E))
            return true;
        else
            return false;
    }
}
