using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    // Start is called before the first frame update
    public void SaveGame(){
        SaveSystem.SaveGame(new GameData());
    }
    public void LoadGame(){
        GameData gameData = SaveSystem.LoadGame();
        if(gameData == null){
            
        }
        else{
            Player.Stats.isLoadGame = true;
            SceneManager.LoadScene(gameData.numberOfScene);
            Player.Stats.playerPosition = new Vector3(gameData.playerPosition[0],gameData.playerPosition[1],gameData.playerPosition[2]);
            Player.Stats.score = gameData.score;
        }
    }
    public void DeleteSave(){
        SaveSystem.DeleteSave();
    }
    void Start() {
        GameData gameData = SaveSystem.LoadGame();
        LoadHighScore();
        if(gameData==null){
            GameObject loadButton = GameObject.Find("LoadButton");
            if(loadButton!=null){
                loadButton.SetActive(false);
            }

        }
        else{
            GameObject loadButton = GameObject.Find("LoadButton");
            if(loadButton!=null){
                loadButton.SetActive(true);
            }
        }

        if(Player.Stats.isLoadGame){
            GameObject player = GameObject.Find("Player");
            if(player!=null){
                player.GetComponent<Transform>().position = Player.Stats.playerPosition;
            }
            Player.Stats.missionInt=gameData.missionInt;
            Player.Stats.currentHealth=gameData.currentHealth;
            Player.Stats.goalInt=gameData.goalInt;
            Player.Stats.quest=gameData.quest;
            
            if(Player.Stats.numberOfScene==1){
                GameObject gameObject;
                gameObject = GameObject.Find("QueenDragon");
                if(gameObject!=null){
                    if(gameData.queenDragonIsLive){
                        gameObject.GetComponent<Transform>().position = new Vector3(gameData.positionQueenDragon[0],gameData.positionQueenDragon[1],gameData.positionQueenDragon[2]);
                    }
                    else{
                        gameObject.SetActive(false);
                    }
                }
                gameObject = GameObject.Find("DarkDragon");
                if(gameObject!=null){
                    if(gameData.darkDragonIsLine){
                        gameObject.GetComponent<Transform>().position = new Vector3(gameData.positionDarkDragon[0],gameData.positionDarkDragon[1],gameData.positionDarkDragon[2]);
                    }
                    else{
                        gameObject.SetActive(false);
                    }
                }
                for(int i=0;i<11;i++){
                    gameObject = GameObject.Find("Enemy"+(i+1).ToString());
                    if(gameObject!=null){
                        if(gameData.slimeIsLive[i]){
                            gameObject.GetComponent<Transform>().position = new Vector3(gameData.positionSlimes[i][0],gameData.positionSlimes[i][1],gameData.positionSlimes[i][2]);
                        }
                        else{
                            gameObject.SetActive(false);
                        }
                    }
                }
                for(int i=0;i<3;i++){
                    gameObject = GameObject.Find("Diamond"+(i+1).ToString());
                    if(gameObject!=null){
                        if(gameData.diamondIsExis[i]){
                            gameObject.GetComponent<Transform>().position = new Vector3(gameData.positionDiamonds[i][0],gameData.positionDiamonds[i][1],gameData.positionDiamonds[i][2]);
                        }
                        else{
                            gameObject.SetActive(false);
                        }
                    }
                }
                for(int i=0;i<3;i++){
                    gameObject = GameObject.Find("MovingPlatform"+(i+1).ToString());
                    if(gameObject!=null){                
                        gameObject.transform.GetChild(0).gameObject.GetComponent<Transform>().position = new Vector3(gameData.positionMovingPlatforms[i][0],gameData.positionMovingPlatforms[i][1],gameData.positionMovingPlatforms[i][2]);                
                    }
                }

            }
            if(Player.Stats.numberOfScene==2){
                GameObject gameObject;
                gameObject = GameObject.Find("FireDragon");
                if(gameObject!=null){
                    if(gameData.fireDragonIsLine){
                        gameObject.GetComponent<Transform>().position = new Vector3(gameData.positionFireDragon[0],gameData.positionFireDragon[1],gameData.positionFireDragon[2]);
                    }
                    else{
                        gameObject.SetActive(false);
                    }
                }
                gameObject = GameObject.Find("LifeDragon");
                if(gameObject!=null){
                    if(gameData.lifeDragonIsLine){
                        gameObject.GetComponent<Transform>().position = new Vector3(gameData.positionLifeDragon[0],gameData.positionLifeDragon[1],gameData.positionLifeDragon[2]);
                    }
                    else{
                        gameObject.SetActive(false);
                    }
                }
                gameObject = GameObject.Find("GoldenDragon");
                if(gameObject!=null){
                    if(gameData.goldenDragonIsLine){
                        gameObject.GetComponent<Transform>().position = new Vector3(gameData.positionGoldenDragon[0],gameData.positionGoldenDragon[1],gameData.positionGoldenDragon[2]);
                    }
                    else{
                        gameObject.SetActive(false);
                    }
                }
                for(int i=3;i<6;i++){
                    gameObject = GameObject.Find("Diamond"+(i+1).ToString());
                    if(gameObject!=null){
                        if(gameData.diamondIsExis[i]){
                            gameObject.GetComponent<Transform>().position = new Vector3(gameData.positionDiamonds[i][0],gameData.positionDiamonds[i][1],gameData.positionDiamonds[i][2]);
                        }
                        else{
                            gameObject.SetActive(false);
                        }
                    }
                }
                for(int i=3;i<8;i++){
                    gameObject = GameObject.Find("MovingPlatform"+(i+1).ToString());
                    if(gameObject!=null){                
                        gameObject.transform.GetChild(0).gameObject.GetComponent<Transform>().position = new Vector3(gameData.positionMovingPlatforms[i][0],gameData.positionMovingPlatforms[i][1],gameData.positionMovingPlatforms[i][2]);                
                    }
                }

            }
        }    
        
    }
    public void SaveHighScore(){
        for(int i = 0; i<9;i++){
            for(int j=i+1;j<10;j++){
                if(GameStats.Stats.highScores[i]<GameStats.Stats.highScores[j]){
                    int temp = GameStats.Stats.highScores[i];
                    GameStats.Stats.highScores[i] = GameStats.Stats.highScores[j];
                    GameStats.Stats.highScores[j] = temp;
                }
            }
        }
        int index=10;
        for(int i = 0; i<10; i++){
            if(Player.Stats.score>GameStats.Stats.highScores[i]){
                index = i;
                break;
            }

        }

        for(int i = 9; i>index;i--){
            GameStats.Stats.highScores[i]=GameStats.Stats.highScores[i-1];
        }
        GameStats.Stats.highScores[index]=Player.Stats.score;
        // Debug.Log("Index: "+ index);
        // string debugText="";
        //     for(int i = 0; i<10;i++){
        //         debugText+= GameStats.Stats.highScores[i]+" ";

        //     }
        // Debug.Log(debugText);
        SaveSystem.SaveHighScore();
    }
    public void LoadHighScore(){
        int[] highScores = SaveSystem.LoadHighScore();
        if(highScores == null){
            GameStats.Stats.highScores = new int[10];
            for(int i=0;i<10;i++){
                GameStats.Stats.highScores[i]=0;
            }
            // string debugText="";
            // for(int i = 0; i<10;i++){
            //     debugText+= GameStats.Stats.highScores[i]+" ";

            // }
            // Debug.Log(debugText);
        }
        else{
            GameStats.Stats.highScores=highScores;
            // string debugText="Load ";
            // for(int i = 0; i<10;i++){
            //     debugText+= GameStats.Stats.highScores[i]+" ";

            // }
            // Debug.Log(debugText);
        }
    }
    public void ViewHighScore(){
        SceneManager.LoadScene(4);
    }
}
