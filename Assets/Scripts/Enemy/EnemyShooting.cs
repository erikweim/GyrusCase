using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject missile = null;
    public float randomMin = 2.0f;
    public float randomMax = 3.0f;
    public float fixInterval = 1.0f;
    public bool random = true;

    public void Shoot(Vector3 position, Quaternion rotation)
    {
        if (missile != null)
        {
            Instantiate(missile, position, rotation);
        }
        else
        { Debug.Log("Missing Missile GameObject"); }

    }
    private IEnumerator AutoShoot()
    {
        yield return new WaitForSeconds(random ? Random.Range(2.0f, 3.0f) : fixInterval);
        Shoot(gameObject.transform.position, gameObject.transform.rotation);
        StartCoroutine(AutoShoot());
    }

    private void Awake()
    {
        StartCoroutine(AutoShoot());
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
