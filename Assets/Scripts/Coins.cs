using UnityEngine;

public class Coins : MonoBehaviour
{
    public float coinValue;
    
    void Start()
    {
        Destroy(gameObject, 6);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2") || collision.gameObject.CompareTag("Single"))
        {
            Debug.Log("coins picked up");
            collision.gameObject.GetComponent<PlayerController>().AddCoins(coinValue);
            Destroy(this.gameObject);
        }
    }
}
