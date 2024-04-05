using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : Singleton<FishSpawner> 
{
    public GameObject fishPrefab;
    public int initialFishCount = 10;
    public int maxFishCount = 25;
    public float spawnInterval = 2f;
    public float minX = -32f;
    public float maxX = 40f;
    public float minY = -32f;
    public float maxY = 31f;
    public float minScale = 1f;
    public float maxScale = 5f;
    public float chanceForSizeOne = 0.5f;

    private List<GameObject> fishPool = new List<GameObject>();

    void Start()
    {
        InitializeFishPool();
        StartCoroutine(SpawnFishRoutine());
    }

    private void InitializeFishPool()
    {
        for (int i = 0; i < initialFishCount; i++)
        {
            GameObject fish = Instantiate(fishPrefab, GetRandomSpawnPosition(), transform.rotation);
            fish.transform.localScale = Vector3.one * GetRandomScale();
            fishPool.Add(fish);
        }
    }

    private IEnumerator SpawnFishRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            if (fishPool.Count < maxFishCount) SpawnFish();
        }
    }

    public void RemoveFishFromPool(GameObject fishToRemove)
    {
        if (fishPool.Contains(fishToRemove))
        {
            fishPool.Remove(fishToRemove);
        }
    }

    private void SpawnFish()
    {
        if (fishPool.Count >= maxFishCount) return; 
        GameObject newFish = Instantiate(fishPrefab, GetRandomSpawnPosition(), transform.rotation);
        float randomScale = GetRandomScale();
        newFish.transform.localScale = Vector3.one * randomScale;
        fishPool.Add(newFish);
        if (newFish != null) newFish.transform.position = GetRandomSpawnPosition();
    }

    private float GetRandomScale()
    {
        // 50% chance for size 1
        if (Random.value < chanceForSizeOne) return 1f;
        else return Random.Range(minScale, maxScale);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        return new Vector3(randomX, randomY, 0f);
    }
}
