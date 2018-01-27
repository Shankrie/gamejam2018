using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAHL.Transmission
{
    // Be healthy and kill zombies
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class Health : MonoBehaviour
    {
        public Shooting Shooting = null;

        private Rigidbody2D _rb ;
        private SpriteRenderer _spriteRenderer;
        private Movement _movement;
        
        private float _deathTime = 0;
        private int _health = 100;

        private bool _isDead = false;

        private const int DEATH_DELAY = 3;

        // Use this for initialization
        void Start()
        {
            if(Shooting == null)
                throw new System.Exception("Add Shooting component to health component");
                
            _rb = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _movement = GetComponent<Movement>();
        }

        void Update()
        {
            if (_isDead)
            {
                if(_deathTime + DEATH_DELAY > Time.time)
                {
                    Globals.RemoveCharacher(transform, _spriteRenderer, _deathTime);
                }
                else
                {
                    Destroy(transform.GetChild(0).gameObject);
                    Destroy(gameObject);
                    _isDead = false;
                }
            }
        }

        public void InflictDamage(Vector2 bulletForce, int damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                _isDead = true;

                Shooting.Dissapear();
                _movement.enabled = false;
                _rb.AddForce(bulletForce * 100);

                _deathTime = Time.time;
            }
        }
    }
}