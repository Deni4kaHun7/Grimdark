using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{   
    [SerializeField] private int damageAmount = 1;
    [SerializeField] private AudioSource swordImpactSound;
    private int damagePlayer;
    private void OnTriggerEnter2D(Collider2D other) {
        swordImpactSound.Play();
        if(other.gameObject.GetComponent<EnemyHealth>()){
            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            damagePlayer = GetComponentInParent<Combat>().damage;
            Debug.Log(damagePlayer);
            enemyHealth.TakeDamage(damagePlayer);
        }else if(other.gameObject.GetComponent<Player_Life>()){
            Player_Life playerHealth = other.gameObject.GetComponent<Player_Life>();
            playerHealth.TakeDamage(damageAmount);
        }
    }
}
