using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeadScreen : MonoBehaviour
{
  public Vector3 ogScale;
  public bool isDead;
  public float deadTime;
  public float maxDeadTime = 3.0f;

  void Start()
  {
    ogScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
    transform.localScale = Vector3.zero;
  }

  void Update()
  {
    if (isDead)
    {
      deadTime += Time.deltaTime;
      if (deadTime >= maxDeadTime)
      {
        SceneManager.LoadScene(0);
      }
    }
      
  }

  public void Show()
  {
    transform.localScale = ogScale;
    GameObject.FindWithTag("Player").GetComponent<PlayerController>().enabled = false;
    isDead = true;
  }
}
