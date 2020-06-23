using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedSelector : MonoBehaviour
{
  public SeedType currentSeed = SeedType.MuscleMelon;

  private GameObject[] seeds;

  void Start()
  {
    seeds = GameObject.FindGameObjectsWithTag("SelectedSeed");

    // start with first seed selected
    SetActive(SeedType.MuscleMelon);
  }

  void Update()
  {
      
  }

  public void NextSeed()
  {
    int current = (int)currentSeed;
    if (current + 1 <= (int)SeedType.Brainapple)
    {
      currentSeed++;
    }
    else
    {
      currentSeed = SeedType.MuscleMelon;
    }

    SetActive(currentSeed);
  }

  public void PrevSeed()
  {
    int current = (int)currentSeed;
    if (current - 1 >= (int)SeedType.MuscleMelon)
    {
      currentSeed--;
    }
    else
    {
      currentSeed = SeedType.Brainapple;
    }

    SetActive(currentSeed);
  }

  // private
  
  private void SetActive(SeedType type)
  {
    HideAll();
    seeds[(int)type - 1].SetActive(true);
  }

  private void HideAll()
  {
    foreach (var seed in seeds)
    {
      seed.SetActive(false);
    }
  }
}
