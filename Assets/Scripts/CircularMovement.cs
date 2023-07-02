using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularMovement : MonoBehaviour
{
    public float radius = 5.0f;
    public float circularSpeed = 1.0f;
    public float radiusSpeed = 1.0f;
    public float radian = 0.0f;
    public float scaleModifier = 1.0f;
    
    private float maxRadius = 0.0f;
    private bool scaling = false;
    private Transform tf = null;

    public void MoveOnCircle( float circularAmount, float radiusAmount)
    {
        //moving along circle, see complex representation r*e^(i*phi)= r*(cos(phi) + i*sin(phi))
        float timeAdjMovement = circularSpeed * Time.deltaTime * circularAmount;
        radian = Mathf.Repeat(radian + timeAdjMovement, 2 * Mathf.PI);

        radius = radius + radiusSpeed * Time.deltaTime * radiusAmount;
        Vector3 newPos =  radius * new Vector3(Mathf.Cos(radian), Mathf.Sin(radian), 0.0f);

        //LookRotation(Z-Axis, Y Axis) Z-Axis should point forward and Y-Axis to target
        Quaternion newRot = Quaternion.LookRotation(Vector3.forward, newPos);

        if (tf != null)
        {
            tf.SetLocalPositionAndRotation(newPos, newRot);

            if (scaling)
            {
                float scale = tf.position.magnitude / maxRadius * scaleModifier;
                tf.localScale = new Vector3(scale, scale, scale);
            }
        }
        else { Debug.Log("Can't find Transform."); }
    }

    public void StartAt(float radius, float radian)
    {
        this.radius = radius;
        this.radian = radian;
        MoveOnCircle(0.0f, 0.0f);
    }

    public void activateScaling(float maxRadius)
    {
        if (maxRadius > 0.0f)
        {
            scaling = true;
            this.maxRadius = maxRadius;
        }
        else { Debug.Log("Invalid Radius."); }
    }

    private void Awake()
    {
        tf = GetComponent<Transform>();
    }
}
