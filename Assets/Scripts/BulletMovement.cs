using UnityEngine;

namespace TAHL.Transmission
{

    [RequireComponent(typeof(Rigidbody2D))]
    public class BulletMovement : MonoBehaviour {

        public Vector2 Direction { set { _direction = value; } }
        public int InstanceId { set { _instanceId = value; } }

        private Rigidbody2D _rb;
        private Vector2 _direction = Vector2.zero;
        private Vector2 _releasePoint = Vector2.zero;

        private float _velocityX = 0;
        private float _velocityY = 0;

        private int _instanceId = 0;


        // Use this for initialization
        void Start() {
            _rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update() {
            float x = transform.position.x;
            float y = transform.position.y;

            transform.position = new Vector3(x + (_velocityX * Time.deltaTime), y + (_velocityY * Time.deltaTime), 0);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(Globals.Tags.Player) ||
                collision.gameObject.CompareTag(Globals.Tags.Untagged))
                return;

            if (collision.gameObject.CompareTag(Globals.Tags.Enemy))
                collision.GetComponent<Enemy>().InflictDamage(new Vector2(1, 1), Globals.Constants.PLAYER_DAMAGE);

            // when hit collider except player then destroy itself
            Destroy(gameObject);
        }

        public void Release(Vector2 releasePoint, float angle , int instanceId, bool facingRight)
        {
            _releasePoint = releasePoint;
            _instanceId = instanceId;

            int directionY = facingRight ? 1 : -1;

            _velocityX = -Globals.Constants.BULLET_SPEED * Mathf.Cos(ToRadians(angle));
            _velocityY = Globals.Constants.BULLET_SPEED * directionY * Mathf.Sin(ToRadians(angle));
        }

        private float ToRadians(float angle)
        {
            return (angle * Mathf.PI) / 180;
        }
    }
}
