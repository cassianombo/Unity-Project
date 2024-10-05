using UnityEngine;

public abstract class Animal : MonoBehaviour
{
    #region Attributes

    #region Unity

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    #endregion

    private Vector2 movement;

    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Move();
        AdjuctFlipX();

        void AdjuctFlipX()
        {
            if (movement.x == 0)
                return;

            spriteRenderer.flipX = movement.x < 0;
        }
    }

    protected abstract void Move();

}
