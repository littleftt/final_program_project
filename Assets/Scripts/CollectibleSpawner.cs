using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    public GameObject healpickupPrefab;
    public Transform collectibleSpawnPoint;

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), 2, 4);
    }
    void Spawn()
    {
        Instantiate(
           healpickupPrefab,
            collectibleSpawnPoint.position,
           healpickupPrefab.transform.rotation
        );
    }
}
