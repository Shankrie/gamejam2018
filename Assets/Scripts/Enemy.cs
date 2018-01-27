using System;
using UnityEngine;

namespace TAHL.Transmission
{
    public class Enemy : MonoBehaviour {

        private GameObject _player;

        private Rigidbody2D _rb;
        private int direction;

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
            int direction = 1;
            if (_player.transform.position.x < transform.position.x)
            {
                direction = -1;
            }

            _rb.velocity = new Vector2(direction * 0.65f, _rb.velocity.y);
        }
    }
}