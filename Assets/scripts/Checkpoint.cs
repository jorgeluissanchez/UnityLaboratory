using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    //BONUS
    [SerializeField] int addTime = 30;

    //Bonus collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.GlobalTime += addTime;//add Bonus
            Destroy(gameObject, 0.1f); //destroy bonus
        }
    }
}
