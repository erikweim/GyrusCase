using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingComponent : MonoBehaviour
{
    public GameObject missile = null;
    public float coolDownDuration = 1.0f;

    private bool active = true;

   public void Shoot(Vector3 position, Quaternion rotation)
    {
        if (missile != null)
        {
            if (active)
            {
                Instantiate(missile, position, rotation);
                active = false;
                StartCoroutine(CoolDown());
            }
        }
        else
        {Debug.Log("Missing Missile GameObject");}
        
    }

    public IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(coolDownDuration);
        active = true;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
