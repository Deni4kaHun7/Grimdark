using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private AudioSource swordSound;
    [SerializeField] private GameObject blackScreen;
    private Canvas canvas;

    private void Awake() {
        canvas = GetComponent<Canvas>();
    }
    private void FixedUpdate() {
        if (Input.anyKey){
            
            StartCoroutine(LoadSceneRoutine());
        }    
    }
    private IEnumerator LoadSceneRoutine(){
        swordSound.Play();
        blackScreen.SetActive(true);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("OpenCutscene");
    }
    
}
