using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace TAHL.Transmission {
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Health))]
    public class Movement : MonoBehaviour {

        public bool IsFacingRight { get {
                return
                    transform.rotation.y > -1.01 &&
                    transform.rotation.y < -0.98;
            } }

        private List<Globals.EnemyCollision> enemyCollisions = new List<Globals.EnemyCollision>();
        private Animator _anim;
        private Rigidbody2D _rb;
        private Health _health;

        private float damageCooldown = 2;
        private float _jumpTime = 0;
        private int _direction = 0;


        // Use this for initialization
        void Start() {
            _rb = GetComponent<Rigidbody2D>();
            _anim = GetComponent<Animator>();
            _health = GetComponent<Health>();
        }

        // Update is called once per frame
        void Update() {
            _rb.velocity = new Vector2(_direction * 8, _rb.velocity.y);

            foreach (Globals.EnemyCollision coll in enemyCollisions)
            {
                if (coll.lastHit + damageCooldown < Time.time)
                {
                    _health.InflictDamage(coll, Vector2.zero, Globals.Constants.ZOMBIE_DAMAGE);
                }
            }
        }

        private void FixedUpdate()
        {
            _direction = 0;
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                _direction = -1;
                _anim.SetBool("walk", true);
            }
            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                _direction = 1;
                _anim.SetBool("walk", true);
            }
            else
            {
                _anim.SetBool("walk", false);
            }

            if(_jumpTime < Time.time && Input.GetKey(KeyCode.Space))
            {
                _rb.velocity = new Vector2(_rb.velocity.x, 20);
                _jumpTime = Time.time + Globals.Delays.JUMP;
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == 8)
            {
                Globals.EnemyCollision coll = new Globals.EnemyCollision();
                coll.obj = other.gameObject;
                enemyCollisions.Add(coll);

                _health.InflictDamage(coll, Vector2.zero, Globals.Constants.ZOMBIE_DAMAGE);
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.layer == 8)
            {
                Globals.EnemyCollision coll = enemyCollisions.FirstOrDefault(colission => colission.obj.GetInstanceID() == other.gameObject.GetInstanceID());
                if (coll != null)
                {
                    enemyCollisions.Remove(coll);
                }
            }
        }


        public void FlipPlayer()
        {
            transform.rotation = Quaternion.Euler(0, IsFacingRight ? 0 : 180, 0);
        }
    }
}
