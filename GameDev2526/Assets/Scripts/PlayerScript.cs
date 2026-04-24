using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //adding a comment to fix stuff
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //attributes

    
    private Vector2 initialPlayerLocation = new Vector2(0,0);
    [Header("Player Stats")]
    [SerializeField] private int health = 100;
    public float movementSpeed = 5f;
    public float jumpingForce = 10f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public float dieHeight = -10f;
    public LayerMask groundLayer;
    public bool hasKey = false;


    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool hasDoubleJump;
    [Header("Platform Settings(can move to game manager later)")]
    [SerializeField] private int usagetime = 3;
    [SerializeField] private int cooldownTime = 3;
    [SerializeField] private GameObject platformPrefab;
    private float nextSpawnTime=0;
    //idk man
    //public PhysicsMaterial2D normalFriction;
    //xpublic PhysicsMaterial2D zeroFriction;
    BoxCollider2D col;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        col = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        //when we press A or D this value will +1 or -1,
        //if we press nothing the value is 0
        if (Input.GetKeyDown(KeyCode.F)&&Time.time>nextSpawnTime&&usagetime>0)
        {
            createPlatform();
            transform.position = initialPlayerLocation;
            health = 100;
            usagetime--;
            nextSpawnTime = Time.time+cooldownTime;
        }
        rb.linearVelocity = new Vector2(moveInput * movementSpeed, rb.linearVelocity.y);

        if ((Input.GetKeyDown(KeyCode.Space)||Input.GetKeyDown(KeyCode.UpArrow) ||Input.GetKeyDown(KeyCode.W)) && (isGrounded || hasDoubleJump))  
        {
            if (!isGrounded)
            {
                hasDoubleJump = false;  
            }
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingForce);
        }

        if (isGrounded)
        {
            hasDoubleJump = true;
            col.sharedMaterial.friction = 1.0f;
        }
        else
        {
            col.sharedMaterial.friction = 0.0f;
        }

        if (transform.position.y < dieHeight || Input.GetKeyDown(KeyCode.R))    
        {
            Die(); 

        }
    }
    private void FixedUpdate()
    {
        //creates an overlap circle to check if the player has touched the ground layer
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Damage")
        {
            health -= 50; // loses 50 health each time we collide with the damage tilemap layer

            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingForce);

            StartCoroutine(BlinkRed());

            if (health <= 0)
            {
                Die();
            }
        }
    }

    private IEnumerator BlinkRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

    private void Die()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        
    }
    private void createPlatform()
    {
        Vector2 spawnLocation = (Vector2)transform.position + new Vector2(0, -0.5f);
        GameObject tempPlatform = Instantiate(platformPrefab, spawnLocation, Quaternion.identity);
        //Destroy(tempPlatform, spawnTime);
    }
}
