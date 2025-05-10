using System.Collections.Generic;
using UnityEngine;

public class ObstacleObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private int initialPoolSize = 10;

    private readonly List<GameObject> obstaclePool = new();

    private static ObstacleObjectPool staticInstance;

    private void Awake()
    {
        if (staticInstance != null)
        {
            Destroy(gameObject);
            return;
        }
        staticInstance = this;
    }

    public static ObstacleObjectPool GetInstance()
    {
        return staticInstance;
    }

    private void Start()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            CreateNewProjectile();
        }
    }

    private void CreateNewProjectile()
    {
        GameObject p = Instantiate(obstaclePrefab);
        p.SetActive(false);
        obstaclePool.Add(p);
    }

    public GameObject Acquire()
    {
        if (obstaclePool.Count == 0)
        {
            CreateNewProjectile();
        }
        GameObject p = obstaclePool[0];
        obstaclePool.Remove(p);
        //projectilePool.RemoveAt(0);
        p.SetActive(true);
        return p;
    }

    public void Return(GameObject projectile)
    {
        obstaclePool.Add(projectile);
        projectile.SetActive(false);
    }
}
