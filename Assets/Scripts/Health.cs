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
        private float _timeToDie = 0;
        private int _health = 100;

        private bool _isDead = false;

        // Use this for initialization
        void Start()
        {
            if(shooting == null)
                throw new System.Exception("Add Shooting component to health component");
            _rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            if(!_isDead && 1 < Time.time)
            {
                // Destroy(gameObject);
                //_rb.constraints = RigidbodyConstraints2D.None;
                //_rb.AddForce(new Vector2(100, 300) * 100);
                _isDead = true;
            }
        }

        public void InflictDamage(Vector2 bulletForce, int damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                movement.enabled = false;
                shooting.enabled = false;
                _rb.AddForce(bulletForce * 100);
            }
        }
    }
}