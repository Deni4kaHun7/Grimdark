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
        UIFade.Instance.FadeToBlack();
        StartCoroutine(LoadSceneRoutine());
    }

    private IEnumerator LoadSceneRoutine(){
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

