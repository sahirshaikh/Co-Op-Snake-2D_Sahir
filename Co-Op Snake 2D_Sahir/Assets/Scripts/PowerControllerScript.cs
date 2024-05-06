using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerControllerScript : MonoBehaviour
{
    [SerializeField] private float PowerUpSpawnTime;
    [SerializeField] private float PowerLifeTime;
    [SerializeField] private float XpositiveSpawanRange;
    [SerializeField] private float XnegativeSpawanRange;
    [SerializeField] private float YpositiveSpawanRange;
    [SerializeField] private float YnegativeSpwanRange;
    [SerializeField] private GameObject[] powerUps;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawnpowerroutine());       
    }
    IEnumerator Spawnpowerroutine()
    {
        while (true)
        {
            int randomIndex = Random.Range(0, powerUps.Length);
            spawnIteams(powerUps[randomIndex].gameObject);
            yield return new WaitForSeconds(PowerUpSpawnTime);
        }
    }

    void spawnIteams(GameObject gameObject)
    {
        GameObject Object = Instantiate(gameObject);
        Destroy(Object,PowerLifeTime);
    }
}
