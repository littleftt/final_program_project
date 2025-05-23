using UnityEngine;

public class HealingCollectible : MonoBehaviour
{
    public float healValue;

    private void Start()
    {
        Destroy(gameObject, 6);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2") || collision.gameObject.CompareTag("Single"))
        {
            Debug.Log("heal picked up");
            collision.gameObject.GetComponent<PlayerController>().AddHeart(healValue);
            Destroy(this.gameObject);
        }
    }
}
