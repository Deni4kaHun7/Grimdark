using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{   
    [SerializeField] private float changeRoamingDirFloat = 2f;
    [SerializeField] private float attackCoolDown = 2f;
    [SerializeField] private float roamingCoolDown = 10f;
    [SerializeField] private float attackRange = 1f;
    private Combat combat;
    private bool canAttack = true;
    private bool canRoam = true;
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
        combat = GetComponent<Combat>();
        state = State.Roaming;
    }

    private void Start() {
        roamPosition = GetRoamingPosition();
    }

    private void Update() {
        MovementStateControl();
        combat.FlipColliderDirectionEnemy();
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
            Debug.Log("fsfd"); 
            StartCoroutine(RoamingCoolDownRoutine());
        }

        if(Vector2.Distance(Player_Movement.Instance.transform.position, transform.position) < attackRange){
            roamPosition = Player_Movement.Instance.transform.position - transform.position;
            state = State.Attacking;
            }
    }

    private IEnumerator RoamingCoolDownRoutine(){
        Debug.Log("routine");
        yield return new WaitForSeconds(roamingCoolDown);
        
        roamPosition = GetRoamingPosition();
        canRoam = true;
    }

    private Vector2 GetRoamingPosition(){
        timeRoaming = 0f;
        enemyPathfinding.StopMoving();
        return new Vector2(Random.Range(-.1f,.1f), 0).normalized;
    }

    private void Attacking(){
        timeRoaming += Time.deltaTime;
        enemyPathfinding.MoveTo(roamPosition);

        if(Vector2.Distance(Player_Movement.Instance.transform.position, transform.position) > attackRange){
            state = State.Roaming;
        }
        
        if(timeRoaming > changeRoamingDirFloat){
            roamPosition = Player_Movement.Instance.transform.position - transform.position;
        }

        if(canAttack){
            canAttack = false;
            combat.Attack();
            StartCoroutine(AttackCoolDownRoutine());
        }
    }

    private IEnumerator AttackCoolDownRoutine(){
        yield return new WaitForSeconds(attackCoolDown);
        canAttack = true;
    }
}
