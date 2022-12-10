using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerCollision : MonoBehaviour
{
    public Transform repawnPosition;
    public HealthBar healthBar;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Deathzone"))
        {
            this.gameObject.GetComponent<PlayerController>().UpdatScore(100, false);
            this.gameObject.GetComponent<PlayerCombat>().Respawn();

        }

        if (other.CompareTag("Respawn"))
        {
            repawnPosition = other.gameObject.GetComponent<Transform>();
            // if(Player.Stats.canSave){
            //     SaveSystem.SaveGame(new GameData());
            // }
        }
        // if(other.CompareTag("Portal")){
        //     // GameObject playerObj = GameObject.Find("Player");
        //     // playerObj.transform.position= new Vector2(82,1);
        //     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        // }

        if (other.CompareTag("Diamond"))
        {
            this.gameObject.GetComponent<PlayerController>().UpdatScore(other.gameObject.GetComponent<DiamondController>().ScoreWhenGetDiamond,true);
            
            // Player.Stats.score += other.gameObject.GetComponent<DiamondController>().ScoreWhenGetDiamond;
            // other.gameObject.GetComponent<DiamondController>().ScoreWhenGetDiamond = 0;
            //scoreText.text = Player.Stats.score.ToString();
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Lava"))
        {
            this.gameObject.GetComponent<PlayerCombat>().TakeDamage(30);
        }

        if (other.CompareTag("Thorn"))
        {
            this.gameObject.GetComponent<PlayerCombat>().TakeDamage(10);
            // Player.Stats.currentHealth -= 10;
            // healthBar.SetHealth(Player.Stats.currentHealth);
        }

    }

}
