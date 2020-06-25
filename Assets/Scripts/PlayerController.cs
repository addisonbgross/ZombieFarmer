using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
  public float speed;
  public bool isFacingLeft = true;

  public Animator animator;
  public LayerMask layerMask;
  public HashSet<Collider> Overlaps;
  public SeedSelector seedSelector;

  public Dictionary<SeedType, int> rewards;
  public AudioSource audioData;

  void Start()
  {
    Overlaps = new HashSet<Collider>();
    seedSelector = transform.Find("SeedSelector").GetComponent<SeedSelector>();
    rewards = new Dictionary<SeedType, int>()
    {
      { SeedType.None, 0 },
      { SeedType.MuscleMelon, 100 },
      { SeedType.SkinBean, 500 },
      { SeedType.LiverBerry, 2000 },
      { SeedType.Brainapple, 7000 },
    };
    audioData = GetComponent<AudioSource>();
  }

  void Update()
  {
    // seed selection
    if (Input.GetKeyDown(KeyCode.U))
    {
      seedSelector.PrevSeed();
    }
    else if (Input.GetKeyDown(KeyCode.I))
    {
      seedSelector.NextSeed();
    }

    // movement
    if (Input.GetKey(KeyCode.A))
    {
      animator.SetTrigger("Walk_Left");
      isFacingLeft = true;
      speed = -20.0f;
    }
    else if (Input.GetKey(KeyCode.D))
    {
      animator.SetTrigger("Walk_Right");
      isFacingLeft = false;
      speed = 20.0f;
    }
    else if (!Input.anyKey)
    {
      animator.SetTrigger("Idle");
      speed = 0;
    }

    // attack or interact
    if (Input.GetKeyDown(KeyCode.J))
    {
      if (isFacingLeft)
      {
        animator.SetTrigger("Attack_Left");
      }
      else
      {
        animator.SetTrigger("Attack_Right");
      }

      PerformAttack();
    }
    else if (Input.GetKeyDown(KeyCode.K))
    {
      PerformAction();
    }

    transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
  }

  public void AddOverlap(Collider obj)
  {
    Overlaps.Add(obj);
  }

  public void RemoveOverlap(Collider obj)
  {
    Overlaps.Remove(obj);
  }

  public void StopTheme()
  {
    audioData.Stop();
  }

  // private

  private void PerformAttack()
  {
    if (Overlaps.Count > 0)
    {
      foreach (Collider overlap in Overlaps)
      {
        if (overlap)
        {
          if (overlap.tag == "Enemy")
          {
            Zombie zombie = overlap.GetComponent<Zombie>();
            if (zombie)
            {
              zombie.GetHit();
            }
          }
        }
      }
    }
  }

  private void PerformAction()
  {
    if (Overlaps.Count > 0)
    {
      foreach (Collider overlap in Overlaps)
      {
        if (overlap && overlap.tag == "Mound")
        {
          HandleMoundAction(overlap);
        }
      }
    }
  }

  private void HandleMoundAction(Collider overlap)
  {
    Mound mound = overlap.GetComponent<Mound>();
    if (mound)
    {
      if (mound.isReadyToHarvest)
      {
        animator.SetTrigger("Action");
        SeedType harvested = mound.GetHarvested();              
        Debug.Log(harvested);

        Text scoreUI = GameObject.FindWithTag("Score").GetComponentInChildren<Text>();
        scoreUI.text = (int.Parse(scoreUI.text) + rewards[harvested]).ToString();

        // prevent death
        seedSelector.ResetHealth();
        return;
      }

      if (mound.seedType != SeedType.None && mound.growTime > 0)
      {
        // seed is busy growing
        return;
      }

      SeedType plantedSeed = seedSelector.PlantSeed();
      if (plantedSeed != SeedType.None)
      {
        // plant new seed
        animator.SetTrigger("Action");
        mound.AddSeed(plantedSeed);
      }
    }
  }
}
