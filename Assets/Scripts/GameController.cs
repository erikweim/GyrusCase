using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameOverDisplay gameOverScript = null;
    public ScoreDisplay scoreDisplay = null;
    public LifesDisplay lifesDisplay = null;

    private int score = 0;

  public void GameOver()
    {
        scoreDisplay.HideDisplay();
        lifesDisplay.HideDisplay();
        gameOverScript.Setup(score);
    }

   public void ChangeScore(int amount)
    {
        score += amount;
        scoreDisplay.DisplayScore(score);
    }
}
