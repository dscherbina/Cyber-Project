using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Character : Unit
{

    [SerializeField] public int lives = 5;
    private Heal heal;
    Text text;
    public int Lives
    {
        get { return lives; }
        set
        {
            if (value <= 5) lives = value;
            livesBar.Refresh();
        }
    }
    private LivesBar livesBar;

    [SerializeField] private AudioSource Jumping;
    [SerializeField] private AudioSource Running;
    [SerializeField] private AudioSource Shooting;
    [SerializeField] private AudioSource Hurting;
    [SerializeField] private AudioSource Healing;
    [SerializeField] private AudioSource Taking;
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private float jumpForce = 10f;


    private AudioSource audioSource;
    new private Rigidbody2D rigidbody;
    private Animator animator;
    private SpriteRenderer sprite;
    private bool isGrounded;
    private bool climbed;
    private bool shootenabled;
    private bool shootfalse = true;
    private bool shootingrun;
    private bool hurt;
    private bool isjumping = true;
    public float fireDelay = 0.1f;
    float fireElapsedTime = 0;

    private Bullet bullet;
    private CharState State
    {
        get { return (CharState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }

    public int bosslives { get; private set; }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        bullet = Resources.Load<Bullet>("Bullet");
        livesBar = FindObjectOfType<LivesBar>();
        audioSource = GetComponent<AudioSource>();
        text = GetComponent<Text>();
    }
    private void Update()
    {
        if (isGrounded) State = CharState.idle;
        if (Input.GetButtonDown("Fire1") && (!climbed) && isGrounded && fireElapsedTime >= fireDelay && shootfalse == true)
        {
            shootenabled = true;
            State = CharState.shoot;
            fireElapsedTime = 0;
            Shoot();
            Shooting.Play();
        }
        else
        {
            shootenabled = false;
        }
        if (!isGrounded)
        {
            State = CharState.jump;
        }

        if ((isGrounded) && Input.GetButtonDown("Jump"))
        {
            Jump();
            Running.Stop();
        }
        if ((!isGrounded))
        {
            Running.Stop();
        }
        if (Input.GetButton("Horizontal"))
        {
            Run();
            
            shootingrun = false;
        }
        if (Input.GetButtonDown("Horizontal"))
        {
            
            Running.Play();
        }
        else if (Input.GetButtonUp("Horizontal")) Running.Stop();

        fireElapsedTime += Time.deltaTime;
        if (climbed) State = CharState.climb;
        if (hurt)
        {
            State = CharState.hurt;
            Running.Stop();
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (climbed == false)
            {
                Crouch();
                Running.Stop();
            }
        }

    }

    private void FixedUpdate()
    {
        CheckGround();
    }
     
    private void Run()
    {
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
        sprite.flipX = direction.x < 0;
        
        if (isGrounded) State = CharState.run;
        if (Input.GetButton("Fire1") && isGrounded && shootingrun == true)
        {
            State = CharState.shootingrun;

        }
        hurt = false;
    }

    private void Shoot()
    {

        if (shootenabled == true)
        {
            if (Input.GetKey(KeyCode.S))
            {
                State = CharState.crouch;
                Vector3 position = transform.position; position.y += 0.6f;
                Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as Bullet;
                newBullet.Direction = newBullet.transform.right * (sprite.flipX ? -5f : 5.0f);
                newBullet.Parent = gameObject;
                hurt = false;
            }
            else
            {
                Vector3 position = transform.position; position.y += 1f;
                Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as Bullet;
                newBullet.Direction = newBullet.transform.right * (sprite.flipX ? -5f : +5.0f);
                newBullet.Parent = gameObject;
                hurt = false;
            }

        }
    }
    private void Jump()
    {
        if (isjumping == true)
        {
            rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            State = CharState.jump;
            hurt = false;
            shootingrun = false;
            Jumping.Play();
        }
        
    }
    private void Crouch()
    {
        if (Input.GetKey(KeyCode.S)) State = CharState.crouch;
        hurt = false;
        
    }
    private void CheckGround()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        isGrounded = collider.Length > 1;
        if (!isGrounded) State = CharState.jump;
    }
    public override void ReceiveDamage()
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.AddForce(transform.up * 3f, ForceMode2D.Impulse);
        Lives--;
        Debug.Log(lives);
        
        Hurting.Play();
        hurt = true;
        if (Lives <= 0)
        {
            Camera.main.GetComponent<UIManager>().Lose();
            Die();
        }

    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();
        Bullet bullet = collider.gameObject.GetComponent<Bullet>();
        Fire fire = collider.gameObject.GetComponent<Fire>();
        Heal heal = collider.gameObject.GetComponent<Heal>();
        if (unit)
        {
            ReceiveDamage();
            State = CharState.hurt;
        }
        if (unit is Monster)
        {
            State = CharState.hurt;
        }
        if (bullet && bullet.Parent != gameObject)
        {
            ReceiveDamage();
            State = CharState.hurt;
        }
        if (fire && fire.Parent != gameObject)
        {
            ReceiveDamage();
            State = CharState.hurt;
        }
        if (collider.gameObject.tag == "die")
        {
            Lives = 0;
            Camera.main.GetComponent<UIManager>().Lose();
            MoneyText.Coin = 0;
            Die();
        }
        if (collider.tag == "Heal" && Lives < 5)
        {
            Healing.Play();
        }
        if (collider.tag == "Coins")
        {
            Taking.Play();
        }
        if (collider.tag == "shootfalse")
        {
            shootfalse = false;
            shootingrun = false;
            isjumping = false;

        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "ladder")
            climbed = true;
        shootenabled = false;
        State = CharState.climb;

        if (other.tag == "nojump")
        {

            isjumping = false;

        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        climbed = false;
        shootfalse = true;
        shootingrun = true;
        isjumping = true;

    }

    public enum CharState
    {
        idle,
        run,
        jump,
        shoot,
        shootingrun,
        climb,
        crouch,
        hurt,
        die
    }
}
