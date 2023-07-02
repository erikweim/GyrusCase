using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private CircularMovement cm = null;
    void Awake()
    {
        cm = GetComponent<CircularMovement>();
    }

    void Update()
    {
        if (cm != null)
        {
            cm.MoveOnCircle(1.0f, 1.0f);
        }
        else { Debug.Log("Missing CircularMovement."); }
    }
}
