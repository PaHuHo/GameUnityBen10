using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats 
{
    //Singleton/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private static GameStats stats;

    public static GameStats Stats{
        get{
            if(stats==null){
                stats=new GameStats();
            };
            return stats;
        }
        set{stats=value;}
    }
    private GameStats(){}
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    
    public int[] highScores;

    


}
