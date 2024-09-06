using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    private  bool isFacingRight = true;
    public float jumpCooldownCounter;
    private BoxCollider2D _collider;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    public LayerMask GroundLayer;

    // Update is called once per frame
    void Start()
    {
        _collider = rb.GetComponent<BoxCollider2D>();
        
    }
    void Update()
    {
        if (jumpCooldownCounter > 0.05)
        {
            jumpCooldownCounter -= Time.deltaTime;
        }
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButton("Jump") && IsGrounded() && jumpCooldownCounter >= -0.05f && jumpCooldownCounter <= 0.05f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            jumpCooldownCounter = 0.1f;
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        Flip();
        float distance = _collider.bounds.extents.y + 0.1f;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }
    private bool IsGrounded()
    {
        float distance = _collider.bounds.extents.y + 0.1f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distance, GroundLayer);
        return hit.collider != null;
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
