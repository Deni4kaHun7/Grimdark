using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranger : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject bulletPrefab;

    public void Attack(){
        Vector2 tartgetDirection = Player_Movement.Instance.transform.position - transform.position;

        GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        newBullet.transform.right = tartgetDirection;
    }

    public void FlipColliderDirection(){
        return;
    }
}
