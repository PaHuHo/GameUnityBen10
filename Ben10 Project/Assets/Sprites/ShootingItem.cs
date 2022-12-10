using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingItem : MonoBehaviour
{
    public int damageShoot=30;
    public float velX=5f;
    public float timeShoot=1f;
    public GameObject boomItem;
    float velY=0f;
    Rigidbody2D rb;

    private void Start() {
        rb=GetComponent<Rigidbody2D>();
        Destroy(gameObject,timeShoot);
    }
    private void Update() {
        rb.velocity=new Vector2(velX,velY);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag=="Player"){
            Instantiate(boomItem,gameObject.transform.position,Quaternion.identity);
            other.gameObject.GetComponent<PlayerCombat>().TakeDamage(damageShoot);
            Destroy(gameObject);
        }
        else if(other.tag=="Boss"){
            Instantiate(boomItem,gameObject.transform.position,Quaternion.identity);
            other.gameObject.GetComponent<Boss>().TakeDamage(damageShoot);
            Destroy(gameObject);
        }
        else if(other.tag=="Slime") {
            Instantiate(boomItem,gameObject.transform.position,Quaternion.identity);
            other.gameObject.GetComponent<Enemy>().TakeDamage(damageShoot);
            Destroy(gameObject);
        }
        else if(other.tag=="Dragon") {
            Instantiate(boomItem,gameObject.transform.position,Quaternion.identity);
            other.gameObject.GetComponent<Dragon_Controller>().TakeDamage(damageShoot);
            Destroy(gameObject);
        }
        
    }
}
