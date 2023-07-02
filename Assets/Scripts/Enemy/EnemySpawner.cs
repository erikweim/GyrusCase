using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int numEnemies = 2;
    public float startRadius = 1.0f;
    public GameObject enemy = null;
    public GameObject player = null;
    public List<EnemyWave> waves;
    private bool lastSpawned = false;
    private List<GameObject> enemies = new List<GameObject>();
    public GameController gc = null;
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
            int numberWaves = waves.Count();
            for ( int i = 0; i < numberWaves; i++)
            {
                EnemyWave wave = waves[i];
                StartCoroutine(SpawnEnemies(wave.number, wave.spawnTime, i==numberWaves-1));
            }
        }
        else
        { Debug.Log("Missing Enemy Prefab.");
        }
    }

    private IEnumerator SpawnEnemies(int numEnemies, float timer, bool last)
    {
        yield return new WaitForSeconds(timer);

        for (int i = 0; i < numEnemies; i++)
        {
            GameObject clone = Instantiate(enemy, Vector3.zero, Quaternion.identity);
            enemies.Add(clone);

            SetupMovememt(clone, 2 * Mathf.PI / numEnemies * i);
            SetupReferences(clone);
        }

        if (last)
        { lastSpawned = true; }

    }

    private void SetupMovememt(GameObject clone, float placeCircle)
    {
        //Setup for Circular Movement
        CircularMovement cm = clone.GetComponent<CircularMovement>();
        if (cm != null)
        {
            //TODO: configurable speed for each wave?!
            cm.StartAt(startRadius, placeCircle);
            cm.activateScaling(playerRadius);
        }
        else { Debug.Log("Instantiated Object missing CircularMovement."); }
    }

    private void SetupReferences(GameObject clone)
    {
        //Setup for Score counting
        Enemy enemy = clone.GetComponent<Enemy>();
        if (enemy != null)
        { enemy.gc = gc; }
        else { Debug.Log("Instantiated Object missing Target."); }

        enemy.spawner = this;
    }

    public void EnemyDestroyed( GameObject enemy)
    {
        enemies.Remove(enemy);
        if(enemies.Count ==0 & lastSpawned)
        {
            gc.GameOver();
            Debug.Log("GameOber");
        }
    }
}
