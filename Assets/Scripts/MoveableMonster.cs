using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MoveableMonster : Monster
{
    private AudioSource audioSource;
    private Animator animator;
    [SerializeField] private float speed = 2f;
    [SerializeField] private AudioSource ExplosionMM;
    private Vector3 direction;
    private SpriteRenderer sprite;

    protected override void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    protected override void Start()
    {
        direction = transform.right;
    }
    protected override void Update()
    {
        Move();
    }
 
    private void Move()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.up * 0.4f + transform.right * direction.x * 0.5f, 0.2f);
        if (colliders.Length > 0 && colliders.All(x => !x.GetComponent<Character>()))  direction *= -1f;
        
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    }
    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();
        Bullet bullet = collider.GetComponent<Bullet>();

        if (bullet)
        {
            Invoke("Die", 0.5f);
            animator.SetTrigger("MoveableMonsterDie");
            ExplosionMM.Play();

        }
    }
}
