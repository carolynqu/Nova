using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reparable : MonoBehaviour
{
    public void FixIt()
    {
        gameObject.tag = "Repaired";
    }
}

//tag any obstacles with obstacle, dangerous is just for the out of bounds
