using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 2f;
    private Rigidbody2D rb;
    public Vector2 moveDir {get; private set;}
    private Knockback knockback;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        knockback = GetComponent<Knockback>();   
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate() {
        if(knockback.gettingKnockedBack){ return ;}

        rb.MovePosition(rb.position + moveDir * (Time.deltaTime * movementSpeed));
        animator.SetFloat("moveX", moveDir.x);
        ChangeSpriteDir();
    }

    public void MoveTo(Vector2 targetedDir){
        Debug.Log("called moveto "+ targetedDir);
        moveDir = targetedDir;
        
    }

    public void StopMoving() {
        moveDir = Vector3.zero;
    }

    private void ChangeSpriteDir(){
        Debug.Log("moveDir "+ moveDir);
        if (moveDir.x < 0) {
            spriteRenderer.flipX = true;
        } else if (moveDir.x > 0) {
            spriteRenderer.flipX = false;
        }
    }
}