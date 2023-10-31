using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{   
    [SerializeField] private int damageAmount = 1;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.GetComponent<EnemyHealth>()){
            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(damageAmount);
        }else if(other.gameObject.GetComponent<Player_Life>()){
            Player_Life playerHealth = other.gameObject.GetComponent<Player_Life>();
            playerHealth.TakeDamage(damageAmount);
        }
    }
}