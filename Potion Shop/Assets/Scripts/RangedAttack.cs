using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    private float timeBTWShots;
    public float startTimeBTWShots;
    public GameObject projectile;
    public float offset;
    public Transform shotPoint;
    private Transform player;
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerController>().attackPos;
        timeBTWShots = startTimeBTWShots;
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            Vector3 difference = player.position - transform.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
            shotPoint.rotation = transform.rotation;
            if (timeBTWShots <= 0)
            {
                Instantiate(projectile, shotPoint.position, Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z + offset));
                timeBTWShots = startTimeBTWShots;
            }
            else
            {
                timeBTWShots -= Time.deltaTime;
            }
        }
       

    }
}
