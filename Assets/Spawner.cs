using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] public GameObject[] spawnPoints;
    [SerializeField] public GameObject[] spawnablePrefabs;
    [SerializeField] private float respawnTime = 5f;
    [SerializeField] private int maxEntities = 5;
    [SerializeField] private string spawnTag = "Enemy";
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCoroutine());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator SpawnCoroutine()
    {
        while(true){
            SpawnEnemies();
            yield return new WaitForSeconds(respawnTime);
        }
    }

    void SpawnEnemies(){
        var enemies = GameObject.FindGameObjectsWithTag(spawnTag);
        if(enemies.Length >= maxEntities) return;
        GameObject instance = Instantiate(GetRandomSpawnable());
        instance.transform.position = GetRandomSpawnpoint().transform.position;
        
    }

    private GameObject GetRandomSpawnpoint(){
        return spawnPoints[Random.Range(0,spawnPoints.Length)];
    }

    private GameObject GetRandomSpawnable(){
        return spawnablePrefabs[Random.Range(0,spawnablePrefabs.Length)];
    }
}
