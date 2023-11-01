using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{   
    [SerializeField] private float attackCoolDown = 2f;
    [SerializeField] private float detectPlayerRange = 1.6f;
    [SerializeField] private float attackRange = 1.1f;
    private Vector2 leftDir = new Vector2(-1f, 0f);
    private Vector2 rightDir = new Vector2(1f, 0f);
    private Bandit bandit;
    private bool canAttack = true;
    private bool canRoam = true;
    public static EnemyAI Enemy;
    //enum is like a list of different states that enemy can have. Later I will use it to tell my enemy what to do if he has a specific type of State
    private enum State{
        Roaming,
        Attacking
    }

    private Vector2 roamPosition;
    private float timeRoaming = 0f;
    private State state;
    private EnemyPathfinding enemyPathfinding;

    private void Awake() {
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        bandit = GetComponent<Bandit>();
        state = State.Roaming;
        Enemy = this;
    }

    private void Start() {
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
        if(canAttack){
            canAttack = false;
            enemyPathfinding.MoveTo(roamPosition);
            bandit.Attack();
            
            StartCoroutine(AttackCoolDownRoutine());
        }
        
        if(Vector2.Distance(Player_Movement.Instance.transform.position, transform.position) < attackRange){
            bandit.FlipColliderDirection();
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
