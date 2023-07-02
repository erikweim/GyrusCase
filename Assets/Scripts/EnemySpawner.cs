using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int numEnemies = 2;
    public float startRadius = 1.0f;
    public GameObject enemy = null;
    public GameObject player = null;
    public List<EnemyWave> waves;
    private float playerRadius = 0.0f;

    void Start()
    {
        if (player != null)
        {
            playerRadius = player.GetComponent<CircularMovement>().radius;
        }
        else { Debug.Log("Missing Player."); }

        if (enemy != null)
        {
            foreach(EnemyWave wave in waves)
            {
                StartCoroutine(SpawnEnemies(wave.number, wave.spawnTime));
            }
        }
        else
        { Debug.Log("Missing Enemy Prefab.");
        }
    }

    private IEnumerator SpawnEnemies(int numEnemies, float timer)
    {
        yield return new WaitForSeconds(timer);

        for (int i = 0; i < numEnemies; i++)
        {
            GameObject clone = Instantiate(enemy, Vector3.zero, Quaternion.identity);
            CircularMovement cm = clone.GetComponent<CircularMovement>();
            if (cm != null)
            {
                //TODO: configurable speed for each wave?!
                cm.StartAt(startRadius, 2 * Mathf.PI / numEnemies * i);
                cm.activateScaling(playerRadius);
            }
            else { Debug.Log("Instantiated Object missing CircularMovement."); }
        }
    }
}
