using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float speedEnemy = 0.94f;
    [SerializeField] private bool vertical = true;
    [SerializeField] private float changeTime = 1.26f;
    [SerializeField] private ParticleSystem smokeEffect;
    Rigidbody2D rigidbody2D;
    Animator animator;
    float timer;
    int direction = 1;
    bool isBroken = true;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }

    void FixedUpdate()
    {
        if (!isBroken)
        {
            return;
        }
        Vector2 position = rigidbody2D.position;

        if (vertical)
        {
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
            position.y = position.y + Time.deltaTime * speedEnemy * direction; ;
        }
        else
        {
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
            position.x = position.x + Time.deltaTime * speedEnemy * direction; ;
        }

        rigidbody2D.MovePosition(position);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }
    public void Fix() 
    {
        isBroken = false;
        rigidbody2D.simulated = false; //This removes the Rigidbody from the Physics System simulation, so it won’t be taken into account by the system for collision, and the fixed robot won’t stop the Projectile anymore or be able to hurt the main character.
        animator.SetBool("Fixed", true);
        smokeEffect.Stop();
    }
}
