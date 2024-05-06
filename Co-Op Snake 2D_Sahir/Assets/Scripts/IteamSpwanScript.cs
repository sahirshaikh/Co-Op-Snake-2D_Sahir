using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IteamSpwanScript : MonoBehaviour
{
    [SerializeField] private GameObject MassGainer;
    [SerializeField] private GameObject MassBurner;

    [SerializeField] private float foodLifeTime;
    [SerializeField] private float foodSpawnTime;
    [SerializeField] private float XpositiveSpawanRange;
    [SerializeField] private float XnegativeSpawanRange;
    [SerializeField] private float YpositiveSpawanRange;
    [SerializeField] private float YnegativeSpwanRange;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnFoodroutine());        
    }

    private IEnumerator SpawnFoodroutine()
    {
        while (true)
        {           
        GameObject foodPrefab = Random.value < 0.5f ? MassBurner : MassGainer;
        SpawnFood(foodPrefab);
        yield return new WaitForSeconds(foodSpawnTime);
        }
    }

    private void SpawnFood(GameObject prefab)
    {
        Vector2 spawnposition = new Vector2(Random.Range(XpositiveSpawanRange, XnegativeSpawanRange), Random.Range(YpositiveSpawanRange, YnegativeSpwanRange));
        GameObject food = Instantiate(prefab, spawnposition, Quaternion.identity);
        Destroy(food,foodLifeTime);
    }
}
