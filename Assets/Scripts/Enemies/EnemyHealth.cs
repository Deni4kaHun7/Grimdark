using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startHealth = 3;
    [SerializeField] private float knockBackThrust = 15f;
    private int currentHealth;
    private Knockback knockback;
    readonly int FLASH_HASH = Animator.StringToHash("flash");

    private void Awake() {
        knockback = GetComponent<Knockback>();    
    }

    private void Start() {
        currentHealth=startHealth;
    }

    public void TakeDamage(int damage){
        currentHealth -= damage;
        Debug.Log(currentHealth);
        GetComponent<Animator>().SetTrigger(FLASH_HASH);
        knockback.GetKnockedBack(Player_Movement.Instance.transform , knockBackThrust);
        DetectDeath();
    }

    public void DetectDeath(){
        if(currentHealth <= 0){
            Destroy(gameObject);
        }
    }
}
