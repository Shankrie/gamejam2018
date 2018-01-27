using UnityEngine;
using System.Collections;

namespace TAHL.Transmission
{
    public class Shooting : MonoBehaviour
    {
        public GameObject bullet;

        public void Start()
        {

        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Shoot();
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

    }
}