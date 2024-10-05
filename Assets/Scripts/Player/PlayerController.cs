using Assets.Scripts.Constants;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    #region Attributes

    [SerializeField] private float moveSpeed = 1f;
    //private Inventory inventory;

    #region Unity 
    private IAPlayer playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    #endregion

    #endregion

    // Start is called before the first frame update
    void Awake()
    {
        playerControls = new IAPlayer();
        playerControls.Enable();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        //inventory = new();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();

        void PlayerInput()
        {
            movement = playerControls.Movement.Move.ReadValue<Vector2>();

            animator.SetBool(CONSTANTS.IS_MOVING, movement.x != 0 || movement.y != 0);


        }
    }

    private void FixedUpdate()
    {
        Move();
        AdjustPlayerFacingDirection();

        void Move()
        {
            rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
        }

        void AdjustPlayerFacingDirection()
        {
            movement = playerControls.Movement.Move.ReadValue<Vector2>();

            if (movement.x is 0)
                return;

            spriteRenderer.flipX = movement.x < 0;
        }
    }


}
