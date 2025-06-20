using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private int PlayerScore;
    public Text score;
    private int savedScore;
    // Start is called before the first frame update
    void Start()
    {
        savedScore = PlayerPrefs.GetInt("Score");
        score.text = savedScore.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void playerScoreUpdate(int points)
    {
        PlayerScore += points;
        score.text = PlayerScore.ToString();

    }
}
