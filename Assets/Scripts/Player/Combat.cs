using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class Combat : MonoBehaviour
{
    [SerializeField] private Transform weaponCollider;
    //[SerializeField] private AudioSource swordSwingSound;
    //[SerializeField] private AudioSource blockSound;
    public int damage{get; private set;}
    private PlayerControls playerControls;
    private Animator animator;
    private float dirX;
    private Vector2 moveDir;
    private Player_Life health;

    private void Awake() {
        playerControls = new PlayerControls();
        animator = GetComponent<Animator>();
        health = GetComponent<Player_Life>();
    }

    private void OnEnable() {
        playerControls.Enable();   
    }

    private void Start() {
        playerControls.Combat.Attack.started += context => {
            if (context.interaction is MultiTapInteraction ){
                //swordSwingSound.Play();
                animator.SetTrigger("attack");
                damage = 1;
            } 
            else if (context.interaction is HoldInteraction){
                animator.SetTrigger("startChargeAnim");
                damage = 3;
            }
        };
        
        playerControls.Combat.Attack.performed += context => {
            if (context.interaction is MultiTapInteraction){
                animator.SetTrigger("3hitCombo");
                //swordSwingSound.Play();
                damage = 2;
            }  
             else if (context.interaction is HoldInteraction){
                //swordSwingSound.Play();
                animator.SetTrigger("finishChargeAnim");
            }
        };

        playerControls.Combat.Defense.started += _ => {
            animator.SetBool("block", true);
            health.canTakeDamage = false;
            //blockSound.Play();
            };

        playerControls.Combat.Defense.performed += _ => {
            animator.SetBool("block", false);
            health.canTakeDamage = true;
            };

        playerControls.Combat.Defense.canceled += _ => {
            animator.SetBool("block", false);
            health.canTakeDamage = true;
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
