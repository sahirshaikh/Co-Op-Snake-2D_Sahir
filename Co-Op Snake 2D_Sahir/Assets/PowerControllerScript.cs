using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerControllerScript : MonoBehaviour
{

    // public GameObject shield;
    // public GameObject ScoreMultiplier;
    // public GameObject SpeedBooster;

    public float PowerUpSpawnTime;
    public float PowerLifeTime;
    public float XpositiveSpawanRange;
    public float XnegativeSpawanRange;
    public float YpositiveSpawanRange;
    public float YnegativeSpwanRange;
    public GameObject[] powerUps;
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
