using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{   
    [SerializeField] private float attackCoolDown = 2f;
    [SerializeField] private float detectPlayerRange = 1.6f;
    [SerializeField] private float attackRange = 1.1f;
    [SerializeField] private MonoBehaviour enemyType;
    public static EnemyAI Instance;
    private Vector2 leftDir = new Vector2(-1f, 0f);
    private Vector2 rightDir = new Vector2(1f, 0f);
    public bool canAttack = true;
    private bool canRoam = true;
    private enum State{
        Roaming,
        Attacking
    }
    private Vector2 roamPosition;
    private State state;
    private EnemyPathfinding enemyPathfinding;

    private void Awake() {
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        state = State.Roaming;
    }

    private void Start() {
        Instance = this;
        roamPosition = leftDir;
    }

    private void Update() {
        MovementStateControl();
    }

    private void MovementStateControl(){
        switch (state){
            case State.Roaming:
                Roaming();
            break;

            case State.Attacking:
                Attacking();
            break;
        }
    }

    private void Roaming(){
        if(canRoam){
            canRoam = false;
            enemyPathfinding.MoveTo(roamPosition);
            StartCoroutine(RoamingCoolDownRoutine());
        }

        if(Vector2.Distance(Player_Movement.Instance.transform.position, transform.position) < detectPlayerRange){
            roamPosition = Player_Movement.Instance.transform.position - transform.position;
            state = State.Attacking;
            }
    }

    private IEnumerator RoamingCoolDownRoutine(){
        yield return new WaitForSeconds(1f);
        enemyPathfinding.StopMoving();
        yield return new WaitForSeconds(2f);

        if(roamPosition == leftDir){
            roamPosition = rightDir;
        }else{
            roamPosition = leftDir;
        }
        canRoam = true;
    }

    private void Attacking(){  
        if(canAttack && EnemyHealth.Instance.currentHealth > 0){
            enemyPathfinding.MoveTo(roamPosition);
            canAttack = false;
            (enemyType as IEnemy).Attack();
            
            StartCoroutine(AttackCoolDownRoutine());
        }
        
        if(Vector2.Distance(Player_Movement.Instance.transform.position, transform.position) < attackRange){
            Debug.Log("fsdf");
            (enemyType as IEnemy).FlipColliderDirection();
            enemyPathfinding.ChangeSpriteDir();
            enemyPathfinding.StopMoving();
        }

        if(Vector2.Distance(Player_Movement.Instance.transform.position, transform.position) > detectPlayerRange){
            state = State.Roaming;
        }
    }

    private IEnumerator AttackCoolDownRoutine(){
        yield return new WaitForSeconds(attackCoolDown);
        roamPosition = Player_Movement.Instance.transform.position - transform.position;
        canAttack = true;
    }
}
