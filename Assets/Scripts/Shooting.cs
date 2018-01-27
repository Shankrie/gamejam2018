using UnityEngine;

namespace TAHL.Transmission
{
    public class Shooting : MonoBehaviour
    {
        public GameObject bullet;

        private float _angle = 1;
        private bool _facingRight = false;

        public void Start()
        {

        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Shoot();
            }

            //CalculateAngle();                
            _angle = CalculateAngle();

            transform.rotation = Quaternion.Euler(0, 0, -_angle - 270);
            if (_angle > 0)
            {
                Debug.Log("test");
            }
            else if (_angle < 0)
            {
                Debug.Log("Best");
            }
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