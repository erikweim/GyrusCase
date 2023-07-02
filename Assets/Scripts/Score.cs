using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;
    private int score = 0;

    private void Start()
    {
        ChangeScore(0);
    }

    public void ChangeScore(int amount)
    {
        score += amount;
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
        else
        { Debug.Log("Missing Text"); }

    }
}
