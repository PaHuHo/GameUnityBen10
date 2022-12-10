using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;
public class BossRun : StateMachineBehaviour
{

    public float speed=2.5f;
    public float rangeAttack=3f;
    public float attackRate = 1f;
    public float nextAttackTime=0f;

    public float shootRate=1f;
    public float nextShoot=0f;
    private float nextTimeTeleport = 0f;

    private bool canShoot=true;
    Transform player;
    Rigidbody2D rb2D;
    Boss boss;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

       player=GameObject.FindGameObjectWithTag("Player").transform;
       rb2D=animator.GetComponent<Rigidbody2D>();
       boss=animator.GetComponent<Boss>();

       nextTimeTeleport=Time.time + Random.Range(2f,4f);
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.SetBool("IsBattle", boss.animator.GetBool("IsBattle"));
        if(!boss.animator.GetBool("IsBattle")){
            canShoot=false;
        }else{
            canShoot=true;
        }
        boss.LookAtPlayer();
        Vector2 target=new Vector2(player.position.x,rb2D.position.y);
        Vector2 newPostion=Vector2.MoveTowards(rb2D.position,target,speed*Time.fixedDeltaTime);
        rb2D.MovePosition(newPostion);

        if(Vector2.Distance(player.position,rb2D.position)>9f&&canShoot){
            if(Time.time >= nextShoot){
                animator.SetTrigger("RangeAttack");
                nextShoot = Time.time + 1f / shootRate;
            }
        }
        else if(Vector2.Distance(player.position,rb2D.position)<=rangeAttack){
            if(Time.time >= nextAttackTime){

            animator.SetTrigger("Attack");
            nextAttackTime = Time.time + 1f / attackRate;
            }
        }
        else{
            if(Time.time >= nextTimeTeleport){
                nextAttackTime = Time.time + Random.Range(0.2f,1f);
                rb2D.transform.position = new Vector2(player.position.x + Random.Range(-1f,1f), rb2D.transform.position.y);
                nextTimeTeleport=Time.time + Random.Range(4f,8f);
            }
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       animator.ResetTrigger("Attack");
    }

    
}
