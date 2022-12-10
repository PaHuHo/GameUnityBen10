using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !Player.Stats.isConversation)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        // if(GameIsPaused){
        //     GameObject gameObject = GameObject.Find("SaveButton");
        //     Debug.Log(Player.Stats.canSave.ToString());
        //     if(!Player.Stats.canSave){
        //         if(gameObject!=null){
        //             gameObject.SetActive(false);
        //         }
        //     }
        //     else{
        //         if(gameObject!=null){
        //             gameObject.SetActive(true);
        //         }
        //     }
        // }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        Player.Stats.score = 0;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        Button button = GameObject.Find("SaveButton").GetComponent<Button>();
        if (Player.Stats.quest.id == 3 && Player.Stats.quest.goal.currentAmount == 1)
        {

            if (button != null)
            {
                button.enabled = false;
            }
        }
        else
        {
            if (button != null)
            {
                button.enabled = true;
            }
        }
        GameIsPaused = true;
    }
    public void ReloadGame()
    {
        //Debug.Log("Reload");
        Time.timeScale = 1f;
        if(SceneManager.GetActiveScene().buildIndex==1)
        {
            Player.Stats.score=0;          
        }
        Player.Stats.quest=null;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}
