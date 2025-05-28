using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class levelDisplayDisplay : MonoBehaviour
{
    private TMP_Text _scoreText;
    private void Awake()
    {
        _scoreText = GetComponent<TMP_Text>();
    }

    public void Update()
    {
        
        _scoreText.text = ("" + GameObject.Find("Level Manager").GetComponent<LevelManager>().level);
    }
}
