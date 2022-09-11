using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    //create a list with powers
    public enum PowerUpType
    {
        Range,
        Shot
    }

    public PowerUpType powerUpType;
}
