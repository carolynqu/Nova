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
//right now reparable doesn't do anything but change the tag so just check that the tag changes to see if it works
//tag any obstacles with obstacle, dangerous is just for the out of bounds
