using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public int id;
    public bool isActive;
    public string title;
    
    public string[] description;
    public QuestGoal goal;


    public void QuestComplete(){
        isActive=false;
        Debug.Log(title +" was complete");
    }
}
