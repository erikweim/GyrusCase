using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class LifesDisplay : MonoBehaviour
{
    public Text lifeText;

    public void DisplayLife(int amount)
    {
        if (lifeText != null)
        {
            lifeText.text = amount.ToString();
        }
        else
        { Debug.Log("Missing Text"); }

    }

    public void HideDisplay()
    {
        gameObject.SetActive(false);
    }
    
}
