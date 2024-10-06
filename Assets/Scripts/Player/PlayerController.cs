using Assets.Scripts.Constants;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    #region Attributes

    [SerializeField] private float moveSpeed = 1f;

    public static PlayerController Instance;

    public PlayerInteractionArea InteractionArea;

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
        Instance = this;

        playerControls = new IAPlayer();
        playerControls.Enable();

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

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
        DropItem();

        void Move()
        {
            rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
        }

        void AdjustPlayerFacingDirection()
        {
            movement = playerControls.Movement.Move.ReadValue<Vector2>();

            if (movement.x is 0)
                return;

            transform.localScale = new(movement.x < 0 ? -1 : 1, 1);
        }

        void DropItem()
        {
            if (!(Input.GetKeyDown(KeyCode.Q) || Input.GetKey(KeyCode.Q)))
                return;
            InventoryItem inventoryItem = InventoryManager.Instance.GetCurrentInventoryItem();

            if (inventoryItem is null)
                return;

            Item slotItem = inventoryItem.item;

            GameObject newItemGo = Instantiate(InventoryManager.Instance.worldItemPrefab);
            newItemGo.GetComponent<WorldItem>().Item = slotItem;
            newItemGo.GetComponent<WorldItem>().Owner = this.gameObject;
            newItemGo.transform.position = InteractionArea.transform.position;
            
            Destroy(inventoryItem.gameObject);
        }
    }

}
