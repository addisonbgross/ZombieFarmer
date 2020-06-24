using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner: MonoBehaviour 
{
  public GameObject[] prefabToSpawn;
  public float spawnTime;
  public float spawnTimeRandom;
  public float spawnRange;
  public float defaultSpeed = 2.5f;

  private float spawnTimer;

  void Start () 
  {
    ResetSpawnTimer();
  }

  void Update () 
  {
    spawnTimer -= Time.deltaTime;
    if (spawnTimer <= 0.0f)
    {
      GameObject obj =
        Instantiate(
          prefabToSpawn[Random.Range(0, prefabToSpawn.Length - 1)],
          transform.position + new Vector3(0, 2, 0) + new Vector3(Random.Range(0, spawnRange), 0, 0),
          Quaternion.identity
        );

      if (obj.tag == "Enemy")
      {
        Zombie zombie = obj.GetComponent<Zombie>();
        zombie.speed = defaultSpeed + Random.Range(0, 8);
      }

      ResetSpawnTimer();
    }
  }

  //Resets the spawn timer with a random offset
  void ResetSpawnTimer()
  {
    spawnTimer = (float)(spawnTime + Random.Range(0, spawnTimeRandom*100)/100.0);
  }
}
