using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Unit
{
    private AudioSource audioSource;
    private Animator animator;
    [SerializeField] private AudioSource Explosion;
    protected virtual void Awake() 
    {
       animator = GetComponent<Animator>();
       audioSource = GetComponent<AudioSource>();
    }
   protected virtual void Start() 
    {
       
    }
   protected virtual void Update()
    {
        
    }

    protected virtual void  OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();
        Bullet bullet = collider.GetComponent<Bullet>();

        if (bullet)
        {
            Invoke("Die", 0.5f);
            animator.SetTrigger("MonsterDie");
            Explosion.Play();
        }
        
    }   

}
