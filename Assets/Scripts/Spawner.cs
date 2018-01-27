using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject EnemyPrefab;
    public Direction direction;

    public enum Direction
    {
        toRight = 1,
        toLeft = -1
    }

    private float initialSpawnInterval = 6.5f;
    private float timeTilNextSpawn;
    private float lastSpawn;

    // Use this for initialization
    void Start () {
        SpawnEnemy();
        timeTilNextSpawn = 0;
        timeTilNextSpawn = initialSpawnInterval;
        lastSpawn = Time.time;
    }
    
    // Update is called once per frame
    void Update () {
        if (lastSpawn + timeTilNextSpawn < Time.time)
        {
            SpawnEnemy();
            lastSpawn = Time.time;

            if (timeTilNextSpawn > 1.5f)
            {
                timeTilNextSpawn -= 0.2f;
            }
        }
    }

    /// <summary>
    /// Spawns random power up at random location
    /// </summary>
    private void SpawnEnemy()
    {
        GameObject enemy = Instantiate(EnemyPrefab, transform.position, transform.rotation);
        //enemy.GetComponent<Enemy>().Direction = (int)direction;
    }
}
