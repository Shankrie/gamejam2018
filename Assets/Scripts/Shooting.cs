using UnityEngine;

namespace TAHL.Transmission
{
    public class Shooting : MonoBehaviour
    {
        public GameObject bullet;

        private Transform _firePoint;
        private Movement _movement;
        private SpriteRenderer _spriteRenderer;

        private float angle = 0;
        private float bulletAngle = 0;
        private float _lastShotTime = 0;
        private float _deathTime = 0;

        private bool _dissapear = false;

        private const float SHOOT_DELAY = 0.5f;

        public void Start()
        {
            _movement = transform.parent.GetComponent<Movement>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _firePoint = transform.GetChild(0);
        }

        public void Update()
        {
            if(_dissapear)
            {
                if(_deathTime + Globals.Constants.DEATH_DELAY > Time.time)
                    Globals.RemoveCharacher(transform, _spriteRenderer, _deathTime);
                return;
            }

            // Check if last time shot and current time diff is greater when shoot delay
            if ((Time.time - _lastShotTime) > SHOOT_DELAY &&
                Input.GetKeyDown(KeyCode.Mouse0))
            {
                Shoot();
                _lastShotTime = Time.time;
            }

            //CalculateAngle();                
            angle = CalculateAngle();

            if (_movement.IsFacingRight)
            {
                bulletAngle = angle - 85;
                // lock on these degrees
                if(angle < -50 && angle > -135)
                    transform.rotation = Quaternion.Euler(180, 0, bulletAngle);
            }
            else
            {
                bulletAngle = -angle + 95;
                if (angle > 50 && angle < 135)
                    transform.rotation = Quaternion.Euler(0, 0, bulletAngle);
            }

            if ((angle > 0 && _movement.IsFacingRight) ||
                (angle < 0 && !_movement.IsFacingRight))
            {
                if (_movement.IsFacingRight)
                {
                    if(angle >= 135 && angle <= -135)
                    {
                        transform.rotation = Quaternion.Euler(0, 0, 135);
                    }
                    else
                    {

                        transform.rotation = Quaternion.Euler(0, 0, bulletAngle);
                    }
                }
                else
                {
                    transform.rotation = Quaternion.Euler(180, 0, 0);
                }


                _movement.FlipPlayer();
            }
        }

        public void Dissapear()
        {
            _deathTime = Time.time;
            _dissapear = true;
        }

        /// <summary>
        /// 
        /// </summary>
        private void Shoot()
        {
            //Instantiate(bullet, transform.position, transform.rotation);
            GameObject movingBullet = GameObject.Instantiate(bullet, _firePoint.transform.position, Quaternion.identity) as GameObject;
            movingBullet.transform.parent = null;

            //shootedBullet.parent = null;
            BulletMovement bulletMovement = movingBullet.GetComponent<BulletMovement>();
            bulletMovement.Release(_firePoint.position, bulletAngle, GetInstanceID(), _movement.IsFacingRight);
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