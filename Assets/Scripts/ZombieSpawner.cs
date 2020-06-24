 using UnityEngine;
 using System.Collections;
 
public class ZombieSpawner: MonoBehaviour 
{
  public GameObject prefabToSpawn;
  public float spawnTime;
  public float spawnTimeRandom;
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
      GameObject zombie = Instantiate(prefabToSpawn, transform.position + new Vector3(0, 2, 0), Quaternion.identity);
      Zombie brain = zombie.GetComponent<Zombie>();
      brain.speed = defaultSpeed + Random.Range(0, 8);
      ResetSpawnTimer();
    }
  }

  //Resets the spawn timer with a random offset
  void ResetSpawnTimer()
  {
    spawnTimer = (float)(spawnTime + Random.Range(0, spawnTimeRandom*100)/100.0);
  }
}
