using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class UpSideDown : MonoBehaviour
{
    public PlayerController player;
    
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2") || collision.gameObject.CompareTag("Single"))
        {
            player = collision.gameObject.GetComponent<PlayerController>();
            if (player == null) return;
            player.UpsideDown();
            Destroy(gameObject);
        }
    }
    
}
