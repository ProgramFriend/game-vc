using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyPatrol : MonoBehaviour
{
    public float speed;


    public Vector2 min_xy;
    public Vector2 max_xy;

    Vector2 moveSpot;
    Vector2 direction;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        min_xy += (Vector2)transform.position;
        max_xy += (Vector2)transform.position;
        
        moveSpot = new Vector2(Random.Range(min_xy.x, max_xy.x), Random.Range(min_xy.y, max_xy.y));
    }
    void FixedUpdate()
    {
        direction = moveSpot - rb.position;
        transform.position = Vector2.MoveTowards(transform.position, moveSpot, speed * Time.fixedDeltaTime);
        rb.velocity = direction * speed * Time.fixedDeltaTime;
        if(Vector2.Distance(rb.position, moveSpot) <= 0.2f)
        {
            moveSpot = new Vector2(Random.Range(min_xy.x, max_xy.x), Random.Range(min_xy.y, max_xy.y));
        }
    }
}
