using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
  PlayerController Parent;

  void Start()
  {
    Parent = transform.parent.GetComponent<PlayerController>(); 
  }

  void OnTriggerEnter(Collider other)
  {
    if (other.tag == "Enemy" || other.tag == "Mound")
    {
      Parent.AddOverlap(other);
    }
  }

  void OnTriggerExit(Collider other)
  {
    if (other.tag == "Enemy" || other.tag == "Mound")
    {
      Parent.RemoveOverlap(other);
    }
  }
}
