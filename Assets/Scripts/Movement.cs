using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace TAHL.Transmission {
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Health))]
    public class Movement : MonoBehaviour {

        public Image HealthBar;
        public bool IsFacingRight { get {
                return
                    transform.rotation.y > -1.01 &&
                    transform.rotation.y < -0.98;
            } }

        private Animator _anim;
        private Rigidbody2D _rb;
        private Health _health;
        private int _direction = 0;
        private float healthPoints = 100;
        private float damageCooldown = 2;

        private List<EnemyCollision> enemyCollisions = new List<EnemyCollision>();

        private class EnemyCollision
        {
            public GameObject obj;
            public float lastHit;
        }

        // Use this for initialization
        void Start() {
            _rb = GetComponent<Rigidbody2D>();
            _anim = GetComponent<Animator>();
            _health = GetComponent<Health>();
        }

        // Update is called once per frame
        void Update() {
            _rb.velocity = new Vector2(_direction * 8, 0);

            foreach (EnemyCollision coll in enemyCollisions)
            {
                if (coll.lastHit + damageCooldown < Time.time)
                {
                    InflictDamage(coll);
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
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == 8)
            {
                EnemyCollision coll = new EnemyCollision();
                coll.obj = other.gameObject;
                enemyCollisions.Add(coll);

                InflictDamage(coll);
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.layer == 8)
            {
                EnemyCollision coll = enemyCollisions.FirstOrDefault(colission => colission.obj.GetInstanceID() == other.gameObject.GetInstanceID());
                if (coll != null)
                {
                    enemyCollisions.Remove(coll);
                }
            }
        }

        private void InflictDamage(EnemyCollision coll) {

            if (healthPoints > 0)
            {
                healthPoints -= 100;
                coll.lastHit = Time.time;

                HealthBar.fillAmount = healthPoints / 100;

                _health.InflictDamage(Vector2.zero, 100);
                _anim.SetTrigger("death");
            }
        }

        public void FlipPlayer()
        {
            transform.rotation = Quaternion.Euler(0, IsFacingRight ? 0 : 180, 0);
        }
    }
}
