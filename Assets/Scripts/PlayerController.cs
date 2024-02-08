using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed, jumpSpeed;
    [SerializeField] private LayerMask ground;

    private PlayerActionsControls playerActionsControls;
    private Rigidbody2D rb;
    private Collider2D col;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        playerActionsControls = new PlayerActionsControls();
        rb = GetComponent<Rigidbody2D>();
        col = rb.GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        playerActionsControls.Enable();
    }

    private void OnDisable()
    {
        playerActionsControls.Disable();

    }

    void Start()
    {
        playerActionsControls.Land.Jump.performed += _ => Jump();
    }

    private void Jump()
    {
        if (IsGrounded())
        {      
            rb.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
            animator.SetTrigger("Jump");
        }

        Debug.Log(IsGrounded());
        

       
    }

    private bool IsGrounded()
    {
        Vector2 topLeftPoint = transform.position;
        topLeftPoint.x -= col.bounds.extents.x;
        topLeftPoint.y += col.bounds.extents.y;

        Vector2 bottomRightPoint = transform.position;
        bottomRightPoint.x += col.bounds.extents.x;
        bottomRightPoint.y -= col.bounds.extents.y;

        return Physics2D.OverlapArea(topLeftPoint, bottomRightPoint, ground);
    }

    void Update()
    {
        Move();
    }

    private void Move() 
    {
        // Read the movement value
        float movementInput = playerActionsControls.Land.Move.ReadValue<float>();

        // Move the player
        Vector3 currentPosition = transform.position;
        currentPosition.x += movementInput * speed * Time.deltaTime;
        transform.position = currentPosition;

        // Animation
        if (movementInput != 0) animator.SetBool("Run", true);
        else animator.SetBool("Run", false);

        //Sprite Flip
        if (movementInput == -1)
            spriteRenderer.flipX = true;
        else if (movementInput == 1)
            spriteRenderer.flipX = false;
    }
}
