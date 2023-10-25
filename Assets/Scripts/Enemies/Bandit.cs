using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandit : MonoBehaviour, IEnemy
{   
    [SerializeField] private Animator animator;
    [SerializeField] private Transform weaponCollider;

    private void Awake() {
        animator = GetComponent<Animator>();
    }
    public void Attack(){
        animator.SetTrigger("attack");

        weaponCollider.gameObject.SetActive(true);
    }

    public void DoneAttackingAnimEvent(){
        weaponCollider.gameObject.SetActive(false);
    }
}
