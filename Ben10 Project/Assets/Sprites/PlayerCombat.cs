using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public HealthBar healthBar;
    public Transform attackPoint;
    public LayerMask enemyLayers;

    public GameObject shootingItemToRight, shootingItemToLeft;
    public AudioSource attackSoundEffect;
    public AudioSource hurtSoundEffect;
    public AudioSource deathSoundEffect;
    public Rigidbody2D rigidbody_2D;
    void Start()
    {
        if (Player.Stats.isLoadGame)
        {
            healthBar.SetMaxHealth(Player.Stats.maxHealth);
            healthBar.SetHealth(Player.Stats.currentHealth);
        }
        else
        {
            Player.Stats.currentHealth = Player.Stats.maxHealth;
            healthBar.SetMaxHealth(Player.Stats.maxHealth);
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (Player.Stats.isConversation)
        {
            return;
        }
        // if (Time.time >= Player.Stats.nextAttackTime)
        // {
        //     if (Input.GetKeyDown("a") && Player.Stats.numberOfTransform == 0)
        //     {

        //         Attack();

        //         Player.Stats.nextAttackTime = Time.time + 1f / Player.Stats.attackRate;
        //     }
        // }
        // if (Input.GetKeyDown("s") && Time.time >= Player.Stats.nextShoot && Player.Stats.numberOfTransform == 1)
        // {
        //     animator.SetTrigger("Shooting");
        //     Player.Stats.nextShoot = Time.time + 1f / Player.Stats.shootRate;
        // }
        if (Time.time >= Player.Stats.nextAttackTime)
        {
            if (Input.GetKeyDown("a") && !Player.Stats.onTransform)
            {
                if (Player.Stats.numberOfTransform == 0)
                {
                    Attack();

                    Player.Stats.nextAttackTime = Time.time + 1f / Player.Stats.attackRate;
                }
                else
                {
                    animator.SetTrigger("Shooting");
                    Player.Stats.nextAttackTime = Time.time + 1f / Player.Stats.attackRate;

                }

            }
        }

    }
    IEnumerator Death()
    {
        deathSoundEffect.Play();

        GameObject boss = GameObject.Find("Boss");
        if (boss != null)
        {
            boss.gameObject.GetComponent<Boss>().Respawn(false);
        }
        animator.SetTrigger("Die");
        //Animation
        yield return new WaitForSeconds(0.5f);
        this.gameObject.GetComponent<PlayerController>().UpdatScore(20, false);
        //this.gameObject.GetComponent<PlayerController>().UpdatScore(100, false);
        Respawn();
        // if(Player.Stats.canSave){
        //     SaveSystem.SaveGame(new GameData());
        // }
    }
    public void Respawn()
    {
        Player.Stats.currentHealth = Player.Stats.maxHealth;
        healthBar.SetHealth(Player.Stats.currentHealth);

        Transform repawnPosition = this.gameObject.GetComponent<PlayerCollision>().repawnPosition;
        this.transform.position = new Vector2(repawnPosition.position.x, repawnPosition.position.y + 0.5f);
        rigidbody_2D.velocity = new Vector2(0, 0);


    }
    public void TakeDamage(int damage)
    {
        Player.Stats.currentHealth -= damage;
        hurtSoundEffect.Play();
        healthBar.SetHealth(Player.Stats.currentHealth);

        if (Player.Stats.currentHealth <= 0)
        {
            StartCoroutine(Death());
        }
    }
    void Attack()
    {
        //Play animation attack
        animator.SetTrigger("Attacking");
        //Sound Effect Attack
        attackSoundEffect.Play();
        //Detect enemy in  range attack
        Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(attackPoint.position, Player.Stats.rangeAttack, enemyLayers);
        //Damage them
        foreach (Collider2D enemy in hitEnemy)
        {
            // Debug.Log("We hit" + enemy.name);
            //enemy.GetComponent<Enemy>().TakeDamage(Player.Stats.attackDamage);
            if (enemy.CompareTag("Boss"))
            {
                enemy.GetComponent<Boss>().TakeDamage(Player.Stats.attackDamage);
            }
            else
            {
                enemy.GetComponent<Enemy>().TakeDamage(Player.Stats.attackDamage);
            }

        }
    }

    void Shoot()
    {
        if (!Player.Stats.canShoot)
        {
            return;
        }
        Vector2 bulletPos = transform.position;

        if (Player.Stats.facingRight)
        {
            bulletPos += new Vector2(+1.5f, 0.5f);
            Instantiate(shootingItemToRight, bulletPos, Quaternion.identity);
        }
        else
        {
            bulletPos += new Vector2(-1.5f, 0.5f);
            Instantiate(shootingItemToLeft, bulletPos, Quaternion.identity);
        }
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
