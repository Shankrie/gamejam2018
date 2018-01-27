﻿using UnityEngine;

namespace TAHL.Transmission
{
    public class Shooting : MonoBehaviour
    {
        public GameObject bullet;

        private Movement movement;


        public void Start()
        {
            movement = transform.parent.GetComponent<Movement>();
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Shoot();
            }

            //CalculateAngle();                
            float angle = CalculateAngle();

            bool isFacingRight = transform.parent.rotation.y > -1.01 && 
                transform.parent.rotation.y < -0.98;
            if(isFacingRight)
                transform.rotation = Quaternion.Euler(180, 0, angle - 85);
            else
                transform.rotation = Quaternion.Euler(0, 0, -angle + 95);

            if (angle > 0 && isFacingRight)
                movement.FlipPlayer(isFacingRight: true);
            else if (angle < 0 && !isFacingRight)
                movement.FlipPlayer(isFacingRight: false);
        }

        private void Shoot()
        {
            //Instantiate(bullet, transform.position, transform.rotation);
            GameObject shootedBullet = GameObject.Instantiate(bullet, transform.position, transform.rotation) as GameObject;
            shootedBullet.transform.parent = null;

            //shootedBullet.parent = null;
            BulletMovement bulletMovement = shootedBullet.GetComponent<BulletMovement>();
            bulletMovement.Direction = transform.root.rotation.y > (-1 - 0.1f) && transform.root.rotation.y < (-1 + 0.1f) ? -1 : 1;
            bulletMovement.InstanceId = transform.root.GetInstanceID();
        }

        /// <summary>
        /// Calculates angle between mouse position and pivot point around which bow rotates
        /// </summary>
        /// <returns>Angle in degrees</returns>
        private float CalculateAngle()
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));

            // DEBUG:
            //Vector3 rayDirection = mousePos - Hand.position;
            //Debug.DrawRay(Hand.position, rayDirection, Color.red);

            //rayDirection = new Vector3(mousePos.x, Hand.position.y, mousePos.z) - Hand.position;
            //Debug.DrawRay(Hand.position, rayDirection, Color.green);

            float xDiff = (transform.position.x - mousePos.x);
            float yDiff = transform.position.y - mousePos.y;
            return Mathf.Atan2(xDiff, yDiff) * Mathf.Rad2Deg;
        }
    }
}