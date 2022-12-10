using UnityEngine;
using DialogueEditor;

public class DialogTrigger : MonoBehaviour
{

    bool playerDetected;
    public NPCConversation conversation;
    public GameObject iconDialog;
    public GameObject sword;
    public QuestManager questManager;
    //public int levelMission = 0;
    // Start is called before the first frame update
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
    public void GiveScore(int giveScore){
        GameObject player = GameObject.Find("Player");
        player.gameObject.GetComponent<PlayerController>().UpdatScore(giveScore, true);
    }
    public void TakeScord(int giveScore){
        GameObject player = GameObject.Find("Player");
        player.gameObject.GetComponent<PlayerController>().UpdatScore(giveScore, false);
    }
    // public void UpdateLevel()
    // {
    //     levelMission += 5;
    //     //levelMission=questManager.missionInt * levelMission;
    // }
    public void TakeSword()
    {
        sword.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        if (playerDetected && Input.GetKeyDown(KeyCode.E))
        {
            //Start Dialog
            // if(Player.Stats.quest!=null){
            //     if(Player.Stats.quest.goal.currentAmount==1){
            //         UpdateLevel();
            //     }
            // }
            Player.Stats.isConversation=true;
            ConversationManager.Instance.StartConversation(conversation);
            //ConversationManager.Instance.SetInt("MissionLevel",levelMission);
            if (questManager.quest[questManager.missionInt].isActive)
            {
                ConversationManager.Instance.SetInt("MissionLevel", questManager.missionInt + 1);
                ConversationManager.Instance.SetInt("GoalLevel", questManager.quest[questManager.missionInt].goal.currentAmount + 1);
                if(questManager.missionInt==1){
                    ConversationManager.Instance.SetBool("enoughScore", Player.Stats.score>=100);
                }
            }

        }
    }
    
    public void EndConversaion()
    {
        Player.Stats.isConversation=false;
        // playerDetected = false;
        // iconDialog.SetActive(false);
    }
}
