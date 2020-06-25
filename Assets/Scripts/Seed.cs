using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
  public SeedType type = SeedType.MuscleMelon;
  public AudioSource audioData;

  private Animator animator;
  private bool isPickedUp;

  void Start()
  {
    audioData = GetComponent<AudioSource>();
    animator = GetComponent<Animator>();
    animator.enabled = false;
  }

  void Update()
  {
    if (isPickedUp)
    {
      transform.position += new Vector3(0, 0.25f, 0);
    }
  }

  public SeedType PickUp()
  {
    if (isPickedUp)
    {
      return SeedType.None;
    }

    audioData.Play(0);
    animator.enabled = true;
    isPickedUp = true;
    return type;
  }

  public void OnDone()
  {
    Destroy(gameObject);
  }
}
