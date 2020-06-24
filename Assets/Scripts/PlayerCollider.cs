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

    if (other.tag == "Seed")
    {
      Seed seed = other.GetComponent<Seed>();
      if (seed)
      {
        Parent.seedSelector.AddSeed(seed.type);
        seed.PickUp();
      }
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
