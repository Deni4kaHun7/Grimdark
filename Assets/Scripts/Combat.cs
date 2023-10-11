using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    private PlayerControls playerControls;
    private Animator animator;

    private void Awake() {
        playerControls = new PlayerControls();
        animator = GetComponent<Animator>();
    }

    private void OnEnable() {
        playerControls.Enable();   
    }

    private void Start() {
        playerControls.Combat.Attack.started += _ => Attack();
    }

    private void Attack(){
        animator.SetTrigger("attack");
    }
}
