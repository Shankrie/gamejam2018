using UnityEngine;

namespace TAHL.Transmission {
    public class Spawner : MonoBehaviour {

        public GameObject EnemyPrefab;

        private GameObject _player = null;

        private float _spawnTime;
        private float _lastSpawn;

        private float _decreaseSpawnTime = 0;

        // Use this for initialization
        void Start() {
            SpawnEnemy();
            _spawnTime = NextSpawnTime();
            _lastSpawn = Time.time;

            _player = GameObject.FindGameObjectWithTag(Globals.Tags.Player);
        }

        // Update is called once per frame
        void Update() {
            if (_player == null)
                return;

            if (_spawnTime < Time.time && !Globals.GlobarVars.GameOverFlag)
            {
                SpawnEnemy();
                _lastSpawn = Time.time;

                _decreaseSpawnTime -= Globals.Constants.DECREASE_SPAWN_TIME_BY;
                _spawnTime = NextSpawnTime() - _decreaseSpawnTime;
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
