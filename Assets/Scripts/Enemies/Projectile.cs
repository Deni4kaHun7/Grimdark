using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 22f;

    private void Update() {
        MoveProjectile();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Player_Life playerHealth = other.gameObject.GetComponent<Player_Life>();
        playerHealth?.TakeDamage(1);
        Destroy(gameObject);
    }

    private void MoveProjectile(){
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
    }
}