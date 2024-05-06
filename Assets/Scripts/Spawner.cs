using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnableObject
    { 
        public GameObject prefab;
        [Range(0f, 1f)] //Makes a slider in Unity
        public float spawnChance;
    }

    public SpawnableObject[] objects;

    //Sets min and max spawn rate
    public float minSpawnRate = 1f;
    public float maxSpawnRate = 2f;

    private void OnEnable()
    {
        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Spawn()
    {
        float spawnChance = Random.value;

        foreach (var obj in objects)
        {
            if (spawnChance < obj.spawnChance)
            {
                GameObject objectPrefab =  Instantiate(obj.prefab);
                objectPrefab.transform.position += transform.position;
                break;
            }

            spawnChance -= obj.spawnChance;
        }
        
        //Spawns another object with a new spawn rate
        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }
}
