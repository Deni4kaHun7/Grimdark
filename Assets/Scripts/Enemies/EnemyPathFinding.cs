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
    
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        knockback = GetComponent<Knockback>();   
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate() {
        if(knockback.gettingKnockedBack){ return ;}

        rb.MovePosition(rb.position + moveDir * (Time.deltaTime * movementSpeed));
        Debug.Log(moveDir);
        if (moveDir.x < 0) {
            spriteRenderer.flipX = true;
        } else if (moveDir.x > 0) {
            spriteRenderer.flipX = false;
        }

    }

    public void MoveTo(Vector2 targetedDir){
        moveDir = targetedDir;
    }
}