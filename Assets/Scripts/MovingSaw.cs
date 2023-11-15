using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSaw : MonoBehaviour
{
    [SerializeField] public GameObject movingSaw;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hi");
            ActivateMovingSaw();
        }
    }

    private void ActivateMovingSaw()
    {
        movingSaw.SetActive(true);
    }
}

