﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameObject Character;
    private AudioSource audioSource;
    private AudioClip audioClip;
    private GameObject parent;
    new private Rigidbody2D rigidbody;
    public GameObject Parent { set { parent = value; } get { return parent;} }
    private float speed = 10f;
    private Vector3 direction;
    public Vector3 Direction { set { direction = value; } }
    private SpriteRenderer sprite;
    
   
    public Color Color
    {
        set { sprite.color = value; }
    }

    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        rigidbody = GetComponent<Rigidbody2D>();
      

    }
    private void Start()
    {
       Destroy(gameObject, 1f);
       
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();
        if (unit && unit.gameObject != parent)
        {
            Destroy(gameObject);
            
        }
        
    }
}
