using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public GoalType goalType;

    public bool haveKey=false;
    public int requiredAmount;
    public int currentAmount=0;

    public bool IsReached(){
        return (currentAmount>=requiredAmount);
    }

    public void EnemyKilled(){
        if(goalType==GoalType.Kill)
        currentAmount++;
    }
     public void ItemCollected(){
        if(goalType==GoalType.Gathering)
        currentAmount++;
    }
    public void UpdateGoal(){
        currentAmount++;
    }
    public void GetKey(){
        haveKey=true;
    }
}
public enum GoalType{
        Kill,
        Gathering
    }
