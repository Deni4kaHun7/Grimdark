using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranger : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject bulletPositionLeft;
    [SerializeField] private GameObject bulletPositionRight;
    private GameObject newBullet;
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
        if(bulletPositionLeft.activeSelf == true){
            newBullet = Instantiate(bulletPrefab, bulletPositionLeft.transform.position, Quaternion.identity);
        }else {
            newBullet = Instantiate(bulletPrefab, bulletPositionRight.transform.position, Quaternion.identity);
        }
        animator.SetTrigger("attack");
        Vector2 tartgetDirection = Player_Movement.Instance.transform.position - transform.position;

        
        newBullet.transform.right = tartgetDirection;
    }

    public void FlipColliderDirection(){
        moveDir = enemyPathfinding.moveDir;
        if (moveDir.x > 0) {
            bulletPositionLeft.SetActive(false);
            bulletPositionRight.SetActive(true);
        } else if (moveDir.x < 0) {
            bulletPositionLeft.SetActive(true);
            bulletPositionRight.SetActive(false);
        }
    }
}
