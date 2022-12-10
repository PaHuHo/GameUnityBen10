using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{

    public Transform groundCheck;
    public LayerMask groundLayer;
    public LayerMask lavaLayer;
    public LayerMask playerLayer;
    public LayerMask enemyLayer;
    public Transform bodyLeftTopPointCheck;
    public Transform bodyRightBottomPointCheck;
    public Transform playerTransform;
    public SpriteRenderer sprite;
    public Animator animator;
    public BoxCollider2D boxCollider2D;
    public Rigidbody2D rigidbody_2D;
    public TrailRenderer trailRenderer;
    public HealthBar healthBar;

    public AudioSource jumpSoundEffect;

    public TextMeshProUGUI scoreText;
    // Start is called before the first frame update

    public void UpdatScore(int score, bool increasing)
    {
        if (increasing)
        {
            Player.Stats.score += score;
            scoreText.text = Player.Stats.score.ToString();
        }
        else
        {
            Player.Stats.score -= score;
            if(Player.Stats.score<0)
            {
                Player.Stats.score=0;
            }
            scoreText.text = Player.Stats.score.ToString();
        }

    }

    void Start()
    {
        Player.Stats.playerTransform = playerTransform;
        Player.Stats.numberOfScene = SceneManager.GetActiveScene().buildIndex;
        Player.Stats.facingRight = true;
        scoreText.text = Player.Stats.score.ToString();
        if (Player.Stats.playerObj == null)
        {
            Player.Stats.playerObj = GameObject.Find("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.Stats.isDasing)
        {
            return;
        }
        if(Player.Stats.isConversation){
            return;
        }

        Player.Stats.dirX = Input.GetAxisRaw("Horizontal");
        rigidbody_2D.velocity = new Vector2(Player.Stats.dirX * 7f, rigidbody_2D.velocity.y);

        UpdateAnimation();
        // if(Input.GetKey("d")){
        //     GetComponent<Rigidbody2D>().velocity=new Vector2(2,0);
        // }
        // if(Input.GetKey("a")){
        //     GetComponent<Rigidbody2D>().velocity=new Vector3(-2,0,0);
        // }
        if (Input.GetButtonDown("Jump") && Player.Stats.isGrounded && !Player.Stats.onTransform)
        {
            jumpSoundEffect.Play();
            rigidbody_2D.velocity = new Vector3(rigidbody_2D.velocity.x, 12f, 0);
            animator.SetTrigger("Jump");
        }
        if (Player.Stats.numberOfTransform != 0)
        {
            if (Player.Stats.endTimeTransform < Time.time)
            {
                TransformToBen10();
            }
        }
        if (Input.GetKeyDown("x") && Player.Stats.numberOfTransform == 0 && Player.Stats.timeCanTransform <= Time.time)
        {
            TransformToHeadblast();
        }


        if (Input.GetKeyDown(KeyCode.LeftShift) && Player.Stats.canDash && !Player.Stats.onTransform)
        {
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        if (Player.Stats.isDasing)
        {
            return;
        }
        Player.Stats.isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        Player.Stats.onLava = Physics2D.OverlapArea(bodyLeftTopPointCheck.position, bodyRightBottomPointCheck.position, lavaLayer);

        if (Player.Stats.onLava && !Player.Stats.delayLavaDamage && Player.Stats.numberOfTransform != 1)
        {
            StartCoroutine(OnLava());
        }

        if (Player.Stats.dirX > 0f && !Player.Stats.facingRight)
        {
            Flip();
        }
        else if (Player.Stats.dirX < 0f && Player.Stats.facingRight)
        {
            Flip();
        }
    }
    // void OnCollisionStay2D(Collision2D other)
    // {
    //     if (other.gameObject.CompareTag("Ground"))
    //     {
    //         canJump = true;
    //     }
    // }
    // private void OnCollisionExit2D(Collision2D other) {
    //     canJump = false;
    // }
    private void UpdateAnimation()
    {
        if (Player.Stats.dirX > 0f)
        {
            animator.SetBool("Walking", true);
        }
        else if (Player.Stats.dirX < 0f)
        {
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }
    }
    void Flip()
    {
        Player.Stats.facingRight = !Player.Stats.facingRight;

        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        //gameObject.transform.Rotate(0f,180f,0f);

    }
    //Kết thúc biến hình thành headblast
    void EndTransformToHeadblast()
    {
        animator.SetBool("OnTransform", false);
        Player.Stats.onTransform=false;
        
        playerTransform.transform.localScale = new Vector3(Player.Stats.facingRight ? 1f : -1f, 1f, 1f);
        boxCollider2D.size = Player.Stats.sizeOfBoxColliderPlayer;
        Player.Stats.timeCanTransform = Time.time + Player.Stats.cooldownTransform;
        groundCheck.position = new Vector3(Player.Stats.playerTransform.position.x-0.016f,Player.Stats.playerTransform.position.y -0.67f, groundCheck.position.z);
    }
    //Kết thúc biến hình từ headblast trở lại ben.
    void EndEndTransformFromHeadblast()
    {
        animator.SetBool("OnTransform", false);
        Player.Stats.onTransform=false;



        boxCollider2D.size = Player.Stats.sizeOfBoxColliderHeadblast;
        playerTransform.transform.localScale = new Vector3(Player.Stats.sizeOfHeadblast * (Player.Stats.facingRight ? 1f : -1f), Player.Stats.sizeOfHeadblast, 1f);
        groundCheck.position = new Vector3(groundCheck.position.x, groundCheck.position.y + 0.2f, groundCheck.position.z);

    }


    void TransformToHeadblast()
    {
        Player.Stats.endTimeTransform = Time.time + Player.Stats.timeToTransform;
        Player.Stats.numberOfTransform = 1;
        animator.SetBool("OnTransform", true);
        Player.Stats.onTransform=true;
        animator.SetInteger("NumberOfTransform", Player.Stats.numberOfTransform);

    }

    void TransformToBen10()
    {
        Player.Stats.numberOfTransform = 0;
        animator.SetBool("OnTransform", true);
        Player.Stats.onTransform=true;
        animator.SetInteger("NumberOfTransform", Player.Stats.numberOfTransform);


    }

    private IEnumerator Dash()
    {
        Physics2D.IgnoreLayerCollision(7, 9, true);
        Player.Stats.canDash = false;
        Player.Stats.isDasing = true;
        float originalGravity = rigidbody_2D.gravityScale;
        rigidbody_2D.gravityScale = 0f;
        rigidbody_2D.velocity = new Vector2(transform.localScale.x * Player.Stats.dasingPower, 0f);
        trailRenderer.emitting = true;
        yield return new WaitForSeconds(Player.Stats.dasingTime);
        trailRenderer.emitting = false;
        rigidbody_2D.gravityScale = originalGravity;
        Player.Stats.isDasing = false;
        yield return new WaitForSeconds(Player.Stats.dasingCooldown);
        Physics2D.IgnoreLayerCollision(7, 9, false);
        Player.Stats.canDash = true;
    }

    private IEnumerator OnLava()
    {
        Player.Stats.delayLavaDamage = true;
        this.GetComponent<PlayerCombat>().TakeDamage(10);
        yield return new WaitForSeconds(0.5f);
        Player.Stats.delayLavaDamage = false;
    }
}
