using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public static PlatformManager Instance=null;
    public GameObject platformPrefab;

    private void Awake() {
        if(Instance ==null){
            Instance=this;
        }else if(Instance!=this){
            Destroy(gameObject);
        }
    }
    IEnumerator RespawnPlatform(Vector2 respawn){

        yield return new WaitForSeconds(2f);
        Instantiate(platformPrefab,respawn,platformPrefab.transform.rotation);
    }
}
