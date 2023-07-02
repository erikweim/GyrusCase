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
    public GameController gc = null;

    private bool lastSpawned = false;
    private List<GameObject> enemies = new List<GameObject>();
    private float playerRadius = 0.0f;
    private float scale = 1.0f;

    public void EnemyDestroyed(GameObject enemy)
    {
        enemies.Remove(enemy);
        if (enemies.Count == 0 & lastSpawned)
        {
            //No more Enemies
            gc.GameOver();
        }
    }

    private void Start()
    {
        if (player != null)
        {
            playerRadius = player.GetComponent<CircularMovement>().radius;
        }
        else { Debug.Log("Missing Player."); }

        if (enemy != null)
        {
            int numberWaves = waves.Count() - 1;
            StartCoroutine(SpawnEnemies(waves[0].number, waves[0].spawnTime, numberWaves, 0));
        }
        else
        {
            Debug.Log("Missing Enemy Prefab.");
        }

        scale = startRadius / playerRadius;
    }

    private IEnumerator SpawnEnemies(int numEnemies, float timer, int numberWaves, int currentNumber)
    {
        yield return new WaitForSeconds(timer);

        for (int currentEnemy = 0; currentEnemy < numEnemies; currentEnemy++)
        {
            GameObject clone = Instantiate(enemy, Vector3.zero, Quaternion.identity);
            //sometimes popping up to big
            clone.transform.localScale = new Vector3(scale, scale, scale);

            enemies.Add(clone);

            SetupMovememt(clone, 2 * Mathf.PI / numEnemies * currentEnemy);
            SetupReferences(clone);
        }

        if (currentNumber == numberWaves)
        { lastSpawned = true; }
        else
        {
            currentNumber++;
            StartCoroutine(SpawnEnemies(waves[currentNumber].number, waves[currentNumber].spawnTime, numberWaves, currentNumber));
        }

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

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
