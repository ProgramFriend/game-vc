using Pathfinding;
using UnityEngine;

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

//	Vector2 targetSpot;

	void Start()
    {
		seeker = GetComponent<Seeker>();
		rb = GetComponent<Rigidbody2D>();
		target = GameObject.Find("MC").transform;

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

		//DOES THIS REALLY NEED TO BE HERE????
		if(((Vector2)path.vectorPath[currentWaypoint] - rb.position).magnitude < 0)
		{
			direction = (rb.position - (Vector2)path.vectorPath[currentWaypoint]).normalized;
		}
        else
        {
			direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
		}

		rb.velocity = direction * speed * Time.fixedDeltaTime;

		float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

		if (distance < nextWaypointDistance)
			currentWaypoint++;
	}
}