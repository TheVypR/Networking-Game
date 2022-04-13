using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move
{
    public Vector2 axis;
    public int index;

    public Move(Vector2 axis, int index)
    {
        this.index = index;
        this.axis = axis;
    }

    public Move()
    {

    }
}
