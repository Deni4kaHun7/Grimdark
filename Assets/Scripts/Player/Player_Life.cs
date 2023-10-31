using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Life : MonoBehaviour
{
    [SerializeField] private int startHealth = 3;
    [SerializeField] private float knockBackThrust = 15f;
    [SerializeField] private float deathTime = 1f;
    readonly int KNOCKBACK_HASH = Animator.StringToHash("knockback");
    private int currentHealth;
    private Knockback knockback;
    private Rigidbody2D rb;


    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        knockback = GetComponent<Knockback>();
    }
    void Start()
    {
        currentHealth = startHealth;
    }

    public void TakeDamage(int damage){
        currentHealth -= damage;
        knockback.GetKnockedBack(EnemyAI.Enemy.transform , knockBackThrust);
        GetComponent<Animator>().SetTrigger(KNOCKBACK_HASH);
        Debug.Log(currentHealth);
        DetectDeath();
    }

    public void DetectDeath(){
        if(currentHealth <= 0){
            StartCoroutine(DeathRoutine());
        }
    }

    private IEnumerator DeathRoutine(){
        yield return new WaitForSeconds(deathTime);
        rb.velocity = Vector2.zero;
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            RestartLevel();
        } 
    }

    /*
    private void OnTriggerEnter(Collider other) {
        Debug.Log("12312412");
        if (other.gameObject.GetComponent<DamageSource>()){
            Debug.Log("ssdf");
            knockback.GetKnockedBack(other.transform, knockBackThrust);
        }
    }*/

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
