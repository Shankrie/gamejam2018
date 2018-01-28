using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TAHL.Transmission
{
    // Be healthy and kill zombies
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class Health : MonoBehaviour
    {
        public Shooting Shooting = null;
        //public Image HealthBar;

        private Rigidbody2D _rb;
        private Animator _anim;
        private SpriteRenderer _spriteRenderer;

        private Movement _movement;
        private Enemy Enemy;

        private GameObject _gameEndDialog;

        private float _deathTime = 0;
        private int _health = 100;

        private bool _isDead = false;


        // Use this for initialization
        void Start()
        {
            _gameEndDialog = GameObject.FindGameObjectWithTag(Globals.Tags.GameEndDialog);
            _gameEndDialog.SetActive(false);
            if(Shooting == null)
                throw new System.Exception("Add Shooting component to health component");
                
            _rb = GetComponent<Rigidbody2D>();
            _anim = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _movement = GetComponent<Movement>();
        }

        void Update()
        {
            if (_isDead)
            {
                if(_deathTime + Globals.Delays.DEATH > Time.time)
                {
                    Globals.RemoveCharacher(transform, _spriteRenderer, _deathTime);
                }
                else
                {
                    Destroy(gameObject);
                    _isDead = false;
                }
            }
        }

        private void PlayGameOverSound()
        {
            AudioClip clip = (AudioClip)Resources.Load("game-over");
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(clip);

        }

        public void InflictDamage(Globals.EnemyCollision coll, Vector2 bulletForce, int damage)
        {
            _health -= damage;
            //HealthBar.fillAmount = _health / 100;
            coll.lastHit = Time.time;

            if (_health <= 0 && !_isDead)
            {
                _isDead = true;

                
                Shooting.enabled = false;
                _movement.enabled = false;
                _rb.AddForce(bulletForce * 100);

                _deathTime = Time.time;
                _anim.SetTrigger("death");

                if (_gameEndDialog != null)
                {
                    Globals.GlobarVars.GameOverFlag = true;
                    PlayGameOverSound();
                    _gameEndDialog.SetActive(true);
                }

                Destroy(Shooting.gameObject);
            }
        }
    }
}