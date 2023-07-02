using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int lifes = 4;
    public LifesDisplay lifeScreen = null;
    public GameController gc = null;

    private void Awake()
    {
        if (lifeScreen != null)
        { lifeScreen.DisplayLife(lifes); }
        else { Debug.Log("Missing ScreenText"); }

    }

    public void Hit()
    {
        if (lifes > 1)
        {
            lifes--;
        }
        else
        { lifes = 0;
            gc.GameOver();
            Destroy(gameObject);
        }

        if (lifeScreen != null)
        { lifeScreen.DisplayLife(lifes); }
        else { Debug.Log("Missing ScreenText"); }
    }
}
