using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<GameObject> levels;
    public GameObject activeLevel;
    public int levelIndex = 0;
    public GameObject goalUI;

    private void Awake()
    {
        instance = this;
    }

    public void LevelOver()
    {
        goalUI.SetActive(true);
    }

    public void NextLevel()
    {
        Destroy(activeLevel);
        levelIndex++;
        activeLevel = Instantiate(levels[levelIndex]);
        goalUI.SetActive(false);
    }
}
