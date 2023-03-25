using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private bool allowedToJump;
    [SerializeField] private bool touchingWall;

    private RaycastHit2D landingHit;
    private RaycastHit2D leftHit;
    private RaycastHit2D rightHit;
    private RaycastHit2D topHit;
    private CircleCollider2D playerColider;

    float rightPositionX;
    float topPositionY;
    float bottomPositionY;
    float leftPositionX;
    Rigidbody2D playerBody;


    void Start()
    {
        allowedToJump = true;

        playerBody = GetComponent<Rigidbody2D>();
        playerColider = GetComponent<CircleCollider2D>();
        rightPositionX = playerColider.bounds.max.x + .1f;
        topPositionY = playerColider.bounds.max.y + .1f;
        bottomPositionY = playerColider.bounds.min.y - .1f;
        leftPositionX = playerColider.bounds.min.x - .1f;

    }

    void Update()
    {
        //GetInput();
        CheckLanding();
        CheckLeftHit();
        CheckRightHit();
        CheckTopHit();
        CheckTouchingWall();
    }

    private void CheckTouchingWall()
    {
        if (touchingWall)
        {
            allowedToJump = true;
        }
        if (rightHit.collider == null && leftHit.collider == null)
        {
            touchingWall = false;
        }
    }

    private void CheckTopHit()
    {
        topHit = Physics2D.Raycast(new Vector2(this.transform.position.x, topPositionY + transform.position.y), Vector2.up, 0.2f);
        if (topHit.collider != null && topHit.collider.tag == "floor")
        {
            allowedToJump = false;
            Debug.Log("Hit the top");
        }

    }

    private void CheckRightHit()
    {
        rightHit = Physics2D.Raycast(new Vector2(rightPositionX + transform.position.x, this.transform.position.y), Vector2.right, 0.2f);
        if (rightHit.collider != null && rightHit.collider.tag == "wall")
        {
            touchingWall = true;
        }
    }

    private void CheckLeftHit()
    {
        leftHit = Physics2D.Raycast(new Vector2(leftPositionX + transform.position.x, this.transform.position.y), Vector2.left, 0.2f);
        if (leftHit.collider != null && leftHit.collider.tag == "wall")
        {
            touchingWall = true;
        }
    }

    private void CheckLanding()
    {
        landingHit = Physics2D.Raycast(new Vector2(this.transform.position.x, bottomPositionY + transform.position.y), Vector2.down, 0.2f);
        if (landingHit.collider != null && landingHit.collider.tag == "floor")
        {
            allowedToJump = true;
            Debug.Log("Hit the floor");
        }
    }

    private void GetInput()
    {
        float direction = Input.GetAxisRaw("Horizontal");
        float move = 0.0f;

        if (direction != 0.0f)
        {
            move = direction * speed;
            playerBody.velocity = new Vector2(move, playerBody.velocity.y);
        }
        if (Input.GetKey(KeyCode.Space) && allowedToJump)
        {
            playerBody.velocity = new Vector2(playerBody.velocity.x, jumpHeight);
            allowedToJump = false;
        }
    }
}