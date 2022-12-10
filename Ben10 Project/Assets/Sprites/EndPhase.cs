using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DialogueEditor;

public class EndPhase : MonoBehaviour
{
    public NPCConversation conversation;
    // Start is called before the first frame update
    void Start()
    {
        ConversationManager.Instance.StartConversation(conversation);
    }
    public void EndGame(){
        SceneManager.LoadScene(0);
        
    }
    
}
