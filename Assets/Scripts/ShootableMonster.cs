using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableMonster : Monster
{
    private AudioSource audioSource;
    private Animator animator;
    [SerializeField] private float rate = 2f;
    private Bullet bullet;
    [SerializeField] private Color bulletcolor = Color.white;
    [SerializeField] private AudioSource Shooting;
    [SerializeField] private AudioSource ExplosionSM;

    protected override void Awake()
    {
        bullet = Resources.Load<Bullet>("Bullet");
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    protected override void Start()
    {
        InvokeRepeating("Shoot",rate,rate);
        
    }
    private void Shoot()
    {
        Vector3 position = transform.position; position.y += 0.5f; position.x -= 0.3f;
        Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as Bullet;

        newBullet.Parent = gameObject;
        newBullet.Direction = -newBullet.transform.right;
        newBullet.Color = bulletcolor;
        Shooting.Play();

    }
    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();
        Bullet bullet = collider.GetComponent<Bullet>();
        if (bullet)
        {
            Invoke("Die", 0.5f);
            animator.SetTrigger("ShootableMonsterDie");
            ExplosionSM.Play();
        }
    }
   

}
