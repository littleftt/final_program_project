using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Small : MonoBehaviour
{
    public PlayerController player;
    

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2"))
        {
            player = collision.gameObject.GetComponent<PlayerController>();
            if (player == null) return;
            player.SmallForm();
            Destroy(gameObject);
        }
    }
}
