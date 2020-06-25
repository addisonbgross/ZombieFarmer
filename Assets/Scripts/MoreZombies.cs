using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreZombies : MonoBehaviour
{
  public AudioSource audioData;
  public Vector3 ogScale;

  private float interval;
  private float intervalLength = 45.0f;
  
  private bool isShowing;
  private float showTime;
  private float showTimeLength = 3.0f;
  private float spawnWaitIncrease = 0.75f;
  private Spawner[] spawners;

  void Start()
  {
    audioData = GetComponent<AudioSource>();
    ogScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
    transform.localScale = Vector3.zero;

    GameObject[] objs = GameObject.FindGameObjectsWithTag("ZombieSpawner");
    spawners = new Spawner[objs.Length];
    for (var i = 0; i < spawners.Length; ++i)
    {
      spawners[i] = objs[i].GetComponent<Spawner>();
      spawners[i].isEnabled = false;
    }
  }

  void Update()
  {
    if (isShowing)
    {
      showTime += Time.deltaTime;
      if (showTime >= showTimeLength)
      {
        showTime = 0;
        interval = 0;
        isShowing = false;
        transform.localScale = Vector3.zero;

        foreach (var spawner in spawners)
        {
          if (!spawner.isEnabled)
          {
            spawner.isEnabled = true;
          }

          spawner.spawnTime *= spawnWaitIncrease;
        }
      }

      return;
    }
    
    if (interval >= intervalLength)
    {
      audioData.Play();
      transform.localScale = ogScale;
      isShowing = true;
    }
    else
    {
      interval += Time.deltaTime;
    }
  }
}
