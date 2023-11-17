using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player_Life : MonoBehaviour
{
    

    [SerializeField] public int startHealth = 3;
    [SerializeField] private float knockBackThrust = 15f;
    [SerializeField] private float deathTime = 1f;
    public bool canTakeDamage;
    public bool isDead {get; private set; }
    readonly int KNOCKBACK_HASH = Animator.StringToHash("knockback");
    readonly int DEATH_HASH = Animator.StringToHash("death");
    private Animator healthUIAnimator;
    public int currentHealth;
    private Knockback knockback;
    private Rigidbody2D rb;
    public static Player_Life Instance;


    public void Awake() {
        healthUIAnimator = GameObject.Find("Image").GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        knockback = GetComponent<Knockback>();
    }

    public void Start()
    {
        Instance = this;
        isDead = false;
        canTakeDamage = true;
        currentHealth = startHealth;
    }

    public void TakeDamage(int damage){
        if (canTakeDamage){
            currentHealth -= damage;
            ScreenShakeManager.Instance.ShakeScreen();
            knockback.GetKnockedBack(EnemyAI.Instance.transform , knockBackThrust);
            GetComponent<Animator>().SetTrigger(KNOCKBACK_HASH);
            healthUIAnimator.SetTrigger("dmg");
            DetectDeath();
        }

    }

    public void DetectDeath(){
        if(currentHealth <= 0 && !isDead){
            isDead = true;
            currentHealth = 0;
            GetComponent<Animator>().SetTrigger(DEATH_HASH);
            StartCoroutine(DeathLoadSceneRoutine());
        }
    }

    private IEnumerator DeathLoadSceneRoutine(){
        yield return new WaitForSeconds(deathTime);
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
        Destroy(gameObject);
        Destroy(GameObject.FindGameObjectWithTag("Respawn"));
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Enemy")){
            Destroy(g);
        }
    }
}
