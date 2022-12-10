using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public int phase;
    public Quest[] quest;
    public int missionInt = 0;
    public GameObject questLineUI;

    public GameObject activeBoss;


    public Text titleText;
    public Text descriptionText;
    private void Start()
    {
        if (Player.Stats.isLoadGame)
        {
            missionInt = Player.Stats.missionInt;
            quest[missionInt].isActive = true;
            //quest[missionInt].goal.currentAmount = Player.Stats.goalInt;
            quest[missionInt].goal = Player.Stats.quest.goal;
            ShowQuestLine();
        }
    }
    public void ShowQuestLine()
    {
        questLineUI.SetActive(true);
        titleText.text = quest[missionInt].title;
        descriptionText.text = quest[missionInt].description[quest[missionInt].goal.currentAmount];
    }

    public void HideQuestLine()
    {
        questLineUI.SetActive(false);
    }
    private void Update()
    {
        if (Player.Stats.quest != null)
        {
            if (Player.Stats.quest.isActive)
            {
                if (Player.Stats.quest.goal.IsReached())
                {
                    Player.Stats.quest.QuestComplete();
                    Player.Stats.quest = null;
                    HideQuestLine();
                    missionInt++;
                }
                if (Player.Stats.score >= 200 && phase == 1 && quest[missionInt].goal.currentAmount == 0)
                {
                    UpdateGoal();
                }
                if (Player.Stats.score >= 100 && phase == 2 && quest[missionInt].goal.currentAmount == 0 && missionInt == 3)
                {
                    UpdateGoal();
                }
            }
            if (quest[missionInt].isActive)
            {
                Player.Stats.quest = quest[missionInt];
                Player.Stats.goalInt = quest[missionInt].goal.currentAmount;
                Player.Stats.missionInt = missionInt;
                ShowQuestLine();
            }
            if (missionInt == 2)
            {
                if (activeBoss != null)
                {
                    activeBoss.SetActive(true);
                }
            }
        }
    }
    public void ChangeQuest(int indexQuest)
    {
        missionInt = indexQuest;
        Player.Stats.quest = null;
        quest[missionInt].isActive = true;
        Player.Stats.quest = quest[missionInt];
    }
    public void AcceptQuest()
    {
        quest[missionInt].isActive = true;
        Player.Stats.quest = quest[missionInt];

    }
    public void UpdateGoal()
    {
        Player.Stats.quest.goal.UpdateGoal();
    }
    public void GetKey()
    {
        Player.Stats.quest.goal.GetKey();
    }
}
