using UnityEngine;

namespace TAHL.Transmission {
    public class Spawner : MonoBehaviour {

        public GameObject EnemyPrefab;

        private GameObject _player = null;

        private float spawnTime;
        private float lastSpawn;

        private float decreaseSpawnTime = 0;

        // Use this for initialization
        void Start() {
            SpawnEnemy();
            spawnTime = NextSpawnTime();
            lastSpawn = Time.time;

            _player = GameObject.FindGameObjectWithTag(Globals.Tags.Player);
        }

        // Update is called once per frame
        void Update() {
            if (_player == null)
                return;

            if (spawnTime < Time.time)
            {
                SpawnEnemy();
                lastSpawn = Time.time;

                decreaseSpawnTime -= Globals.Constants.DECREASE_SPAWN_TIME_BY;
                spawnTime = NextSpawnTime() - decreaseSpawnTime;
            }
        }

        private float NextSpawnTime()
        {
            return Time.time + Random.Range(Globals.Constants.MIN_SPAWN_TIME, Globals.Constants.MAX_SPAWN_TIME);
        }

        /// <summary>
        /// Spawns random power up at random location
        /// </summary>
        private void SpawnEnemy()
        {
            GameObject enemy = Instantiate(EnemyPrefab, transform.position, transform.rotation);
        }
    }
}
