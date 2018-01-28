using UnityEngine;

namespace TAHL.Transmission
{
    public class Shooting : MonoBehaviour
    {
        public GameObject bullet;

        private Transform _firePoint;
        private Movement _movement;
        private SpriteRenderer _spriteRenderer;

        private float _angle;
        private float _bulletAngle;
        private float _lastShotTime;

        public void Start()
        {
            _movement = transform.parent.GetComponent<Movement>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _firePoint = transform.GetChild(0);

            _angle = 0;
            _bulletAngle = 0;
            _lastShotTime = 0;
        }

        public void Update()
        {
            // Check if last time shot and current time diff is greater when shoot delay
            if ((Time.time - _lastShotTime) > Globals.Delays.SHOT &&
                Input.GetKeyDown(KeyCode.Mouse0))
            {
                Shoot();
                _lastShotTime = Time.time;
            }

            //CalculateAngle();                
            _angle = CalculateAngle();

            if (_movement.IsFacingRight)
            {
                _bulletAngle = _angle - 85;
                // lock on these degrees
                if(_angle < -50 && _angle > -135)
                    transform.rotation = Quaternion.Euler(180, 0, _bulletAngle);
            }
            else
            {
                _bulletAngle = -_angle + 95;
                if (_angle > 50 && _angle < 135)
                    transform.rotation = Quaternion.Euler(0, 0, _bulletAngle);
            }

            if ((_angle > 0 && _movement.IsFacingRight) ||
                (_angle < 0 && !_movement.IsFacingRight))
            {
                _movement.FlipPlayer();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void Shoot()
        {
            // Play sound
            PlayShot();
            Invoke("PlayLeverRifleCocking", 0.5f);

            // Instantiate bullet
            GameObject movingBullet = GameObject.Instantiate(bullet, _firePoint.transform.position, 
                _movement.IsFacingRight ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0)
            ) as GameObject;
            movingBullet.transform.parent = null;

            //shootedBullet.parent = null;
            BulletMovement bulletMovement = movingBullet.GetComponent<BulletMovement>();
            bulletMovement.Release(_firePoint.position, _bulletAngle, GetInstanceID(), _movement.IsFacingRight);
        }

        private void PlayShot()
        {
            AudioClip clip = (AudioClip)Resources.Load("lever-action-rifle-shot");
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(clip);
        }

        private void PlayLeverRifleCocking()
        {
            AudioClip clip = (AudioClip)Resources.Load("lever-action-rifle-cocking");
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(clip);
        }

        /// <summary>
        /// Calculates _angle between mouse position and pivot point around which bow rotates
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