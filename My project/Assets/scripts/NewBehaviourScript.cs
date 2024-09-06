using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed = 10f;
    public Rigidbody2D rb;
    private bool isOnGround;
    private BoxCollider2D _collider;
    public LayerMask GroundLayer;
    public float jumpCooldownCounter;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _collider = rb.GetComponent<BoxCollider2D>();
        rb.gravityScale = 3.5f;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the distance to check below the player
        float distance = _collider.bounds.extents.y + 0.1f;

        // Move the player horizontally
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rb.velocity.y);

        // Perform the raycast downward from the center of the collider
 

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distance, GroundLayer);

        
        isOnGround = hit.collider != null ;

        Debug.Log("Raycast hit: " + hit.collider.name);

        if (jumpCooldownCounter > 0.05)
        {
            jumpCooldownCounter -= Time.deltaTime;
        }

        // Jump if on the ground and the player presses space or W
        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W)) && isOnGround && jumpCooldownCounter >= -0.05f && jumpCooldownCounter <= 0.05f)
        {
            rb.AddForce(Vector2.up * 25f, ForceMode2D.Impulse);
           
            Debug.Log("Can't jump");

            jumpCooldownCounter = 0.1f;
        }
        else if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Can jump");
        }
        Debug.Log(jumpCooldownCounter);
    }

}
