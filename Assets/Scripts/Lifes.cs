using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Lifes : MonoBehaviour
{
    public Text lifeText;
    public int lifes = 0;

    public void SetLife(int amount)
    {
        lifes = amount;
        if (lifeText != null)
        {
            lifeText.text = lifes.ToString();
        }
        else
        { Debug.Log("Missing Text"); }

    }
}
