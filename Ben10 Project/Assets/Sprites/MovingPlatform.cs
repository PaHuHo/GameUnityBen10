using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public  GameObject player;
    public Transform positon1,position2;
    public float speed;
    public Transform startPosition;

    Vector3 nextPosition;
    // Start is called before the first frame update
    void Start()
    {
        nextPosition=startPosition.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position==positon1.position){
            nextPosition=position2.position;
        }
        if(transform.position==position2.position){
            nextPosition=positon1.position;
        }

        transform.position=Vector3.MoveTowards(transform.position,nextPosition,speed*Time.deltaTime);
    }
    private void OnDrawGizmos() {
        Gizmos.DrawLine(positon1.position,position2.position);
    }
}
