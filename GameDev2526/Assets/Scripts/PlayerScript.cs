using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //attributes
    public float movementSpeed = 5f;
    public float jumpingForce = 10f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;


    private Rigidbody2D rb;
    private bool isGrounded;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        //when we press A or D this value will +1 or -1,
        //if we press nothing the value is 0

        rb.linearVelocity = new Vector2(moveInput * movementSpeed, rb.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.Space)||Input.GetKeyDown(KeyCode.UpArrow) ||Input.GetKeyDown(KeyCode.W) && isGrounded==true)  
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingForce);
        }

    }
    private void FixedUpdate()
    {
        //creates an overlap circle to check if the player has touched the ground layer
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

}
