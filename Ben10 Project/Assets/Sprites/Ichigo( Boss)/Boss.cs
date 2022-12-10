using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using DialogueEditor;
using TMPro;

public class Boss : MonoBehaviour
{
    public Transform repawnPosition;
    public int maxHealth = 100;
    int currentHealth;
    public HealthBar healthBar;
    public Animator animator;
    public Transform player;
    public bool isFlipped = true;
    public ActiveBoss active;
    public int attackDamage = 10;
    public float rangeAttack = 1.5f;
    public Transform attackPoint;
    public LayerMask playerMask;

    public GameObject shootingItemToRight, shootingItemToLeft;
    public bool canShoot = true;
    public NPCConversation conversation;
    public GameObject iconDialog;
    //bool playerDetected;
    public int levelMission = 0;
    public TextMeshProUGUI scoreText;

    public QuestManager questManager;


    private void OnTriggerEnter2D(Collider2D other)
    {
        // if (other.tag == "Player" && !animator.GetBool("IsBattle"))
        // {
        //     playerDetected = true;
        //     iconDialog.SetActive(true);
        // }
    }
    public void Respawn(bool defeat)
    {
        this.transform.position = new Vector2(repawnPosition.position.x, repawnPosition.position.y);
        if (!defeat)
        {
            currentHealth = maxHealth;
            healthBar.SetHealth(currentHealth);
        }
    }
    public void GiveScore(int giveScore)
    {
        GameObject player = GameObject.Find("Player");
        player.gameObject.GetComponent<PlayerController>().UpdatScore(giveScore, true);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        // if (other.tag == "Player")
        // {
        //     playerDetected = false;
        //     iconDialog.SetActive(false);
        // }
    }
    public void EndConversaion()
    {
        Player.Stats.isConversation=false;
        // playerDetected = false;
        // iconDialog.SetActive(false);
    }
    private void Update()
    {
        // if (!animator.GetBool("IsBattle") && playerDetected && Input.GetKeyDown(KeyCode.E))
        // {
        //     if (Player.Stats.quest != null)
        //     {
        //         Player.Stats.quest.goal.UpdateGoal();
        //     }
        //     GetComponent<Collider2D>().enabled = false;
        //     this.enabled = false;
        //     active.GetComponent<ActiveBoss>().BossUnActived();
        // }
    }
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.gameObject.SetActive(true);
    }
    public void StartConversation()
    {
        Player.Stats.isConversation=true;
        ConversationManager.Instance.StartConversation(conversation);
        ConversationManager.Instance.SetBool("Defeat", questManager.quest[questManager.missionInt].goal.currentAmount == 2);
    }
    public void StartBattle()
    {
        animator.SetBool("IsBattle", true);
    }
    public void EndBattle()
    {
        animator.SetBool("IsBattle", false);
        Respawn(true);
        Player.Stats.isConversation=true;
        ConversationManager.Instance.StartConversation(conversation);
        //ConversationManager.Instance.SetInt("Level",10);
        ConversationManager.Instance.SetBool("Defeat", true);
    }
    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    public void Attack()
    {
        Collider2D colInfo = Physics2D.OverlapCircle(attackPoint.position, rangeAttack, playerMask);
        if (colInfo != null)
        {
            colInfo.GetComponent<PlayerCombat>().TakeDamage(attackDamage);
        }
    }

    public void RangeAttack()
    {
        if (!canShoot)
        {
            return;
        }
        Vector2 bulletPos = transform.position;

        if (isFlipped)
        {
            bulletPos += new Vector2(+2f, Random.Range(-2f, 3f));
            Instantiate(shootingItemToRight, bulletPos, Quaternion.identity);
        }
        else
        {
            bulletPos += new Vector2(-2f, Random.Range(-2f, 3f));
            Instantiate(shootingItemToLeft, bulletPos, Quaternion.identity);
        }
    }
    public void TakeDamage(int damage)
    {

        //Instantiate(bloodEffect,transform.position,Quaternion.identity);
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        //play enemy hurt animation

        Debug.Log(currentHealth);
        if (animator.GetBool("IsBattle") && currentHealth <= 0)
        {
            // Player.Stats.score += scorePlayerWillGetWhenKillIchigo;
            // scorePlayerWillGetWhenKillIchigo = 0;
            // scoreText.text = Player.Stats.score.ToString();
            EndBattle();
            //Dead();
        }
    }

    public void Dead()
    {
        //Enemy dead animation 
        animator.SetTrigger("IsDead");
        healthBar.gameObject.SetActive(false);
        Destroy(healthBar);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        active.GetComponent<ActiveBoss>().BossUnActived();
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, Player.Stats.rangeAttack);
    }
}
