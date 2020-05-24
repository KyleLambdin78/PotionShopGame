using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public BoxCollider2D groundCheck;
    private LayerMask floorMask;
    private Rigidbody2D rb;
    void Start()
    {
        floorMask = LayerMask.GetMask("Floor");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Jump();
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * speed;
    }
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && groundCheck.IsTouchingLayers(floorMask))
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
        
    }
}
