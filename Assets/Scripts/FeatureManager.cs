using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FeatureManager : MonoBehaviour
{
    public PlayerController player;
    private bool isUpsideDown = false;
    private bool isFeatureActive = false;

    public GameObject upperPlatformPrefab;
    private List<GameObject> spawnedPlatforms = new List<GameObject>();

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        }

        InvokeRepeating(nameof(ActivateRandomFeature), 5f, 10f);
    }

    void ActivateRandomFeature()
    {
        if (isFeatureActive) return;

        int rand = Random.Range(0, 3);
        isFeatureActive = true;

        switch (rand)
        {
            case 0:
                StartCoroutine(UpsideDownRoutine());
                break;
            case 1:
                StartCoroutine(DoublePlatformRoutine());
                break;
            case 2:
                StartCoroutine(SmallFormRoutine());
                break;
        }
    }

    IEnumerator UpsideDownRoutine()
    {
        isUpsideDown = true;
        Camera.main.transform.rotation = Quaternion.Euler(0, 0, 180);

        yield return new WaitForSeconds(10f);

        Camera.main.transform.rotation = Quaternion.identity;
        isUpsideDown = false;

        isFeatureActive = false;
    }

    IEnumerator DoublePlatformRoutine()
    {
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("PlatformSpawnPoint");

        foreach (GameObject point in spawnPoints)
        {
            GameObject platform = Instantiate(upperPlatformPrefab, point.transform.position, Quaternion.identity);
            spawnedPlatforms.Add(platform);
        }

        yield return new WaitForSeconds(10f);

        foreach (GameObject platform in spawnedPlatforms)
        {
            Destroy(platform);
        }
        spawnedPlatforms.Clear();

        isFeatureActive = false;
    }

    IEnumerator SmallFormRoutine()
    {
        player.ChangeToSmallForm();

        yield return new WaitForSeconds(10f);

        player.RevertToNormalForm();

        isFeatureActive = false;
    }
}