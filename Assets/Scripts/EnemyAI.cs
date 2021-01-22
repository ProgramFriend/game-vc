using Pathfinding;
using UnityEngine;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour
{
	public Transform target;

	public float updateRate = 4f;

	// Caching
	private Seeker seeker;
	private Rigidbody2D rb;

	public float speed ;
	public float nextWaypointDistance;

	public Path path;
	private int currentWaypoint = 0;
	[HideInInspector]
	public bool reachedEndOfPath = false;

	Vector2 direction;

	private List<Transform> enemies = new List<Transform>();
	float minDist2 = 3f;
	private Transform enemiesGroupTransform;

//	Vector2 targetSpot;

	void Start()
    {
		seeker = GetComponent<Seeker>();
		rb = GetComponent<Rigidbody2D>();
		target = GameObject.Find("MC").transform;

		enemiesGroupTransform = this.transform.parent.parent;
		foreach(Transform enemy in enemiesGroupTransform)
        {
			enemies.Add(enemy.GetChild(0));
			Debug.Log(enemy.name);
        }

		if(target == null)
        {
			Debug.Log("No player found");
			return;
        }
		InvokeRepeating("UpdatePath", 0f, 1 / updateRate);
    }

	void UpdatePath()
    {
		if(seeker.IsDone())
			seeker.StartPath(rb.position, target.position, OnPathComplete);
	}

	void OnPathComplete(Path p)
    {
		//Debug.Log("We got a path. Did it have an error? " + p.error);
        if (!p.error)
        {
			path = p;
			currentWaypoint = 0;
        }
    }

    void FixedUpdate()
    {
		Vector2 closestFriendPos = Vector2.zero;
		float closestDist2 = minDist2;
		//SEPERATION
		foreach (Transform enemy in enemies)
		{
			// Finds distance between an enemie's pos and its friend's pos
			float curDist2 = Vector3.Distance(transform.position, enemy.transform.position);

			// Is the flocker getting closer to its friend?
			if (curDist2 < minDist2)
			{
				closestFriendPos = enemy.transform.position;
				closestDist2 = curDist2;
			}
		}

		if (path == null)
			return;
		if(currentWaypoint >= path.vectorPath.Count)
        {
			reachedEndOfPath = true;
        }
        else
        {
			reachedEndOfPath = false;
        }

		//direction = (rb.position - (Vector2)path.vectorPath[currentWaypoint]).normalized; ENEMY RUNS FFROM PLAYER
		direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized; 

		if (closestDist2 < minDist2)
		{
			Vector2 avoidDir = ((Vector2)transform.position - closestFriendPos).normalized;
			avoidDir *= (1.3f - closestDist2 / minDist2);
			direction += avoidDir;
		}

		rb.velocity = direction * speed * Time.fixedDeltaTime;

		float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

		if (distance < nextWaypointDistance)
			currentWaypoint++;
	}
}