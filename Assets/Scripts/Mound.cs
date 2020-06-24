using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mound : MonoBehaviour
{
  public float growTime;
  public float endGrowTime;
  public SeedType seedType;
  public bool isReadyToHarvest = false;

  private Animator animator;

  private float MuscleMelonGrowTime = 10.0f;
  private float SkinBeanGrowTime = 20.0f;
  private float LiverBerryGrowTime = 25.0f;
  private float BrainappleGrowTime = 30.0f;

  void Start()
  {
    animator = GetComponent<Animator>();
  }

  void Update()
  {
    if (isReadyToHarvest || seedType == SeedType.None)
    {
      return;
    }

    // start growing
    if (growTime == 0)
    {
      growTime += Time.deltaTime;
    }

    if (growTime > 0)
    {
      growTime += Time.deltaTime;
      if (growTime >= endGrowTime)
      {
        isReadyToHarvest = true;
        return;
      }
    }

    animator.Play("Mound_Musclemelon", 0, growTime / endGrowTime);
  }

  public void AddSeed(SeedType type)
  {
    if (type != SeedType.None && seedType == SeedType.None)
    {
      Debug.Log("PLANTED: " + type.ToString());
      seedType = type;

      if (type == SeedType.MuscleMelon)
      {
        endGrowTime = MuscleMelonGrowTime;
      }
      else if (type == SeedType.SkinBean)
      {
        endGrowTime = SkinBeanGrowTime;
      }
      else if (type == SeedType.LiverBerry)
      {
        endGrowTime = LiverBerryGrowTime;
      }
      else if (type == SeedType.Brainapple)
      {
        endGrowTime = BrainappleGrowTime;
      }
    }
  }

  public SeedType GetHarvested()
  {
    SeedType returnType = seedType;
    seedType = SeedType.None;
    growTime = 0;
    animator.Play("Mound_Musclemelon", 0, 0);
    return returnType;
  }

  public void GetEaten()
  {
    seedType = SeedType.None;
    growTime = 0;
    animator.Play("Mound_Musclemelon", 0, 0);
  }
}
