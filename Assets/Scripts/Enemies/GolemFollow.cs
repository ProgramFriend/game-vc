using Pathfinding;
using UnityEngine;
using System.Collections.Generic;

public class GolemFollow : MonoBehaviour
{
	public Transform target;


	// Caching
	private Rigidbody2D rb;

	public float speed;

	Vector2 direction;

	private Animator anim;


	//	Vector2 targetSpot;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		target = GameObject.Find("MC").transform;
	}
    private void Update()
    {
		anim.SetBool("isMoving", true);
		anim.SetFloat("Horizontal", target.position.x - transform.position.x);
		anim.SetFloat("Vertical", target.position.y - transform.position.y);
    }
    void FixedUpdate()
	{

		//float distance = Vector2.Distance(rb.position, target.transform.position);

		//rb.velocity = direction * speed * Time.fixedDeltaTime;

		transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);


	}
}