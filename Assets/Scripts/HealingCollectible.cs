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
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("picked");
            collision.gameObject.GetComponent<PlayerController>().AddHeart(healValue);
            Destroy(this.gameObject);
        }
    }
}
