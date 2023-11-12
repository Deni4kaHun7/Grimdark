using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranger : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject bulletPosition;
    private EnemyPathfinding enemyPathfinding;
    private Animator animator;
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
        Vector2 tartgetDirection = Player_Movement.Instance.transform.position - transform.position;

        GameObject newBullet = Instantiate(bulletPrefab, bulletPosition.transform.position, Quaternion.identity);
        newBullet.transform.right = tartgetDirection;
    }

    public void FlipColliderDirection(){
        moveDir = enemyPathfinding.moveDir;
        if (moveDir.x > 0) {
            bulletPosition.transform.position = new Vector3(0.5f, 0f, 0f);
        } else if (moveDir.x < 0) {
            bulletPosition.transform.position = new Vector3(-0.5f, 0f, 0f);
        }
    }
}
