using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Transform startPoint;
    public GameObject bulletPrefab;

    public Transform playerHand;

    public float bulletForce = 20f;

    public float moveSpeed;
    public Rigidbody2D rb;

    public Vector2 movement;
    private Vector2 shootingPoint;

    public Animator animator;

    int x = 1;
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if(movement != Vector2.zero)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
        }
        
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (movement.y < 0)
        {
            animator.SetFloat("UDIdle", -1);
            animator.SetFloat("RLIdle", 0);
            x = -1;
        }
        else if (movement.y > 0)
        {
            animator.SetFloat("UDIdle", 1);
            animator.SetFloat("RLIdle", 0);
            x = 1;
        }
        else if (movement.x < 0)
        {
            animator.SetFloat("UDIdle", 0);
            animator.SetFloat("RLIdle", -1);
            x = -1;
        }
        else if (movement.x > 0)
        {
            animator.SetFloat("UDIdle", 0);
            animator.SetFloat("RLIdle", 1);
            x = 1;
        }
        playerHand.localPosition = new Vector3(1.1f * x, -0.69f, 0);
        //with conditions at the top, write player weapon animation function;
        //take attack rate property from weapon script or from IWeapon interface
        if (Input.GetButtonDown("Submit"))
        {
            Shoot();
        }

    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }
    public void Shoot()
    {
        if (movement.x == 0) shootingPoint.x = animator.GetFloat("RLIdle");
        else shootingPoint.x = movement.x;

        if(movement.y == 0) shootingPoint.y = animator.GetFloat("UDIdle");
        else shootingPoint.y = movement.y;
        GameObject bullet = Instantiate(bulletPrefab, startPoint.position, startPoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(shootingPoint.normalized * bulletForce, ForceMode2D.Impulse);
    }
}
