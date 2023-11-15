using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private AudioSource swordSound;
    private Canvas canvas;

    private void Awake() {
        canvas = GetComponent<Canvas>();
    }
    private void FixedUpdate() {
        if (Input.anyKey){
            swordSound.Play();
            
            LoadSceneRoutine();
        }    
    }
    private IEnumerator LoadSceneRoutine(){
        //canvas.enabled = false;
        yield return new WaitForSeconds(2f);
        Debug.Log("sdfds");
        //SceneManager.LoadScene("OpenCutscene");
    }
    
}
