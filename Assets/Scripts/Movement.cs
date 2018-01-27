using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAHL.Transmission
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Movement : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private int _direction = 0;      


        // Use this for initialization
        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            _rb.velocity = new Vector2(_direction * 8, 0);
        }

        private void FixedUpdate()
        {
            _direction = 0;
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                _direction = -1;
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                _direction = 1;
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }
}