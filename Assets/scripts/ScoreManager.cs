using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private int PlayerScore;
    public Text score;
    private int savedScore;

    public Transform player;
    public int Score = 0;
    public float scoreMultiplier = 1.0f;

    private Vector3 lastposition;
    private float totalDistance;
    public Text scoreText;  

    // Start is called before the first frame update
    void Start()
    {
        lastposition = new Vector3(player.position.x, 0, player.position.z);
        savedScore = PlayerPrefs.GetInt("Score");
        score.text = savedScore.ToString();
        
    }

    // Update is called once per frame
   

    void Update()
    {
        Vector3 currentPos = new Vector3(player.position.x, 0, player.position.z);
        float stepDistance = Vector3.Distance(currentPos, lastposition);

        totalDistance += stepDistance;
        lastposition = currentPos;


        Score = Mathf.FloorToInt(totalDistance * scoreMultiplier);
        scoreText.text = Score.ToString();
    }

    public void playerScoreUpdate(int points)
    {
        PlayerScore += points;
        score.text = PlayerScore.ToString();

    }
}
