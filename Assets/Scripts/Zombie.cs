using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
  public Animator animator;
  public GameObject target;
  public float speed = 2.5f;

  public AudioSource[] audioData;

  private Mound[] mounds;
  private GameObject player;

  private bool isDying = false;
  private float eatTime;

  private float END_EAT_TIME = 3.0f;
  private float EATING_DISTANCE = 2.0f;
  private float HIT_DELAY = 0.3f;

  void Start()
  {
    audioData = GetComponents<AudioSource>();
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
          Mound mound = target.GetComponent<Mound>();
          if (mound.seedType == SeedType.None)
          {
            // mound was empty on arrival
            animator.ResetTrigger("Eat");
            target = null;
            return;
          }

          if (eatTime >= END_EAT_TIME)
          {
            // eating complete
            target.GetComponent<Mound>().GetEaten();
            target = null;
            eatTime = 0;
            animator.ResetTrigger("Eat");
            animator.SetTrigger("Idle_Left");
          }
          else
          {
            // continue eating
            eatTime += Time.deltaTime;
            animator.SetTrigger("Eat");
          }
        }
        else
        {
          // check for new plants before continuing to chase player
          target = FindMound();
          animator.ResetTrigger("Eat");
        }
      }
      else
      {
        animator.ResetTrigger("Eat");

        // move towards target
        int dir = target.transform.position.x > transform.position.x ? 1 : -1;
        if (dir > 0)
        {
          animator.SetTrigger("Idle_Right");
        }
        else
        {
          animator.SetTrigger("Idle_Left");
        }
        transform.position += new Vector3(dir * speed * Time.deltaTime, 0, 0);
        eatTime = 0;
      }
    }
  }

  public void GetHit()
  {
    if (!isDying)
    {
      animator.SetTrigger("Die");
      StartCoroutine(PlayHitSound());
      isDying = true;
    }
  }

  public void OnDie()
  {
    Destroy(gameObject);
  }

  // private

  private IEnumerator PlayHitSound()
  {
    yield return new WaitForSeconds(HIT_DELAY);
    var rand = Random.Range(0, audioData.Length);
    var source = audioData[rand];
    if (!source.isPlaying)
    {
      source.Play(0);
    }
  }

  private GameObject FindMound()
  {
    // go and eat a random mound
    for (int i = 0; i < mounds.Length - 1; ++i)
    {
      int current = Random.Range(0, mounds.Length);
      if (mounds[current].seedType != SeedType.None)
      {
        return mounds[current].gameObject;
      }
    }

    return null;
  }
}
