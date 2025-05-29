using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public bool loadLevel;
    public int level;
    public int sumOfLevels;
    public int neededForSkip = 0;

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
