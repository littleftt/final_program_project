using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float bound = -10;
    public float speed = 10f;
    public PlayerController player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject go = GameObject.Find("Player");
        player = go.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isGameOver == false)
        {
            transform.Translate(speed * Vector3.left * Time.deltaTime);
        }
        if (this.CompareTag("Obstacle") && transform.position.x < bound)
        {
            // Destroy(gameObject);
            ObstacleObjectPool.GetInstance().Return(gameObject);
        }

    }
}
