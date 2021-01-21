using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;

    public Vector2 movement;

    public Animator animator;

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
        }
        else if (movement.y > 0)
        {
            animator.SetFloat("UDIdle", 1);
            animator.SetFloat("RLIdle", 0);
        }
        else if (movement.x < 0)
        {
            animator.SetFloat("UDIdle", 0);
            animator.SetFloat("RLIdle", -1);
        }
        else if (movement.x > 0)
        {
            animator.SetFloat("UDIdle", 0);
            animator.SetFloat("RLIdle", 1);
        }
        //with conditions at the top, write player weapon animation function;
        //take attack rate property from weapon script or from IWeapon interface

    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }
}
