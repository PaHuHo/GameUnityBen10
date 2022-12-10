using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TreasureTrigger : MonoBehaviour
{
    bool playerDetected;
    public Animator animator;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Player.Stats.quest != null)
        {
            if (other.tag == "Player" && Player.Stats.quest.goal.haveKey)
            {
                playerDetected = true;
                animator.SetBool("IsOpened", true);
            }
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerDetected = false;
            animator.SetBool("IsOpened", false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Player.Stats.quest != null)
        {
            if (Player.Stats.quest.isActive && Player.Stats.quest.goal.haveKey && playerDetected && Input.GetKeyDown(KeyCode.E))
            {
                Player.Stats.quest.goal.ItemCollected();
                Player.Stats.quest.goal.haveKey=false;           
            }
        }
    }
}
