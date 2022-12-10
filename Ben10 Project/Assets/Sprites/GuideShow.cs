using System.Collections;
using System.Collections.Generic;
using DialogueEditor;
using UnityEngine;

public class GuideShow : MonoBehaviour
{
    public NPCConversation conversation;

    public GameObject dragon;
    public GameObject slime;

    public QuestManager quest;
    // Start is called before the first frame update
    void Start()
    {
        if (Player.Stats.isLoadGame)
        {
            Destroy(gameObject);
        }
        else
        {

            StartConversation();
        }
    }


    public void EndConversaion()
    {
        dragon.SetActive(true);
        if (slime != null)
        {
            slime.SetActive(true);
        }
        Player.Stats.isConversation = false;
        Destroy(gameObject);
    }

    public void StartConversation()
    {
        Player.Stats.isConversation = true;
        //Player.Stats.canSave = false;
        ConversationManager.Instance.StartConversation(conversation);
        dragon.SetActive(false);
        if (slime != null)
        {
            slime.SetActive(false);
        }
    }
}
