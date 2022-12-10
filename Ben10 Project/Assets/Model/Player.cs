using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player 
{


    //Singleton/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private static Player stats;

    public static Player Stats{
        get{
            if(stats==null){
                stats=new Player();
            };
            return stats;
        }
        set{stats=value;}
    }
    private Player(){}
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Player stats for PlayerController
    //public bool canSave = true;
    public bool isLoadGame = false;
    public bool facingRight=true;
    public bool isGrounded;
    public GameObject playerObj = null;
    public float dirX = 0f;
    public bool canDash=true;
    public bool isDasing;
    public float dasingPower=35f;
    public float dasingTime=0.07f;
    public float dasingCooldown=1f;
    public Vector3 playerPosition = new Vector3(0,0,0);
    public Transform playerTransform;
    public int numberOfScene = 1;
    //Player stats for PlayerCombat
    public int attackDamage = 40;
    public float nextAttackTime = 0f;
    public float attackRate = 1.5f;
    public float rangeAttack = 0.66f;
    public int maxHealth = 100;
    public int currentHealth;

    //Headblast shoot stats
    public bool canShoot=true;

    public float shootRate=1.5f;
    public float nextShoot=0f;

    //Stats for lava
    public bool onLava=false;
    public bool delayLavaDamage=false;
    //Transformer
    public bool onTransform = false;
    public Vector3 sizeOfBoxColliderPlayer = new Vector3(0.4275805f,1.453937f,1f);
    public Vector3 sizeOfBoxColliderHeadblast = new Vector3(0.4f,1f);
    public float timeCanTransform = 0;
    public float cooldownTransform = 5;
    public int numberOfTransform = 0;
    public float timeToTransform = 15f;
    public float endTimeTransform = 0;
    public float sizeOfHeadblast = 2f;
    //Bag
    public int score = 0;
    //Mission
    public Quest quest;
    public bool isDead = false;

    public bool isConversation;

    public int missionInt;
    public int goalInt;
}   
