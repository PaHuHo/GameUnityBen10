using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayMenu : MonoBehaviour
{
    // Update is called once per frame
    void Start() {
        
    }
    void Update()
    {
    }

    public void PlayGame(){
        Debug.Log("Play");
        Player.Stats.isLoadGame=false;
        Player.Stats.score = 0;
        SceneManager.LoadScene(1);
    }

    public void QuitGame(){
        //Debug.Log("Exit");
        Application.Quit();
    }
    public void BackToMenu()
    {
        Debug.Log("Back");
        SceneManager.LoadScene(0);
    }
}
