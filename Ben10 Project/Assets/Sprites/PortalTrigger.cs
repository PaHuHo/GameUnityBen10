using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using UnityEngine.SceneManagement;


public class PortalTrigger : MonoBehaviour
{
    bool playerDetected;
    public GameObject iconDialog;

    public NPCConversation conversation;


    public bool canUse;
    public int scoreUse;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerDetected = true;
            iconDialog.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerDetected = false;
            iconDialog.SetActive(false);

        }
    }
    void Update()
    {
        if (playerDetected && Input.GetKeyDown(KeyCode.E))
        {
            StartConversation();
        }
    }
    public void UpdatePortal()
    {
        canUse = true;
    }
    public void StartConversation()
    {
        Player.Stats.isConversation = true;
        ConversationManager.Instance.StartConversation(conversation);
        ConversationManager.Instance.SetBool("enoughScore", Player.Stats.score >= scoreUse);
    }
    public void EndConversaion()
    {
        Player.Stats.isConversation = false;
    }
    public void UsePortal()
    {
        Player.Stats.isLoadGame=false;
        //Sau khi dung conng se mat diem
        Player.Stats.score -= scoreUse;
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = i + 1; j < 10; j++)
                {
                    if (GameStats.Stats.highScores[i] < GameStats.Stats.highScores[j])
                    {
                        int temp = GameStats.Stats.highScores[i];
                        GameStats.Stats.highScores[i] = GameStats.Stats.highScores[j];
                        GameStats.Stats.highScores[j] = temp;
                    }
                }
            }
            int index = 10;
            for (int i = 0; i < 10; i++)
            {
                if (Player.Stats.score > GameStats.Stats.highScores[i])
                {
                    index = i;
                    break;
                }

            }

            for (int i = 9; i > index; i--)
            {
                GameStats.Stats.highScores[i] = GameStats.Stats.highScores[i - 1];
            }
            GameStats.Stats.highScores[index] = Player.Stats.score;
            SaveSystem.SaveHighScore();
            SaveSystem.DeleteSave();

        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1 == SceneManager.sceneCount ? 0 : SceneManager.GetActiveScene().buildIndex + 1);
    }
}
