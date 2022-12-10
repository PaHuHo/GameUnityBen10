using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;

    // Update is called once per frame
    private void Update()
    {
        transform.position= new Vector3(player.position.x,
        player.position.y>=12?12:player.position.y>=0?player.position.y:player.position.y>=-3.8?0:player.position.y+3.8f,
        transform.position.z);
        
    }
}
