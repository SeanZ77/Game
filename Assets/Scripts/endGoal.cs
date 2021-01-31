using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endGoal : MonoBehaviour
{
    public PlayerInfo info;
    public GameObject text;

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (info.score >= 80)
            {
                collision.GetComponent<playerManager>().WinGame();
            }
            
            
        }
    }
}
