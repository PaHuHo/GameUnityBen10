using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private float fallDelay=0.01f;
    private float destroyDelay=2f;

    public Rigidbody2D rb;

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")){
            StartCoroutine(Fall());
        }
    }
    private IEnumerator Fall(){
        yield return new WaitForSeconds(fallDelay);
        PlatformManager.Instance.StartCoroutine("RespawnPlatform",new Vector2(transform.position.x,transform.position.y));
        //rb.bodyType=RigidbodyType2D.Dynamic;
        Invoke("DropPlatform",0.5f);
        Destroy(gameObject,destroyDelay);
    }
    void DropPlatform(){
        rb.isKinematic=false;
    }
}
