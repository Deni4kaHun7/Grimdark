using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    private Rigidbody2D rb;
    public bool gettingKnockedBack {get ; private set;}

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }
    public void GetKnockedBack(Transform damageSource, float knockBackThrust){
        gettingKnockedBack = true;
        Vector2 difference = (transform.position - damageSource.position).normalized * knockBackThrust * rb.mass;
        rb.AddForce(difference, ForceMode2D.Impulse);
    }
}
