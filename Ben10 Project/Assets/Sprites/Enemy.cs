using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;
using System;
using TMPro;

public class Enemy : MonoBehaviour
{
    public int maxHealth=100;
    int currentHealth;

    public int damage=1;
    public Animator animator;

    public float startX;
    public float endX;
    public float speed;
    public float vision;
    public float powerToJump;
    public Transform enemyTransform;
    public Transform playerTransform;

    public int giveScore;
    //public TextMeshProUGUI scoreText;
    //public Rigidbody2D rigidbody2D;

    private bool onGoRight=true;
    private float dirX;
    private float distanceX;
    private float distanceY;
    private float oldX;
    private float oldY;
    private float timeToJump=0f;
    // public GameObject bloodEffect;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth=maxHealth;
        oldX = enemyTransform.position.x;
        oldY = enemyTransform.position.y;
        timeToJump=Time.time+Random.Range(6f,16f);
        onGoRight = Random.Range(-1f,1f)>0?true:false;

        Physics2D.IgnoreLayerCollision(7, 7, true);
    }

    void Update()
    {
        distanceX = enemyTransform.position.x-playerTransform.position.x;
        distanceY = enemyTransform.position.y-playerTransform.position.y;
        if(Math.Abs(distanceX)<=vision && Math.Abs(distanceY)<=vision){
            if(distanceX<0){
                dirX=1;
                if(enemyTransform.position.x>=endX){
                    onGoRight=false;
                }
            }
            else{
                dirX=-1;
                if(enemyTransform.position.x<=startX){
                    onGoRight=true;
                }
            }
        }
        else{
            if(onGoRight){
                dirX=1;
                if(enemyTransform.position.x>=endX){
                    onGoRight=false;
                }
            }
            else{
                dirX=-1;
                if(enemyTransform.position.x<=startX){
                    onGoRight=true;
                }
            }
        }
        GetComponent<Rigidbody2D>().velocity = new Vector2(dirX * speed, GetComponent<Rigidbody2D>().velocity.y);

        // if(enemyTransform.rotation.z!=0){
        //     enemyTransform.rotation = Quaternion.Euler(0, 0, 0);
        // }


        if(oldX==enemyTransform.position.x && Time.time>timeToJump){
            GetComponent<Rigidbody2D>().velocity = new Vector3(GetComponent<Rigidbody2D>().velocity.x, powerToJump, 0);
            timeToJump=Time.time+Random.Range(8f,16f);
        }

        oldX = enemyTransform.position.x;
        oldY = enemyTransform.position.y;
    }

    // private void OnTriggerEnter2D(Collider2D other) {
    //     if(other.CompareTag("Player")){
    //         other.gameObject.GetComponent<PlayerCombat>().TakeDamage(damage);
    //     }
    // }
    private void OnCollisionEnter2D(Collision2D other) {
        
        if(other.gameObject.name=="Player"){
            other.gameObject.GetComponent<PlayerCombat>().TakeDamage(damage);
            if(distanceX<0){
                playerTransform.position = new Vector2(playerTransform.position.x+speed/10, playerTransform.position.y);
            }
            else{
                playerTransform.position = new Vector2(playerTransform.position.x-speed/10, playerTransform.position.y);
            }
        }
    }

    public void TakeDamage(int damage){

        //Instantiate(bloodEffect,transform.position,Quaternion.identity);
        currentHealth-=damage;

        //play enemy hurt animation
        animator.SetTrigger("Hurt");
        
        Debug.Log(currentHealth);
        if(currentHealth<=0){
            // Player.Stats.score +=scorePlayerWillGetWhenKillEnemy;
            // scorePlayerWillGetWhenKillEnemy=0;
            // scoreText.text = Player.Stats.score.ToString();
            GameObject player=GameObject.Find("Player");
            player.gameObject.GetComponent<PlayerController>().UpdatScore(giveScore,true);
            Dead();
        }
    }

    void Dead(){
        //Enemy dead animation 
        animator.SetBool("IsDead",true);
        //Disable the enemy
        Destroy(this.gameObject);
        // GetComponent<Collider2D>().enabled=false;
        // this.enabled=false;
    }
}
