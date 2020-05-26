using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public Transform attackPos;
    public float attackRange;
    public float startTimeBTWATK;
    public List<GameObject> hearts;
    public BoxCollider2D groundCheck;
    public GameObject resultScreen;
    private Animator playerAnimation;
    private SpriteRenderer playerRenderer;
    private BoxCollider2D boxCollider;
    private float horizontalMove;
    private LayerMask enemiesOnly;
    private LayerMask floorMask;
    private Rigidbody2D rb;
    private float timeBTWATK;
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        playerRenderer = GetComponent<SpriteRenderer>();
        playerAnimation = GetComponent<Animator>();
        enemiesOnly = LayerMask.GetMask("enemies");
        floorMask = LayerMask.GetMask("Floor");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerAnimation.GetBool("Dead") == false)
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
            if (horizontalMove < 0)
            {
                transform.localScale = new Vector2(-1, transform.localScale.y);
            }
            else if (horizontalMove > 0)
            {
                transform.localScale = new Vector2(1, transform.localScale.y);
            }
            Vector3 movement = new Vector3(horizontalMove, 0f, 0f);
            transform.position += movement * Time.deltaTime;


        }
        
    }
    public void TakeDamage()
    {
        if(playerAnimation.GetBool("Dead") == false)
        {
            List<GameObject> activeHearts = new List<GameObject>();
            for (int i = 0; i < hearts.Count; i++)
            {
                if (hearts[i].activeSelf == true)
                {
                    activeHearts.Add(hearts[i]);
                }
            }

            if (activeHearts.Count - 1 != 0)
            {
                activeHearts[activeHearts.Count - 1].SetActive(false);
            }
            else
            {
                activeHearts[activeHearts.Count - 1].SetActive(false);
                StartCoroutine(GameOver());
            }
        }
       
    }
    public void KnockBack(Transform enemyPosition, float force)
    {
        if (playerAnimation.GetBool("Dead") == false)
        {
            
            Vector2 difference = transform.position - enemyPosition.position;
            rb.AddForce(difference * force, ForceMode2D.Impulse);
        }
        
           
    }
  
    public IEnumerator GameOver()
    {
        rb.gravityScale = 0;
        boxCollider.enabled = false;
        resultScreen.SetActive(true);
        playerAnimation.SetBool("Dead", true);
        yield return new WaitForSeconds(1);

        Destroy(this.gameObject);

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
            playerAnimation.SetBool("isJumping", false);
            playerAnimation.SetTrigger("Attack");
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemiesOnly);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<EnemyController>().TakeDamage();
            }
            timeBTWATK = startTimeBTWATK;
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
