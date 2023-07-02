using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Score score = null;
    public Transform tf = null;
    public EnemySpawner spawner = null;
    private Renderer renderer = null;

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (!renderer.isVisible) {
            Destroy(gameObject);
        }
    }
    public void Hit()
    {

        if (tf != null)
        {
            if (score != null)
            {
                int scoreDelta = Mathf.RoundToInt(1/tf.position.magnitude*100);
                score.ChangeScore(scoreDelta);
                Destroy(gameObject);
            }
            else { Debug.Log("No Score Component"); }
        }
        else { Debug.Log("Missing Transform."); }
    }

    private void OnDestroy()
    {
        spawner.EnemyDestroyed(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player target = collision.GetComponent<Player>();
        if (target != null)
        {
            target.Hit();
        }
    }
}
