using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
   private float health = 100;
   [SerializeField] private float shotCounter;
   
   [SerializeField] private float minTimeBetweenShots = 0.2f;
   [SerializeField] private float maxTimeBetweenShots = 2f;

   [SerializeField] private float projectileSpeed;

   [SerializeField] private GameObject projectile;

   
   private void Start()
   {
       projectileSpeed = 10f;
       shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
   }

   private void Update()
   {
       CountDownAndShoot();
   }

   void CountDownAndShoot()
   {
       shotCounter -= Time.deltaTime;
       if (shotCounter <= 0)
       {
           Fire();
           
           shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
       }
   }

   void Fire()
   {
       GameObject laser = Instantiate(
           projectile,
           transform.position,
           Quaternion.identity) as GameObject;
       laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
   }
   
   private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        
        if (!damageDealer)
        {
            return;
        }
        
        ProcessHit(damageDealer);
    }

    void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.GetHit();
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    
}
