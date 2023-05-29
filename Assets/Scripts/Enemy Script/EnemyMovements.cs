using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovements : MonoBehaviour
{
    float speed;
    Rigidbody2D rb;
    Animator anim;


    public bool isStatic;
    public bool isWalker;
    public bool walksRight;

    public Transform wallCheck, pitCheck, groundCheck;
    public bool walldetected, pitDetected, isGround;
    public float detectionRadius;
    public LayerMask whatIsGround;

    // Start is called before the first frame update
    void Start()
    {
        speed = GetComponent<Enemy>().speed;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        pitDetected = !Physics2D.OverlapCircle(pitCheck.position, detectionRadius,whatIsGround);
        walldetected = Physics2D.OverlapCircle(wallCheck.position, detectionRadius,whatIsGround);

        if(pitDetected || walldetected)
        {
            Flip();
        }
    }

    private void FixedUpdate() 
    {
        if(isStatic)
        {
            anim.SetBool("Idle", true);
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        if(isWalker)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;

            if(!walksRight)
            {
                rb.velocity = new Vector2(-speed * Time.deltaTime, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(speed * Time.deltaTime, rb.velocity.y);
            }
        }
    }


    public void Flip()
    {
        walksRight = !walksRight;
        transform.localScale *= new Vector2(-1, transform.localScale.y);
    }
}
