using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBound : MonoBehaviour
{
    public bool OutOfBoundsX(float margin = 0.5f)
    {
        Vector3 pos = transform.position;
        if (pos.x < -Boarder.xBoarder - margin || pos.x > Boarder.xBoarder + margin)
        {
            return true;
        }
        return false;
    }
    public bool OutOfBounds(float margin = 0.5f)
    {
        Vector3 pos = transform.position;
        if (pos.y < -Boarder.yBoarder - margin || pos.y > Boarder.yBoarder + margin)
        {
            return true;
        }
        return false;
    }
}
