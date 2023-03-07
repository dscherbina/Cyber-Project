using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.Threading;

public class FlyingMonster : Monster
{
    public GameObject boss;
    private AudioSource audioSource;
    private SpriteRenderer sprite;
    new private Rigidbody2D rigidbody;
    [SerializeField] private AIPath aiPath;
    [SerializeField] public int bosslives = 5;
    [SerializeField] private AudioSource ExplosionBoss;
    [SerializeField] private AudioSource firesound;
    private Animator animator;
    private float rate = 3f;
    private Fire fire;
    private Material matblink;
    private Material matdefault;
    private bool shootenabled;


    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        fire = Resources.Load<Fire>("Fire");
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        matblink = Resources.Load("Flying Monster", typeof(Material)) as Material;
        matdefault = sprite.material;

    }
    private void Start()
    {
            InvokeRepeating("ShootFire", rate, rate);
    }

    private void ShootFire()
    {
        shootenabled = true;
        Vector3 position = transform.position; 
        Fire newFire = Instantiate(fire, position, fire.transform.rotation) as Fire;
        firesound.Play();
        newFire.Parent = gameObject;
        newFire.Direction = -newFire.transform.right;
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            newFire.Direction = newFire.transform.right;
            
        }
    }
   


    private void Update()
    {
        sprite.flipX = aiPath.desiredVelocity.x >= 0.01f;


    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Bullet bullet = other.GetComponent<Bullet>();
       
        if (bullet && bullet.Parent != gameObject)
        {
            bosslives--;
            Debug.Log(bosslives);
            BossHealth.health -= 1;
            sprite.material = matblink;
            if (bosslives <= 0)
            {
                
                Camera.main.GetComponent<UIManager>().WinTime();
                
                shootenabled = false;
                animator.SetTrigger("BossDie");
                Invoke("Die", 1f);
                ExplosionBoss.Play();
                shootenabled = false;

            }
            else
            {
                {
                    Invoke("ResetMaterial", .2f);
                }
            }
        }
    }
    

    private void ResetMaterial()
    {
        sprite.material = matdefault;
    }

}

