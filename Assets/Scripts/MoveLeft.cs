using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float bound = -4.5f;
    public float speed = 10f;
    private PlayerController player1;
    private PlayerController player2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject go2 = GameObject.Find("Player 2");
        GameObject go1 = GameObject.Find("Player 1");
        player1 = go1.GetComponent<PlayerController>();
        player2 = go2.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        bool move = true;

        
        if (this.CompareTag("backGround1") && player1.isGameOver)
        {
            move = false;
        }

        if (this.CompareTag("backGround2") && player2.isGameOver)
        {
            move = false;
        }

        if (move)
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
