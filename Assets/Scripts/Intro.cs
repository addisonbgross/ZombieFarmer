using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
  public float showTime;
  public float maxShowTime = 10.0f;

  void Update()
  {
    showTime += Time.deltaTime;
    if (showTime >= maxShowTime)
    {
      gameObject.SetActive(false);
    }
  }
}
