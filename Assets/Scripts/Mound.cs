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
  private string currentSeedAnimation;

  private float MuscleMelonGrowTime = 10.0f;
  private float SkinBeanGrowTime = 20.0f;
  private float LiverBerryGrowTime = 25.0f;
  private float BrainappleGrowTime = 30.0f;

  void Start()
  {
    animator = GetComponent<Animator>();
    currentSeedAnimation = "MuscleMelon_Plant";
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

    animator.Play(currentSeedAnimation, 0, growTime / endGrowTime);
  }

  public void AddSeed(SeedType type)
  {
    if (type != SeedType.None && seedType == SeedType.None)
    {
      seedType = type;

      if (type == SeedType.MuscleMelon)
      {
        endGrowTime = MuscleMelonGrowTime;
        currentSeedAnimation = "MuscleMelon_Plant";
      }
      else if (type == SeedType.SkinBean)
      {
        endGrowTime = SkinBeanGrowTime;
        currentSeedAnimation = "SkinBean_Plant";
      }
      else if (type == SeedType.LiverBerry)
      {
        endGrowTime = LiverBerryGrowTime;
        currentSeedAnimation = "LiverBerry_Plant";
      }
      else if (type == SeedType.Brainapple)
      {
        endGrowTime = BrainappleGrowTime;
        currentSeedAnimation = "Brainapple_Plant";
      }
    }
  }

  public SeedType GetHarvested()
  {
    SeedType returnType = seedType;
    seedType = SeedType.None;
    growTime = 0;
    isReadyToHarvest = false;
    animator.Play(currentSeedAnimation, 0, 0);
    return returnType;
  }

  public void GetEaten()
  {
    seedType = SeedType.None;
    growTime = 0;
    animator.Play(currentSeedAnimation, 0, 0);
  }
}
