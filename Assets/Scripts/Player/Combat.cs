using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    [SerializeField] private Transform weaponCollider;
    private PlayerControls playerControls;
    private Animator animator;
    private float dirX;
    private EnemyPathfinding enemyPathfinding;
    private Vector2 moveDir;

    private void Awake() {
        playerControls = new PlayerControls();
        animator = GetComponent<Animator>();
        enemyPathfinding = GetComponent<EnemyPathfinding>();
    }

    private void OnEnable() {
        playerControls.Enable();   
    }

    private void Start() {
        playerControls.Combat.Attack.started += _ => Attack();
    }

    private void Update() {
        FlipColliderDirectionPlayer();
    }

    public void Attack(){
        animator.SetTrigger("attack");

        weaponCollider.gameObject.SetActive(true);
    }

    public void DoneAttackingAnimEvent(){
        weaponCollider.gameObject.SetActive(false);
    }

    private void FlipColliderDirectionPlayer(){
        dirX = Input.GetAxisRaw("Horizontal");

        if (dirX < 0f){
            weaponCollider.transform.rotation = Quaternion.Euler(0, 180, 0);
        } else if (dirX > 0f){
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void FlipColliderDirectionEnemy(){
        moveDir = enemyPathfinding.moveDir;
        if (moveDir.x > 0) {
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        } else if (moveDir.x < 0) {
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
