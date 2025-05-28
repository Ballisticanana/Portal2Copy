using NUnit.Framework;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public string SceneName;
    public bool loadLevel;
    public int level;
    public int sumOfLevels;
    public int neededForSkip = 0;
    public Text levNo;

    public void Update()
    {
        if(loadLevel == true)
        {
            SceneManager.LoadScene(level);
        }

        if(neededForSkip == 4 && SceneManager.GetActiveScene().buildIndex < sumOfLevels)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
