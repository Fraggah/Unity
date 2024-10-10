using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isTheLast;  
    public int doorID;   

    public void SetIsTheLast(Door otherDoor, bool value)
    {
        if (otherDoor != null)
        {
            otherDoor.isTheLast = value;
        }
    }
}
