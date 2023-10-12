using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startHealth = 3;
    private int currentHealth;

    private void Start() {
        currentHealth=startHealth;
    }

    public void TakeDamage(int damage){
        currentHealth -= damage;
        Debug.Log(currentHealth);
    }
}
