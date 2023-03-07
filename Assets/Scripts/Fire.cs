using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class Fire : MonoBehaviour
{
    
    new private Rigidbody2D rigidbody;
    private SpriteRenderer sprite;
    [SerializeField] private AIPath aiPath;
    private GameObject parent;
    private float speed = 10f;
    private Vector3 direction;
    public Vector3 Direction { set { direction = value; } }
    public GameObject Parent { set { parent = value; } get { return parent; } }



    private void Start()
    {
        Destroy(gameObject, 2f);
    }

    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();

    }
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);


        sprite.flipX = direction.x > 0;


    }
}
