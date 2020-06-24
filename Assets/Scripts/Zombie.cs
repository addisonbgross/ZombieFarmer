using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
  public Animator animator;
  public GameObject target;
  public float speed = 2.5f;

  private Mound[] mounds;
  private GameObject player;

  private bool isDying = false;
  private float eatTime;

  private float END_EAT_TIME = 3.0f;
  private float EATING_DISTANCE = 2.0f;

  void Start()
  {
    mounds = FindObjectsOfType<Mound>();
    player = GameObject.FindWithTag("Player");
  }

  void Update()
  {
    if (isDying)
    {
      return;
    }

    if (!target)
    {
      target = FindMound();
      if (!target)
      {
        target = player;
      }
    }
    else
    {
      float distance = Vector3.Distance(target.transform.position, transform.position);
      if (distance <= EATING_DISTANCE)
      {
        if (target.tag == "Mound")
        {
          if (eatTime >= END_EAT_TIME)
          {
            // eating complete
            target.GetComponent<Mound>().GetEaten();
            target = null;
            eatTime = 0;
          }
          else
          {
            // continue eating
            eatTime += Time.deltaTime;
          }
        }
        else
        {
          // check for new plants before continuing to chase player
          GameObject newMound = FindMound();
          if (newMound)
          {
            target = newMound;
          }
        }
      }
      else
      {
        // move towards target
        int dir = target.transform.position.x > transform.position.x ? 1 : -1;
        transform.position += new Vector3(dir * speed * Time.deltaTime, 0, 0);
        eatTime = 0;
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

  // private

  private GameObject FindMound()
  {
    foreach (Mound mound in mounds)
    {
      if (mound.seedType != SeedType.None)
      {
        return mound.gameObject;
      }
    }

    return null;
  }
}
