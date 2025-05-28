using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class levelDisplay : MonoBehaviour
{
    public UnityEvent OnScoreChange;

    public int Score;

    public void AddScore(int amount)
    {
        Score += amount;
        OnScoreChange.Invoke();
    }
    public void Update()
    {
        Score = (GameObject.Find("Level Manager").GetComponent<LevelManager>().level);
    }
}
