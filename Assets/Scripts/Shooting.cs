using UnityEngine;

namespace TAHL.Transmission
{
    public class Shooting : MonoBehaviour
    {
        public GameObject bullet;
        private Transform firePoint;
        private float angle = 0;
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
                transform.rotation = Quaternion.Euler(180, 0, angle - 85);
            else
                transform.rotation = Quaternion.Euler(0, 0, -angle + 95);

            if ((angle > 0 && movement.IsFacingRight) ||
                (angle < 0 && !movement.IsFacingRight))
            {
                movement.FlipPlayer();
            }
        }

        private void Shoot()
        {
            PlayShot();
            Invoke("PlayLeverRifleCocking", 0.5f); 
            //Instantiate(bullet, transform.position, transform.rotation);
            GameObject movingBullet = GameObject.Instantiate(bullet, firePoint.transform.position, Quaternion.identity) as GameObject;
            movingBullet.transform.parent = null;

            //shootedBullet.parent = null;
            BulletMovement bulletMovement = movingBullet.GetComponent<BulletMovement>();
            bulletMovement.InstanceId = transform.root.GetInstanceID();

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
            bulletMovement.Direction = new Vector2(mousePos.x, mousePos.y);
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