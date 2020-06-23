using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
  public Animator animator;
  public Mound target;

  private bool isDying = false;
  private float eatTime;

  private float SPEED = 2.5f;
  private float END_EAT_TIME = 3.0f;
  private float EATING_DISTANCE = 2.0f;

  void Start()
  {

  }

  void Update()
  {
    if (isDying)
    {
      return;
    }

    if (!target)
    {
      // find random mound
      Mound[] mounds = FindObjectsOfType<Mound>();
      int rando = Random.Range(0, mounds.Length - 1);
      if (mounds[rando] && mounds[rando].seedType != SeedType.None)
      {
        target = mounds[rando];
      }
    }
    else
    {
      float distance = Vector3.Distance(target.transform.position, transform.position);
      if (distance <= EATING_DISTANCE)
      {
        if (eatTime >= END_EAT_TIME)
        {
          // eating complete
          target.seedType = SeedType.None;
          target = null;
          eatTime = 0;
          Debug.Log("EATING COMPLETE");
        }
        else
        {
          // continue eating
          eatTime += Time.deltaTime;
        }
      }
      else
      {
        // move towards target
        int dir = target.transform.position.x > transform.position.x ? 1 : -1;
        transform.position += new Vector3(dir * SPEED * Time.deltaTime, 0, 0);
      }
    }
  }

  public void GetHit()
  {
    animator.SetTrigger("Die");
    isDying = true;
  }

  public void  OnDie()
  {
    Destroy(gameObject);
  }
}
