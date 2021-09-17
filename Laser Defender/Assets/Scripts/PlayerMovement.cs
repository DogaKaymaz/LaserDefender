using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    private Camera gameCamera;
    
    [SerializeField] private GameObject laserPrefab;

    private Coroutine firingCoroutine;
    
     
    private float
        minX,
        maxX,
        minY,
        maxY;

    [Header("Projectile")] 
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float projectileFiringPeriod = 0.1f;
    
    [Header("Player Movement")]
    [SerializeField] private float padding = 1f;
    [SerializeField] private float moveSpeed;
    [SerializeField] private int health = 200;


    private void Awake()
    {
        gameCamera = Camera.main;
        SetUpTheBoundaries();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        ProcessHit(damageDealer);
    }

    void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    

 

    void Update()
    {
        Move();
        Fire();
    }


    void Fire()
    {
        if (Input.GetButtonDown ("Fire1") )
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }

        if ( Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;

            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);

            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }


    void Move()
      {
          
          var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
          var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
          
          
          var changePosX = Mathf.Clamp(transform.position.x + deltaX, minX, maxX);
          var changePosY = Mathf.Clamp(transform.position.y + deltaY, minY, maxY);

          transform.position = new Vector2(changePosX, changePosY);


      }

    void SetUpTheBoundaries()
      {
          minX = gameCamera.ViewportToWorldPoint(new Vector3(0,0,0)).x + padding;
          maxX = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

          minY = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
          maxY = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
      }
    
}
