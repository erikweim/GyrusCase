using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int lifes = 4;
    public Lifes lifeScreen = null;

    private void Awake()
    {
        if (lifeScreen != null)
        { lifeScreen.SetLife(lifes); }
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
        //TODO Trigger Game Over
        }

        if (lifeScreen != null)
        { lifeScreen.SetLife(lifes); }
        else { Debug.Log("Missing ScreenText"); }
    }
}
