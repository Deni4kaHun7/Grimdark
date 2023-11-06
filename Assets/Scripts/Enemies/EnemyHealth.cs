using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Singleton<EnemyHealth>
{
    [SerializeField] private int startHealth = 3;
    [SerializeField] private float knockBackThrust = 15f;
    [SerializeField] private float deathTime = 1f;
    private Rigidbody2D rb;
    public int currentHealth;
    private Knockback knockback;
    readonly int FLASH_HASH = Animator.StringToHash("flash");
    readonly int DEATH_HASH = Animator.StringToHash("death");

    protected override void Awake() {
        base.Awake();
        
        knockback = GetComponent<Knockback>();    
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        currentHealth=startHealth;
    }

    public void TakeDamage(int damage){
        currentHealth -= damage;
        GetComponent<Animator>().SetTrigger(FLASH_HASH); 
        //knockback.GetKnockedBack(Player_Movement.Instance.transform , knockBackThrust);
        DetectDeath();
    }

    public void DetectDeath(){
        if(currentHealth <= 0){
            //EnemyAI.Instance.canAttack = false;
            //Debug.Log(EnemyAI.Instance.canAttack);
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
