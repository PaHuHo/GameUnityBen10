using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveGame(GameData gameData){
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/game.sav";
        FileStream fileStream = new FileStream(path, FileMode.Create);
        binaryFormatter.Serialize(fileStream,gameData);
        fileStream.Close();
        Debug.Log("Save thành công");
        Debug.Log(path);
    }
    public static GameData LoadGame(){
        
        string path = Application.persistentDataPath + "/game.sav";
        if(File.Exists(path)){
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(path, FileMode.Open);
            GameData gameData =  binaryFormatter.Deserialize(fileStream) as GameData;
            fileStream.Close();


            return gameData;
        }
        else{
            Debug.Log("Không tồn tại tệp lưu");
            return null;
        }
    }

    public static bool DeleteSave(){
        
        string path = Application.persistentDataPath + "/game.sav";
        if(File.Exists(path)){
            System.IO.File.Delete(path);
            return true;
        }
        else{
            Debug.Log("Không tồn tại tệp lưu");
            return false;
        }
    }

    public static void SaveHighScore(){
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/highscore.sav";
        FileStream fileStream = new FileStream(path, FileMode.Create);
        binaryFormatter.Serialize(fileStream,new HighScoreData());
        fileStream.Close();
        Debug.Log("Save thành công");
        Debug.Log(path);
    }
    public static int[] LoadHighScore(){
        
        string path = Application.persistentDataPath + "/highscore.sav";
        if(File.Exists(path)){
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(path, FileMode.Open);
            HighScoreData highScoreData =  binaryFormatter.Deserialize(fileStream) as HighScoreData;
            fileStream.Close();

            return highScoreData.highScores;
        }
        else{
            Debug.Log("Không tồn tại tệp lưu");
            return null;
        }
    }

}
