using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandit : MonoBehaviour, IEnemy
{
    [SerializeField] private Transform weaponCollider;
    private Animator animator;
    private EnemyPathfinding enemyPathfinding;
    private Vector2 moveDir;

    private void Awake() {
        animator = GetComponent<Animator>();
        enemyPathfinding = GetComponent<EnemyPathfinding>();
    }

    private void Update() {
        FlipColliderDirection();
    }

    public void Attack(){
        animator.SetTrigger("attack");
    }

    public void StartAttackingAnimEvent(){
        weaponCollider.gameObject.SetActive(true);
    }

    public void DoneAttackingAnimEvent(){
        weaponCollider.gameObject.SetActive(false);
    }

    
    public void FlipColliderDirection(){
        moveDir = enemyPathfinding.moveDir;
        if (moveDir.x > 0) {
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        } else if (moveDir.x < 0) {
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
