using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    private Canvas canvas;

    private void Awake() {
        canvas = GetComponent<Canvas>();
    }
    private void Update() {
        if (Input.anyKey){
            SceneManager.LoadScene("Combat");
            LoadSceneRoutine();
        }    
    }
    private IEnumerator LoadSceneRoutine(){
        //canvas.enabled = false;
        yield return new WaitForSeconds(2f);
        
    }
    
}
