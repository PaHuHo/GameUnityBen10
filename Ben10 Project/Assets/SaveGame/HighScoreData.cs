using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class HighScoreData
{
    public int[] highScores;
    public HighScoreData(){
        highScores = new int[10];
        for(int i = 0; i<10;i++){
            highScores[i]= GameStats.Stats.highScores[i];
        }
    }

}
