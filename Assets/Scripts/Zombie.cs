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
    Debug.Log("OOF OUCHIE!");
    animator.SetTrigger("Die");
  }

  public void  OnDie()
  {
    Debug.Log("DEAD");
    Destroy(animator);
    Destroy(GetComponent<BoxCollider>());
    Destroy(gameObject);
  }
}
