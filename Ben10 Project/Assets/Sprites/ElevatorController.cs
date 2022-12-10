using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    public bool isHorizontal;
    public float startX;
    public float startY;
    public float endX;
    public float endY;
    public float speed;

    public Transform elevatorTransform;

    private bool onGoUp = true;
    private bool onGoRight = true;
    // Start is called before the first frame update
    void Start()
    {
        //elevatorTransform.position = new Vector2(startX,startY);

    }

    // Update is called once per frame
    void Update()
    {
        if(isHorizontal){
            if(onGoRight){
                elevatorTransform.position = new Vector2(elevatorTransform.position.x+speed, startY);
                if(elevatorTransform.position.x>=endX){
                    onGoRight=false;
                }
            }
            else{
                elevatorTransform.position = new Vector2(elevatorTransform.position.x-speed, startY);
                if(elevatorTransform.position.x<=startX){
                    onGoRight=true;
                }
            }
        }
        else{
            if(onGoUp){
                elevatorTransform.position = new Vector2(startX, elevatorTransform.position.y+speed);
                if(elevatorTransform.position.y>=endY){
                    onGoUp=false;
                }
            }
            else{
                elevatorTransform.position = new Vector2(startX, elevatorTransform.position.y-speed);
                if(elevatorTransform.position.y<=startY){
                    onGoUp=true;
                }
            }
        }
    }
}
