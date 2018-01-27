using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    //public float Direction
    //{
    //    get
    //    {
    //        return direction;
    //    }
    //    set { direction = value;  }
    //}

    public GameObject player;

    private Rigidbody2D _rb;
    private int direction;

    // Use this for initialization
    void Start () {

        GameObject[] gos = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[]; // will return an array of all GameObjects in the scene
        foreach (GameObject go in gos)
        {
            if (go.layer == 10)
            {
                player = go;
                break;
            }
        }

        _rb = GetComponent<Rigidbody2D>();
    }
    
    // Update is called once per frame
    void Update () {
        int direction = 1; 
        if (player.transform.position.x < transform.position.x)
        {
            direction = -1;
        }

        _rb.velocity = new Vector2(direction * 0.65f, _rb.velocity.y);
    }
}
