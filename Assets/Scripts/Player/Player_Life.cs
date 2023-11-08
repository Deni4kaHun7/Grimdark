using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player_Life : MonoBehaviour
{
    public bool isDead {get; private set; }

    [SerializeField] private int startHealth = 3;
    [SerializeField] private float knockBackThrust = 15f;
    [SerializeField] private float deathTime = 1f;
    readonly int KNOCKBACK_HASH = Animator.StringToHash("knockback");
    readonly int DEATH_HASH = Animator.StringToHash("death");
    private Animator healthUIAnimator;
    private int currentHealth;
    private Knockback knockback;
    private Rigidbody2D rb;


    public void Awake() {
        //base.Awake();
        healthUIAnimator = GameObject.Find("Image").GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        knockback = GetComponent<Knockback>();
    }
    public void Start()
    {
        isDead = false;
        currentHealth = startHealth;
    }

    public void TakeDamage(int damage){
        currentHealth -= damage;
        ScreenShakeManager.Instance.ShakeScreen();
        knockback.GetKnockedBack(EnemyAI.Instance.transform , knockBackThrust);
        GetComponent<Animator>().SetTrigger(KNOCKBACK_HASH);
        healthUIAnimator.SetTrigger("dmg");
        DetectDeath();
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Enemy")){
            Destroy(g);
        }
    }
}
