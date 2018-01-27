using UnityEngine;

namespace TAHL.Transmission
{

    [RequireComponent(typeof(Rigidbody2D))]
    public class BulletMovement : MonoBehaviour {

        public int Direction { set { _direction = value; } }
        public int InstanceId { set { _instanceId = value; } }

        private Rigidbody2D _rb;
        private int _direction = 1;

        private int _instanceId = 0;

        private const int SPEED = 20;
        private const int DAMAGE = 20;

        // Use this for initialization
        void Start() {
            _rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update() {
            _rb.velocity = new Vector2(SPEED * _direction, 0);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.root.GetInstanceID() == _instanceId)
                return;

            if (collision.gameObject.CompareTag(Globals.Tags.Enemy) || 
                collision.gameObject.CompareTag(Globals.Tags.Player))
            {
                collision.GetComponent<Health>().InflictDamage(DAMAGE, new Vector2(1, 1));
            }
    
            Destroy(gameObject);
        }
    }
}
