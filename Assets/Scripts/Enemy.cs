using System;
using UnityEngine;

namespace TAHL.Transmission
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Animator))]
    public class Enemy : MonoBehaviour {

        private GameObject _player;

        private Rigidbody2D _rb;
        private SpriteRenderer _spriteRender;
        private Animator _anim;

        public bool IsDead { get { return _isDead; } }

        private float _deathTime = 0;
        private int _health = 100;

        private bool _isDead = false;

        private const int DEATH_DELAY = 3;

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
            _spriteRender = GetComponent<SpriteRenderer>();
            _anim = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update() {
            if (_player == null)
                return;

            if(_isDead)
            {
                if(_deathTime + DEATH_DELAY > Time.time)
                {
                    Globals.RemoveCharacher(transform, _spriteRender, _deathTime);
                }
                else
                {
                    Destroy(gameObject);
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
            if(_health <= 0)
            {
                gameObject.tag = Globals.Tags.Untagged;
                _isDead = true;
                _deathTime = Time.time;
                _anim.SetTrigger("death");
            }
        }
    }
}