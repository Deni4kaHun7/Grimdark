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
    readonly int DEATH_HASH = Animator.StringToHash("death");
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
        ScreenShakeManager.Instance.ShakeScreen();
        knockback.GetKnockedBack(EnemyAI.Instance.transform , knockBackThrust);
        GetComponent<Animator>().SetTrigger(KNOCKBACK_HASH);
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
        RestartLevel();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            RestartLevel();
        } 
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
