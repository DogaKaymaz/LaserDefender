using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    [SerializeField] private GameObject laserPrefab;

    private Coroutine firingCoroutine;
    
    private Camera gameCamera;
    
    
    [SerializeField] private float
        projectileSpeed = 10f,
        projectileFiringPeriod = 0.1f;
    

    private float
        minX,
        maxX,
        minY,
        maxY;

    [SerializeField] private float padding = 1f;
    [SerializeField] private float moveSpeed;
    
    
    private void Awake()
    {
        gameCamera = Camera.main;
        SetUpTheBoundaries();
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
