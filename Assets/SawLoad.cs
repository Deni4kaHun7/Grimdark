using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawLoad : MonoBehaviour
{
    [SerializeField] private GameObject Saw;
    private WaypointFollower movement;

    private CircleCollider2D circleCollider;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        circleCollider = Saw.GetComponent<CircleCollider2D>();
        spriteRenderer = Saw.GetComponent<SpriteRenderer>();
        movement = Saw.GetComponent<WaypointFollower>();
        circleCollider.enabled = false;
        spriteRenderer.enabled = false;
        movement.FreezeMovement();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            movement.StartMovement();
            circleCollider.enabled = true;
            spriteRenderer.enabled = true;
        }
    }
}
