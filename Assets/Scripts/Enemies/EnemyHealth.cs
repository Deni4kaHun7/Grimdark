using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startHealth = 3;
    [SerializeField] private float deathTime = 1f;
    public static EnemyHealth Instance;
    private Rigidbody2D rb;
    public int currentHealth;
    readonly int FLASH_HASH = Animator.StringToHash("flash");
    readonly int DEATH_HASH = Animator.StringToHash("death");

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        Instance = this;
        currentHealth=startHealth;
    }

    public void TakeDamage(int damage){
        currentHealth -= damage;
        GetComponent<Animator>().SetTrigger(FLASH_HASH); 
        DetectDeath();
    }

    public void DetectDeath(){
        if(currentHealth <= 0){
            GetComponent<Animator>().SetTrigger(DEATH_HASH);
            StartCoroutine(DeathRoutine());
        }
    }

    private IEnumerator DeathRoutine(){
        yield return new WaitForSeconds(deathTime);
        rb.velocity = Vector2.zero;
        Destroy(gameObject);
    }
}
