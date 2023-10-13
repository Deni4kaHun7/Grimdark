using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    private PlayerControls playerControls;
    private Animator animator;
    [SerializeField] private Transform weaponCollider;
    private float dirX;

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

    private void Update() {
        FlipColliderDirection();
    }

    private void Attack(){
        animator.SetTrigger("attack");

        weaponCollider.gameObject.SetActive(true);
    }

    public void DoneAttackingAnimEvent(){
        weaponCollider.gameObject.SetActive(false);
    }

    public void FlipColliderDirection(){
        dirX = Input.GetAxisRaw("Horizontal");

        if (dirX < 0f){
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        } else if (dirX > 0f){
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
