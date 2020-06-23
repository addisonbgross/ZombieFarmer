using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mound : MonoBehaviour
{
  public float growTime;
  public float endGrowTime;
  public SeedType seedType;

  private float MuscleMelonGrowTime = 30.0f;
  private float SkinBeanGrowTime = 25.0f;
  private float LiverBerryGrowTime = 40.0f;
  private float BrainappleGrowTime = 50.0f;

  void Start()
  {

  }

  void Update()
  {
    // zombie has eaten the mound
    if (seedType == SeedType.None && growTime > 0)
    {
      Debug.Log("MOUND GOT EATEN!");
      growTime = 0;
    }

    if (seedType != SeedType.None)
    {
      // start growing
      if (growTime == 0)
      {
        Debug.Log("START GROWING");
        growTime += Time.deltaTime;
      }

      if (growTime > 0)
      {
        growTime += Time.deltaTime;
        if (growTime >= endGrowTime)
        {
          // growing complete
          Debug.Log("PLANT COMPLETE");
          seedType = SeedType.None;
          growTime = 0;
          // TODO reward
        }
      }
    }
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
}
