using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    public Text scoreText;

    public void DisplayScore(int score)
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
        else
        { Debug.Log("Missing Text"); }
    }

    public void HideDisplay()
    {
        gameObject.SetActive(false);
    }

    private void Start()
    {
        DisplayScore(0);
    }
}
