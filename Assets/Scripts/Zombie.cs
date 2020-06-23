using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
  public Animator animator;

  void Start()
  {
      
  }

  void Update()
  {
      
  }

  public void GetHit()
  {
    animator.SetTrigger("Die");
  }

  public void  OnDie()
  {
    Destroy(gameObject);
  }
}
