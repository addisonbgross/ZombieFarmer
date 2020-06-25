using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeedSelector : MonoBehaviour
{
  public SeedType currentSeed = SeedType.MuscleMelon;

  private GameObject[] seeds;
  private int[] numSeeds;

  private GameObject healthUI;
  private Transform ogHealth;
  private float currentHealth;
  private float healthDecay = 0.0005f;

  private GameObject deadScreen;
  private PlayerController Parent;

  void Start()
  {
    seeds = GameObject.FindGameObjectsWithTag("SelectedSeed");

    healthUI = GameObject.FindWithTag("Health");

    GameObject empty = new GameObject();
    ogHealth = empty.transform;
    ogHealth.parent = healthUI.transform.parent;
    ogHealth.localScale = healthUI.transform.localScale;
    ogHealth.position = healthUI.transform.position;

    deadScreen = GameObject.FindWithTag("DeadScreen");
    Parent = transform.parent.GetComponent<PlayerController>(); 

    // start with first seed selected
    SetActive(SeedType.MuscleMelon);

    // 4 seed types
    numSeeds = new int[4];

    // start with 2 MuscleMelons
    SetSeedNum(SeedType.MuscleMelon, 2);
  }

  void Update()
  {
    Vector3 scale = healthUI.transform.localScale;
    Vector3 position = healthUI.transform.position;
    if (scale.x <= 0)
    {
      deadScreen.GetComponent<DeadScreen>().Show();
      Parent.StopTheme();
      return;
    }

    healthUI.transform.localScale = new Vector3(
        scale.x - healthDecay,
        scale.y,
        scale.z
      );

    healthUI.transform.position = new Vector3(
        position.x - (healthDecay * 4f),
        position.y,
        position.z
      );
  }

  public void ResetHealth()
  {
    healthUI.transform.localScale = ogHealth.localScale;
    healthUI.transform.position = ogHealth.position;
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
