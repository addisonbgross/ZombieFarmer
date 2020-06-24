using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeedSelector : MonoBehaviour
{
  public SeedType currentSeed = SeedType.MuscleMelon;

  private GameObject[] seeds;
  private int[] numSeeds;

  void Start()
  {
    seeds = GameObject.FindGameObjectsWithTag("SelectedSeed");

    // start with first seed selected
    SetActive(SeedType.MuscleMelon);

    // 4 seed types
    numSeeds = new int[4];

    // start with 2 MuscleMelons
    SetSeedNum(SeedType.MuscleMelon, 2);
  }

  public void AddSeed(SeedType type)
  {
    int amount = int.Parse(seeds[(int)type - 1].transform.Find("Num").GetComponentInChildren<Text>().text);
    SetSeedNum(type, amount + 1);
  }

  public SeedType PlantSeed()
  {
    int amount = int.Parse(seeds[(int)currentSeed - 1].transform.Find("Num").GetComponentInChildren<Text>().text);
    if (amount > 0)
    {
      SetSeedNum(currentSeed, amount - 1);
      return currentSeed;
    }
    
    return SeedType.None;
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

  private void SetSeedNum(SeedType type, int amount)
  {
    seeds[(int)type - 1].transform.Find("Num").GetComponentInChildren<Text>().text = amount.ToString();
  }
}
