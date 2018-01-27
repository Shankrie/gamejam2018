﻿using UnityEngine;

namespace TAHL.Transmission
{
    public class Shooting : MonoBehaviour
    {
        public GameObject bullet;
        private Transform firePoint;
        private float angle = 0;
        private float bulletAngle = 0;
        private Movement movement;


        public void Start()
        {
            movement = transform.parent.GetComponent<Movement>();
            firePoint = transform.GetChild(0);
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Shoot();
            }

            //CalculateAngle();                
            angle = CalculateAngle();

            if (movement.IsFacingRight)
            {
                bulletAngle = angle - 85;
                transform.rotation = Quaternion.Euler(180, 0, bulletAngle);
            }
            else
            {
                bulletAngle = -angle + 95;
                transform.rotation = Quaternion.Euler(0, 0, bulletAngle);
            }

            if ((angle > 0 && movement.IsFacingRight) ||
                (angle < 0 && !movement.IsFacingRight))
            {
                movement.FlipPlayer();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void Shoot()
        {
            //Instantiate(bullet, transform.position, transform.rotation);
            GameObject movingBullet = GameObject.Instantiate(bullet, firePoint.transform.position, Quaternion.identity) as GameObject;
            movingBullet.transform.parent = null;

            //shootedBullet.parent = null;
            BulletMovement bulletMovement = movingBullet.GetComponent<BulletMovement>();
            bulletMovement.Release(firePoint.position, bulletAngle, GetInstanceID(), movement.IsFacingRight);
        }

        /// <summary>
        /// Calculates angle between mouse position and pivot point around which bow rotates
        /// </summary>
        /// <returns>Angle in degrees</returns>
        private float CalculateAngle()
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));

            float xDiff = (transform.position.x - mousePos.x);
            float yDiff = transform.position.y - mousePos.y;
            return Mathf.Atan2(xDiff, yDiff) * Mathf.Rad2Deg;
        }
    }
}