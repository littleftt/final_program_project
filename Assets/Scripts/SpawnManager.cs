using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform spawnPoint;

    void Start()
    {
        InvokeRepeating(nameof(Spawn), 1, 2);
    }

    void Spawn()
    {
        GameObject p = ObstacleObjectPool.GetInstance().Acquire();
        p.transform.SetLocalPositionAndRotation(transform.position, transform.rotation);
    }
}
