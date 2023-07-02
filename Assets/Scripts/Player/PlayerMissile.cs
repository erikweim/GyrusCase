using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissile : MonoBehaviour
{
    public float speed = 1.0f;
    public float scaleModifier = 1.0f;

    private Transform tf = null;
    private Vector3 start = Vector3.zero;
    private Vector3 direction = Vector3.zero;

    

    private void Awake()
    {
        tf = GetComponent<Transform>();
        start = tf.position;
        direction = -Vector3.Normalize(start);
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, -tf.position);
        tf.SetPositionAndRotation(tf.position, rotation);
    }

    // Update is called once per frame
    private void Update()
    {
       if (Vector3.Dot(start, tf.position) >= 0)
        {
            Vector3 timeAdjMovement = direction * speed * Time.deltaTime;
            tf.SetPositionAndRotation(tf.position + timeAdjMovement, tf.rotation);

            float scale = tf.position.magnitude / start.magnitude * scaleModifier;
            tf.localScale = new Vector3(scale, scale, scale);

        }
        else { Destroy(gameObject); }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy target = collision.GetComponent<Enemy>();
        if (target != null)
        {
            target.Hit();
            Destroy(gameObject);
        }
    }
}
