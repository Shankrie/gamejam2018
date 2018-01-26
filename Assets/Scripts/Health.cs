using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAHL.Transmission
{
    // Be healthy and kill zombies
    public class Health : MonoBehaviour
    {

        private int _health = 100;

        // Use this for initialization
        void Start()
        {

        }


        public void InflictDamage(int damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}