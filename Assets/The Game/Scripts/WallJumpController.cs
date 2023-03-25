using UnityEngine;

public class WallJumpController : MonoBehaviour
{
    [SerializeField] private float jumpHeight;
    [SerializeField] private float wallSlideSpeed;

    [SerializeField] private bool isWallSliding = false;
    private bool isJumping = false;
    private float wallDirX;

    private Rigidbody2D rb2d;
    private CircleCollider2D col;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
    }

    void FixedUpdate()
    {
        // Wall slide
        if (IsTouchingWall(gameObject) && rb2d.velocity.y < 0)
        {
            isWallSliding = true;
            rb2d.velocity = new Vector2(rb2d.velocity.x, -wallSlideSpeed);
        }
        else
        {
            isWallSliding = false;
        }

        // Wall jump
        if (isJumping && IsTouchingWall(gameObject))
        {
            rb2d.velocity = new Vector2(-wallDirX * jumpHeight, jumpHeight);
            isJumping = false;
        }
    }

    void Update()
    {
        // Check if player is pressing jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // If wall sliding, jump in opposite direction
            if (isWallSliding)
            {
                wallDirX = Mathf.Sign(rb2d.velocity.x);
                isJumping = true;
            }
        }
    }

    private bool IsTouchingWall(GameObject obj)
    {
        if (obj == null)//null check
            return false;

        // Check if the player is touching a wall in the given direction
        Collider2D[] colliders = new Collider2D[1];
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(GetWallLayerMask());

        //return col.OverlapCollider(contactFilter, colliders) > 0;
        return obj.GetComponent<Collider2D>().OverlapCollider(contactFilter, colliders) > 0;
    }

    private LayerMask GetWallLayerMask()
    {
        return LayerMask.GetMask("Wall");
    }
}
