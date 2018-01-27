using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAHL.Transmission
{
    // Be healthy and kill zombies
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Health : MonoBehaviour
    {
        public Shooting shooting = null;
        public Movement movement = null;

        private Rigidbody2D _rb = null;
        private SpriteRenderer _spriteRenderer = null;
        
        private float _deathTime = 0;
        private int _health = 100;

        private bool _isDead = false;

        private const int DEATH_DELAY = 3;

        // Use this for initialization
        void Start()
        {
            if(shooting == null)
                throw new System.Exception("Add Shooting component to health component");
            _rb = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            if (_isDead)
            {
                if(_deathTime + DEATH_DELAY > Time.time)
                {
                    Globals.RemoveCharacher(transform, _spriteRenderer, _deathTime, DEATH_DELAY);
                }
                else
                {
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

                movement.enabled = false;
                shooting.enabled = false;
                _rb.AddForce(bulletForce * 100);

                _deathTime = Time.time;
            }
        }
    }
}