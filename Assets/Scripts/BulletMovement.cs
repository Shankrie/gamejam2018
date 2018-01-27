using UnityEngine;

namespace TAHL.Transmission
{

    [RequireComponent(typeof(Rigidbody2D))]
    public class BulletMovement : MonoBehaviour {

        public Vector2 Direction { set { _direction = value; } }
        public int InstanceId { set { _instanceId = value; } }

        private Rigidbody2D _rb;
        private Vector2 _direction = Vector2.zero;

        private int _instanceId = 0;

        private const int SPEED = 20;
        private const int DAMAGE = 20;

        // Use this for initialization
        void Start() {
            _rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update() {
            _rb.velocity = _direction;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.root.GetInstanceID() == _instanceId)
                return;

            if (collision.gameObject.CompareTag(Globals.Tags.Enemy))
            {
                collision.GetComponent<Enemy>().InflictDamage(new Vector2(1, 1), DAMAGE);
            }
            else if(collision.gameObject.CompareTag(Globals.Tags.Player))
            {
                collision.GetComponent<Health>().InflictDamage(new Vector2(1, 1), DAMAGE);
            }
    
            Destroy(gameObject);
        }
    }
}
