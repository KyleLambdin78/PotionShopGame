using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float distance;
    public LayerMask solidObjects;
    private Vector2 target;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, solidObjects);

        
        if(hitInfo.collider != null)
        {
            PlayerController playerController = hitInfo.collider.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.TakeDamage();
                playerController.KnockBack(transform, 5);
                DestroyProjectile();
            }
            DestroyProjectile();
        }
       
    }
 
    public void DestroyProjectile()
    {
        Destroy(this.gameObject);
    }
}
