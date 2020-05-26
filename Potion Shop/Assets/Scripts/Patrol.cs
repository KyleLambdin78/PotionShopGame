using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float speed;
    public float distance;
    public Transform groundDetection;
    private bool movingRight = true;
    private LayerMask wallMask;

    private void Start()
    {
        wallMask = LayerMask.GetMask("wall");
        StartCoroutine(PatrolArea());
    }
    void Update()
    {
        
    }
    private IEnumerator PatrolArea()
    {
        while (true)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
            RaycastHit2D wallInfo = Physics2D.Raycast(groundDetection.position, -Vector2.right, distance, wallMask);
            if (groundInfo.collider == false || wallInfo.collider == true)
            {
                if (movingRight == true)
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    movingRight = false;
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    movingRight = true;
                }

            }
            yield return null;
        }
        
    }
}
