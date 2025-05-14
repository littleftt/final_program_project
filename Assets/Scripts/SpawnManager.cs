using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public PlayerController player;
    public Transform spawnPoint;

    void Start()
    {
        GameObject playerObj = GameObject.FindWithTag("Player1");
        player = playerObj.GetComponent<PlayerController>();
        InvokeRepeating(nameof(Spawn), 1, 2);
    }

    private void Update()
    {
        if (player.isGameOver == true)
        {
            CancelInvoke(nameof(Spawn));
        }
    }

    void Spawn()
    {
        //Instantiate(
        //    obstaclePrefab,
        //    spawnPoint.position,
        //    obstaclePrefab.transform.rotation
        //);
        GameObject p = ObstacleObjectPool.GetInstance().Acquire();
        p.transform.SetLocalPositionAndRotation(transform.position, transform.rotation);
    }
}
