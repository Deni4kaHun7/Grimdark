using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{   
    [SerializeField] private float changeRoamingDirFloat = 2f;
    //[SerializeField] private MonoBehaviour enemyType;
    [SerializeField] private float attackCoolDown = 2f;
    [SerializeField] private float attackRange = 1f;
    
    private bool canAttack = true;
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
        //Default state of our enemy. He just roames the world
        state = State.Roaming;
    }

    private void Start() {
        roamPosition = GetRoamingPosition();
    }

    private void Update() {
        MovementStateControl();
    }

    /*
    private IEnumerator RoamingRoutine() {
        while (state == State.Roaming)
        {
            Vector2 roamPosition = GetRoamingPosition();
            enemyPathfinding.MoveTo(roamPosition);
            yield return new WaitForSeconds(2f);
        }
    }*/

    private void MovementStateControl(){
        switch (state){
            default:

            case State.Roaming:
                Roaming();
            break;

            case State.Attacking:
                Attacking();
            break;
        }
    }

    private void Roaming(){
        timeRoaming += Time.deltaTime;

        enemyPathfinding.MoveTo(roamPosition);

        if(Vector2.Distance(Player_Movement.Instance.transform.position, transform.position) < attackRange){
            state = State.Attacking;
        }

        if(timeRoaming > changeRoamingDirFloat){
            roamPosition = GetRoamingPosition();
        }
        Debug.Log("roaming");
    }

    private Vector2 GetRoamingPosition(){
        timeRoaming = 0f;
        return new Vector2(Random.Range(-5f,5f), 0).normalized;
    }

    private void Attacking(){
        Debug.Log("attacking");
        timeRoaming += Time.deltaTime;

        enemyPathfinding.MoveTo(roamPosition);

        if(Vector2.Distance(Player_Movement.Instance.transform.position, transform.position) > attackRange){
            state = State.Roaming;
        }

        if(timeRoaming > changeRoamingDirFloat){
            roamPosition = Player_Movement.Instance.transform.position - transform.position;
        }

        /*
        if(canAttack){
            canAttack = false;
            //(enemyType as IEnemy).Attack();

            StartCoroutine(AttackCoolDownRoutine());
        }*/
    }

    private IEnumerator AttackCoolDownRoutine(){
        yield return new WaitForSeconds(attackCoolDown);
        canAttack = true;
    }
}
