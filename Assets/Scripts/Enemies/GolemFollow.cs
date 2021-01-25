using Pathfinding;
using UnityEngine;
using System.Collections.Generic;

public class GolemFollow : MonoBehaviour
{
	Transform target;
	Vector2 pos;
	Vector2 offset;
	Vector2 spawnPos;

	public float speed;
	private Animator anim;

	public Transform atkPoint;

	float x=-1.35f;
	float xx=2f;

	void Start()
	{
		anim = GetComponent<Animator>();
		anim.SetBool("isMoving", true);
		target = GameObject.Find("MC").transform;
	}
    private void Update()
    {
		pos.x = target.position.x - transform.position.x;
		pos.y = target.position.y - transform.position.y;
		

		if (pos.x > 0) offset.x = -xx; //in the right
		else offset.x = xx;
		if (pos.y > 0) offset.y = x;
		else offset.y = x;


		anim.SetFloat("Horizontal", pos.x);
		anim.SetFloat("Vertical", pos.y);
	}
    void FixedUpdate()
	{
		transform.position = Vector2.MoveTowards(transform.position, (Vector2)target.position + offset, speed * Time.fixedDeltaTime);
	}
}