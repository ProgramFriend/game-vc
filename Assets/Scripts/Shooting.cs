using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform startPoint;
    public GameObject bulletPrefab;

    public float bulletForce = 20f;

    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, startPoint.position, startPoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(startPoint.up * bulletForce, ForceMode2D.Impulse);
    }

}
