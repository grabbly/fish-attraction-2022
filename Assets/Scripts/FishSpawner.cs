using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] fishPrefabs;
    [SerializeField] private Transform top;
    [SerializeField] private Transform bottom;
    [SerializeField] private Camera camera;

    [SerializeField] private List<GameObject> addedFishes = new List<GameObject>();


    private bool isSpawning = false;

    private void Awake()
    {
        // StartSpawn();
    }

    public void StartSpawn()
    {
        isSpawning = true;
        for (int i = 0; i < Model.FishAmount; i++)
        {
            AddFish(true);
        }
        StopCoroutine(SpawnFlow());
        StartCoroutine(SpawnFlow());
    }

    private void StopSpawn()
    {
        isSpawning = false;
        StopCoroutine(SpawnFlow());
    }

    private IEnumerator SpawnFlow()
    {
        while (isSpawning)
        {
            AddFish(false);
            yield return new WaitForSeconds(Random.Range(.8f, 1.2f));
        }
    }

    public void AddFishToCenter()
    {
        AddFish(true, true);
    }
    private int counter = 0;
    private void AddFish(bool isNewFish, bool isCenter = false)
    {
        if(addedFishes.Count >= Model.FishAmount && !isCenter) return;
        
        var direction = Random.value > .5f ? 1f: -1f;
        GameObject fishGO = Instantiate(fishPrefabs[++counter % fishPrefabs.Length], transform);
        addedFishes.Add(fishGO);
        Fish fish = fishGO.GetComponent<Fish>();
        fish.onOut = FishOut;
        var dist = camera.orthographicSize * camera.aspect;
        float scale = 1;
        if (isCenter)
        {
            
            fishGO.transform.position = new Vector3(
                0, 
                top.transform.position.y -(top.transform.position.y - bottom.transform.position.y)/2f, 
                -1);
            scale = 2;
            fish.Score = 10;
            fish.name = "Payd";
        }
        else 
        {
            if(!isNewFish)
            fishGO.transform.position = new Vector3(
                dist * direction, 
                Random.Range(top.transform.position.y, bottom.transform.position.y), 
                -1);
            else
            fishGO.transform.position = new Vector3(
                Random.Range(-dist, dist),
                Random.Range(top.transform.position.y, bottom.transform.position.y),
                -1);
            scale = .7f + Random.value;
            fish.Score = scale < 1.2f ? 1 : 2;
            
        }
        
        
        fishGO.transform.localScale = new Vector3(scale*direction, scale, scale);
    }

    private void FishOut(GameObject fish)
    {
        if (addedFishes.Contains(fish))
        {
            addedFishes.Remove(fish);
            Destroy(fish);
        }
    }
}
