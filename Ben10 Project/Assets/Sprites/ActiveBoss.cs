using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveBoss : MonoBehaviour
{
    public GameObject mainCameraSet;
    public GameObject bossCameraSet;

    public GameObject doorLock;
    public GameObject boss;
    public AudioSource BGMusic;
    public AudioSource bossThemeMusic;
    public Transform spawn;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            //Open Conversation with Boss
           StartCoroutine( BossActived());
           other.GetComponent<PlayerCollision>().repawnPosition=spawn;
        }
    }
    
    public IEnumerator BossActived(){
        boss.gameObject.SetActive(true);
        bossCameraSet.gameObject.SetActive(true);
        mainCameraSet.gameObject.SetActive(false);
        doorLock.gameObject.GetComponent<BoxCollider2D>().enabled=true;
        this.GetComponent<BoxCollider2D>().enabled=false;
        bossThemeMusic.Play();
        BGMusic.Pause();
        yield return new WaitForSeconds(0.5f);
        boss.gameObject.GetComponent<Boss>().StartConversation();
        
    }
    public void BossUnActived(){
        //boss.gameObject.SetActive(true);
        bossCameraSet.gameObject.SetActive(false);
        mainCameraSet.gameObject.SetActive(true);
        bossThemeMusic.Pause();
        BGMusic.Play();
        //doorLock.gameObject.GetComponent<BoxCollider2D>().enabled=false;
        Destroy(bossCameraSet,3f);
        Destroy(doorLock);
        Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
