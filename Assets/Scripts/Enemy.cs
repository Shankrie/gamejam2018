using System;
using UnityEngine;

namespace TAHL.Transmission
{
    public class Enemy : MonoBehaviour {

        private GameObject _player;

        private Rigidbody2D _rb;

        private float _timeToDissapear = 0;
        private float _visibility = 1.0f;
        private int _health = 100;

        private bool _isDead = false;

        // Use this for initialization
        void Start() {
            _player = GameObject.FindGameObjectWithTag(Globals.Tags.Player);
            if (_player == null)
                throw new Exception("Player object is required to be in scene");
    
        GameObject[] gos = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[]; // will return an array of all GameObjects in the scene
            foreach (GameObject go in gos)
            {
                if (go.layer == 10)
                {
                    _player = go;
                    break;
                }
            }

            _rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update() {
            if(_isDead)
            {
                if(_timeToDissapear > Time.time)
                {
                    // dissaprea animation
                }
                return;
            }

            int direction = 1;
            if (_player.transform.position.x < transform.position.x)
            {
                direction = -1;
            }

            _rb.velocity = new Vector2(direction * 0.65f, _rb.velocity.y);
        }

        public void InflictDamage(Vector2 force, int damage)
        {
            _health -= damage;
            if(_health == 0)
            {
                _timeToDissapear = Time.time + 3.0f;
            }
        }
    }
}