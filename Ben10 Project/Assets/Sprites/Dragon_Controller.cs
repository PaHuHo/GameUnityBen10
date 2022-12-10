using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;

public class Dragon_Controller : MonoBehaviour
{
    Vector3 position1, position2;
    public int numberOfDragon = 0;
    public int damage;
    public int maxHealth;
    public Animator animator;
    int currentHealth;
    public float speed;
    public float minX, minY, maxX, maxY;
    public int giveScore;
    Vector3 nextPosition;
    private bool isSetup=false;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        animator.SetInteger("NumberOfDragon", numberOfDragon);
        position1 = new Vector3(Random.Range(minX, minX + 40),
                            Random.Range(minY, maxY), 0);
        position2 = new Vector3(Random.Range(maxX - 40, maxX),
                            Random.Range(minY, maxY), 0);
        nextPosition = position2;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isSetup){
            animator.SetInteger("NumberOfDragon", numberOfDragon);
           
        }
        if (transform.position == position1)
        {
            nextPosition = position2;
            position1 = new Vector3(Random.Range(minX, minX + 40),
                           Random.Range(minY, maxY), 0);
        }
        if (transform.position == position2)
        {
            nextPosition = position1;
            position2 = new Vector3(Random.Range(maxX - 40, maxX),
                            Random.Range(minY, maxY), 0);
        }
        if (transform.position.x < nextPosition.x)
        {
            transform.localScale = new Vector3(3f, 3f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(-3f, 3f, 1f);
        }
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.name == "Player")
        {
            other.gameObject.GetComponent<PlayerCombat>().TakeDamage(damage);
        }
    }

    public void TakeDamage(int damage)
    {

        //Instantiate(bloodEffect,transform.position,Quaternion.identity);
        currentHealth -= damage;

        //play enemy hurt animation

        Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {
            // Player.Stats.score +=scorePlayerWillGetWhenKillDragon;
            // scorePlayerWillGetWhenKillDragon=0;
            // scoreText.text = Player.Stats.score.ToString();
            GameObject player = GameObject.Find("Player");
            player.gameObject.GetComponent<PlayerController>().UpdatScore(giveScore, true);
            Dead();
        }
    }

    void Dead()
    {

        //Disable the enemy
        Destroy(this.gameObject);
        // GetComponent<Collider2D>().enabled=false;
        // this.enabled=false;
    }

}
