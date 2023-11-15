using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{
    void OnEnable()
    {
        Destroy(GameObject.FindGameObjectWithTag("Trap"));

        Destroy(GameObject.FindGameObjectWithTag("Player"));
        SceneManager.LoadScene("L1S1");
    }
}
