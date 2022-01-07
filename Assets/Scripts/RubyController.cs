using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    [SerializeField] private float speed = 5 ;
    Rigidbody2D rigidbody2d;
    Animator rubyAnimator;
    public GameObject projectilePrefab;
    private float horizontalInput;
    private float verticalInput;

    public int maxHealth = 5;
    public int currentHealth;
    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;
    Vector2 lookDirection = new Vector2(1, 0);

    public int health 
    { 
        get 
        { 
            return currentHealth; 
        } 
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        rubyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontalInput, verticalInput);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        rubyAnimator.SetFloat("Look X", lookDirection.x);
        rubyAnimator.SetFloat("Look Y", lookDirection.y);
        rubyAnimator.SetFloat("Speed", move.magnitude);

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            Launch();
        } 
        
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + 3.0f * horizontalInput * Time.deltaTime;
        position.y = position.y + 3.0f * verticalInput * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {

        if (amount < 0)
        {
            rubyAnimator.SetTrigger("Hit");
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }
    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        rubyAnimator.SetTrigger("Launch");
    }

}
