using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
   public float[] playerPosition;
   public int numberOfScene;
   public int score;

   public int currentHealth;

   public int missionInt;
   public int goalInt;

    public Quest quest;

    public float[] positionQueenDragon;
    public float[] positionDarkDragon;
    public float[] positionGoldenDragon;
    public float[] positionLifeDragon;
    public float[] positionFireDragon;
    public bool queenDragonIsLive;
    public bool darkDragonIsLine;
    public bool goldenDragonIsLine;
    public bool lifeDragonIsLine;
    public bool fireDragonIsLine;
    public bool[] slimeIsLive;
    public float[][] positionSlimes;
    public bool[] diamondIsExis;
    public float[][] positionDiamonds;
    public float[][] positionMovingPlatforms;
    

    public GameData(){
        playerPosition = new float[3];
        playerPosition[0] = Player.Stats.playerTransform.position.x;
        playerPosition[1] = Player.Stats.playerTransform.position.y;
        playerPosition[2] = Player.Stats.playerTransform.position.z;
        numberOfScene =  Player.Stats.numberOfScene;
        score = Player.Stats.score;
        missionInt=Player.Stats.missionInt;
        currentHealth=Player.Stats.currentHealth;
        goalInt=Player.Stats.goalInt;
        quest=Player.Stats.quest;
        if(Player.Stats.numberOfScene==1){
            GameObject gameobject;
            gameobject = GameObject.Find("QueenDragon");
            if(gameobject==null){
                queenDragonIsLive=false;
            }
            else{
                queenDragonIsLive=true;
                positionQueenDragon= new float[3];
                positionQueenDragon[0] = gameobject.GetComponent<Transform>().position.x;
                positionQueenDragon[1] = gameobject.GetComponent<Transform>().position.y;
                positionQueenDragon[2] = gameobject.GetComponent<Transform>().position.z;
            }
            gameobject = GameObject.Find("DarkDragon");
            if(gameobject==null){
                darkDragonIsLine=false;
            }
            else{
                darkDragonIsLine=true;
                positionDarkDragon= new float[3];
                positionDarkDragon[0] = gameobject.GetComponent<Transform>().position.x;
                positionDarkDragon[1] = gameobject.GetComponent<Transform>().position.y;
                positionDarkDragon[2] = gameobject.GetComponent<Transform>().position.z;
            }
            slimeIsLive = new bool[11];
            positionSlimes = new float[11][];
            for(int i=0;i<11;i++){
                gameobject = GameObject.Find("Enemy"+(i+1).ToString());
                if(gameobject==null){
                    slimeIsLive[i]=false;
                }
                else{
                    slimeIsLive[i]=true;
                    positionSlimes[i]= new float[3];
                    positionSlimes[i][0] = gameobject.GetComponent<Transform>().position.x;
                    positionSlimes[i][1] = gameobject.GetComponent<Transform>().position.y;
                    positionSlimes[i][2] = gameobject.GetComponent<Transform>().position.z;
                }
            }
            diamondIsExis = new bool[6];
            positionDiamonds = new float[6][];
            for(int i=0;i<3;i++){
                gameobject = GameObject.Find("Diamond"+(i+1).ToString());
                if(gameobject==null){
                    diamondIsExis[i]=false;
                }
                else{
                    diamondIsExis[i]=true;
                    positionDiamonds[i]= new float[3];
                    positionDiamonds[i][0] = gameobject.GetComponent<Transform>().position.x;
                    positionDiamonds[i][1] = gameobject.GetComponent<Transform>().position.y;
                    positionDiamonds[i][2] = gameobject.GetComponent<Transform>().position.z;
                }
            }
             positionMovingPlatforms = new float[8][];
            for(int i=0;i<3;i++){
                gameobject = GameObject.Find("MovingPlatform"+(i+1).ToString());
                if(gameobject!=null){
                    positionMovingPlatforms[i]= new float[3];
                    positionMovingPlatforms[i][0] = gameobject.transform.GetChild(0).gameObject.GetComponent<Transform>().position.x;
                    positionMovingPlatforms[i][1] = gameobject.transform.GetChild(0).gameObject.GetComponent<Transform>().position.y;
                    positionMovingPlatforms[i][2] = gameobject.transform.GetChild(0).gameObject.GetComponent<Transform>().position.z;
                }

            }

        }
        if(Player.Stats.numberOfScene==2){
            GameObject gameobject;
            gameobject = GameObject.Find("FireDragon");
            if(gameobject==null){
                fireDragonIsLine=false;
            }
            else{
                fireDragonIsLine=true;
                positionFireDragon= new float[3];
                positionFireDragon[0] = gameobject.GetComponent<Transform>().position.x;
                positionFireDragon[1] = gameobject.GetComponent<Transform>().position.y;
                positionFireDragon[2] = gameobject.GetComponent<Transform>().position.z;
            }
            gameobject = GameObject.Find("LifeDragon");
            if(gameobject==null){
                lifeDragonIsLine=false;
            }
            else{
                lifeDragonIsLine=true;
                positionLifeDragon= new float[3];
                positionLifeDragon[0] = gameobject.GetComponent<Transform>().position.x;
                positionLifeDragon[1] = gameobject.GetComponent<Transform>().position.y;
                positionLifeDragon[2] = gameobject.GetComponent<Transform>().position.z;
            }
            gameobject = GameObject.Find("GoldenDragon");
            if(gameobject==null){
                goldenDragonIsLine=false;
            }
            else{
                goldenDragonIsLine=true;
                positionGoldenDragon= new float[3];
                positionGoldenDragon[0] = gameobject.GetComponent<Transform>().position.x;
                positionGoldenDragon[1] = gameobject.GetComponent<Transform>().position.y;
                positionGoldenDragon[2] = gameobject.GetComponent<Transform>().position.z;
            }
            diamondIsExis = new bool[6];
            positionDiamonds = new float[6][];
            for(int i=3;i<6;i++){
                gameobject = GameObject.Find("Diamond"+(i+1).ToString());
                if(gameobject==null){
                    diamondIsExis[i]=false;
                }
                else{
                    diamondIsExis[i]=true;
                    positionDiamonds[i]= new float[3];
                    positionDiamonds[i][0] = gameobject.GetComponent<Transform>().position.x;
                    positionDiamonds[i][1] = gameobject.GetComponent<Transform>().position.y;
                    positionDiamonds[i][2] = gameobject.GetComponent<Transform>().position.z;
                }
            }
            positionMovingPlatforms = new float[8][];
            for(int i=3;i<8;i++){
                gameobject = GameObject.Find("MovingPlatform"+(i+1).ToString());
                if(gameobject!=null){
                    positionMovingPlatforms[i]= new float[3];
                    positionMovingPlatforms[i][0] = gameobject.transform.GetChild(0).gameObject.GetComponent<Transform>().position.x;
                    positionMovingPlatforms[i][1] = gameobject.transform.GetChild(0).gameObject.GetComponent<Transform>().position.y;
                    positionMovingPlatforms[i][2] = gameobject.transform.GetChild(0).gameObject.GetComponent<Transform>().position.z;
                }

            }
        }
    }
}
