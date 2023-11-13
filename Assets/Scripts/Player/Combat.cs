using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class Combat : MonoBehaviour
{
    [SerializeField] private Transform weaponCollider;
    private PlayerControls playerControls;
    private Animator animator;
    private float dirX;
    private Vector2 moveDir;

    private void Awake() {
        playerControls = new PlayerControls();
        animator = GetComponent<Animator>();
    }

    private void OnEnable() {
        playerControls.Enable();   
    }

    private void Start() {
        playerControls.Combat.Attack.started += context => {
            if (context.interaction is HoldInteraction){
                animator.SetTrigger("startChargeAnim");
            }
            else if (context.interaction is TapInteraction){
                animator.SetTrigger("attack");
            } 
        };
        
        playerControls.Combat.Attack.performed += context => {
            if (context.interaction is HoldInteraction){
                animator.SetTrigger("finishChargeAnim");
            }
            else if (context.interaction is TapInteraction){
                return;
            } 
        };
    }

    private void Update() {
        FlipColliderDirectionPlayer();
    }

    public void ActivateWeaponCollider(){
        weaponCollider.gameObject.SetActive(true);
    }

    public void DeactivateWeaponCollider(){
        weaponCollider.gameObject.SetActive(false);
    }

    private void FlipColliderDirectionPlayer(){
        dirX = Input.GetAxisRaw("Horizontal");

        if (dirX < 0f){
            weaponCollider.transform.rotation = Quaternion.Euler(0, 180, 0);
        } else if (dirX > 0f){
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
