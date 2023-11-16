using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Player_Movement : Singleton<Player_Movement>
{
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private float jump_speed = 14f;
    [SerializeField] private float movement_speed = 7f;
    [SerializeField] private TrailRenderer dashTrail;

     [Header("Dash Settings")]
    [SerializeField] float dashSpeed = 10f;
    [SerializeField] float dashDuration = 0.1f;
    [SerializeField] float dashCooldown = 1f;

    [Header("Sounds")]
    [SerializeField] private AudioSource stepSounds;

    [SerializeField] private AudioSource dashSounds;
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator animator;
    private float dirX;
    private Knockback knockback;
    private enum MovementState { idle, running, jumping, falling }

   
    private bool isDashing;
    private Player_Life health;
    private bool canDash = true;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = rb.GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        knockback = GetComponent<Knockback>();
        health = GetComponent<Player_Life>();
    }

    // Update is called once per frame
    void Update()
    {
        if(knockback.gettingKnockedBack || health.isDead || isDashing){return;}

        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * movement_speed, rb.velocity.y);
        stepSounds.Play();
        

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jump_speed);
            
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            dashTrail.emitting = true;
            StartCoroutine(Dash());
        }

        UpdateAnimationState();
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        animator.SetTrigger("dashTrigger");

        float dashDirection = sprite.flipX ? -1 : 1;

        rb.velocity = new Vector2(dashSpeed * dashDirection, rb.velocity.y);
        yield return new WaitForSeconds(dashDuration);
        dashTrail.emitting = false;
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        animator.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}