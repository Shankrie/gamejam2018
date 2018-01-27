using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour {

    public Image HealthBar;
    public bool IsFacingRight { get {
        return 
            transform.rotation.y > -1.01 && 
            transform.rotation.y < -0.98;
        } }

    private Rigidbody2D _rb;
    private int _direction = 0;
    private float healthPoints = 100;
    private float damageCooldown = 2;

    private List<EnemyCollision> enemyCollisions = new List<EnemyCollision>();

    private class EnemyCollision
    {
        public GameObject obj;
        public float lastHit;
    }

	// Use this for initialization
	void Start () {
        _rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        _rb.velocity = new Vector2(_direction * 8, 0);

        foreach(EnemyCollision coll in enemyCollisions)
        {
            if (coll.lastHit + damageCooldown < Time.time)
            {
                inflictDamage(coll);
            }
        }
	}

    private void FixedUpdate()
    {
        _direction = 0;
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            _direction = -1;
        }
        else if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            _direction = 1;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == 8)
        {
            EnemyCollision coll = new EnemyCollision();
            coll.obj = other.gameObject;
            enemyCollisions.Add(coll);

            inflictDamage(coll);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            EnemyCollision coll = enemyCollisions.FirstOrDefault(colission => colission.obj.GetInstanceID() == other.gameObject.GetInstanceID());
            if (coll != null)
            {
                enemyCollisions.Remove(coll);
            }
        }
    }

    private void inflictDamage(EnemyCollision coll) {

        if (healthPoints > 0)
        {
            healthPoints -= 10;
            coll.lastHit = Time.time;

            HealthBar.fillAmount = healthPoints / 100;
        }
    }

    public void FlipPlayer()
    {
        transform.rotation = Quaternion.Euler(0, IsFacingRight ? 0: 180, 0);
    }
}
