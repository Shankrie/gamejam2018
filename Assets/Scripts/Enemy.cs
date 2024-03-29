﻿using System;
using UnityEngine;

namespace TAHL.Transmission
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Animator))]
    public class Enemy : MonoBehaviour {

        public bool IsDead { get { return _isDead; } }

        private GameObject _player;

        private Rigidbody2D _rb;
        private SpriteRenderer _spriteRender;
        private Animator _anim;

        private float _deathTime = 0;
        private float _lastFlipTime = 0;
        private float _initialTime = 0;
        private float _speedIncreaseTime = 0;
        private float _speed;

        private int _health = 100;
        private int lastDirection = 1;

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
            _spriteRender = GetComponent<SpriteRenderer>();
            _anim = GetComponent<Animator>();

            _initialTime = Time.time;
            _speed = UnityEngine.Random.Range(Globals.Constants.ZOMBIE_SPEED, Globals.Constants.ZOMBIE_SPEED + 1.0f);
            _speedIncreaseTime = UnityEngine.Random.Range(Globals.Constants.ZOMBIE_SPEED_INC_TIME_MIN, 
                Globals.Constants.ZOMBIE_SPEED_INC_TIME_MAX) + Time.time;
        }

        // Update is called once per frame
        void Update() {
            if (_player == null)
                return;

            if(_isDead)
            {
                if(_deathTime + Globals.Delays.DEATH > Time.time)
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

            if(lastDirection != direction && Time.time > _lastFlipTime + Globals.Delays.FLIP)
            {
                transform.rotation = Quaternion.Euler(0, direction == 1 ? 0 : 180, 0);
                lastDirection = direction;
                _lastFlipTime = Time.time;
            }

            _rb.velocity = new Vector2(direction * _speed, _rb.velocity.y);
            if(_speed < Globals.Constants.ZOMBIE_SPEED_MAX && _speedIncreaseTime < Time.time)
            {
                _speed += Globals.Constants.INC_ZOMBIE_SPEED_BY;
                _speedIncreaseTime = UnityEngine.Random.Range(Globals.Constants.ZOMBIE_SPEED_INC_TIME_MIN, 
                    Globals.Constants.ZOMBIE_SPEED_INC_TIME_MAX) + Time.time;

            }
        }

        public void InflictDamage(Vector2 force, int damage)
        {
            _health -= damage;
            if(_health <= 0)
            {
                gameObject.tag = Globals.Tags.Untagged;

                // shut down triggers for not to hit player when dead
                transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = false;

                _isDead = true;
                _deathTime = Time.time;
                _anim.SetTrigger("death");
            }
        }

        private void PlayZombieAttack()
        {
            AudioClip clip = (AudioClip)Resources.Load("zombie-attack");
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(clip);

        }

        private void PlayZombieVoice()
        {
            AudioClip clip = (AudioClip)Resources.Load("zombie-sound");
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(clip);
            audioSource.loop = !IsDead;
        }
    }
}