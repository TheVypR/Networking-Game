using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TrapScript : MonoBehaviour
{
    //holds the costs for traps
    public abstract int cost { get; set; }
}
