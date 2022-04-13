using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public abstract class TrapScript : NetworkBehaviour
{
    //holds the costs for traps
    public abstract int cost { get; set; }
}
