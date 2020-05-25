using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public Transform attackPos;
    public float attackRange;
    public float startTimeBTWATK;
    public BoxCollider2D groundCheck;
    private Animator playerAnimation;
    private SpriteRenderer playerRenderer;
    private float horizontalMove;
    private LayerMask enemiesOnly;
    private LayerMask floorMask;
    private Rigidbody2D rb;
    private float timeBTWATK;
    void Start()
    {
        playerRenderer = GetComponent<SpriteRenderer>();
        playerAnimation = GetComponent<Animator>();
        enemiesOnly = LayerMask.GetMask("enemies");
        floorMask = LayerMask.GetMask("Floor");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        #region
        if (timeBTWATK <= 0)
        {
            Attack();       
        }
        else
        {
            timeBTWATK -= Time.deltaTime;
        }
        #endregion
        Jump();
        horizontalMove = Input.GetAxis("Horizontal") * speed;
        playerAnimation.SetFloat("Speed", Mathf.Abs(horizontalMove));
        if(horizontalMove < 0)
        {
            playerRenderer.flipX = true;
        }
        else if (horizontalMove > 0)
        {
            playerRenderer.flipX = false;
        }
        Vector3 movement = new Vector3(horizontalMove, 0f, 0f);
        transform.position += movement * Time.deltaTime;

        //Melee attack logic
    
    }
    private void LateUpdate()
    {
       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (groundCheck.IsTouchingLayers(floorMask) && playerAnimation.GetBool("isJumping") == true)
        {
            playerAnimation.SetBool("isJumping", false);
        }
        
    }
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && groundCheck.IsTouchingLayers(floorMask))
        {
             playerAnimation.SetBool("isJumping", true);
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
        
    }
    public void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerAnimation.SetTrigger("Attack");
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemiesOnly);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<EnemyController>().TakeDamage();
            }
            timeBTWATK = startTimeBTWATK;
        }
    }
    public void EndAttack()
    {
        playerAnimation.SetBool("isAttacking", false);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
