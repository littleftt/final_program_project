using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    public GameObject[] Prefabs;
    public Transform collectibleSpawnPoint;

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), 2, 4);
    }
    void Spawn()
    {
        int prefabsIndex = Random.Range(0, Prefabs.Length);
        Instantiate(
           Prefabs[prefabsIndex],
            collectibleSpawnPoint.position,
           Prefabs[prefabsIndex].transform.rotation
        );
    }
}
