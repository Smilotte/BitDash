/*
 * The file is used to control camera to following player 27/10/2020 @Xiaoyan Zhou
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager2 : MonoBehaviour
{
    //player position
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
    
    }

    //Camera position
    public void LateUpdate()
    {
        //player position x,y
        float playerPositionX = player.position.x;
        float cameraPositionX = transform.position.x;

        if (player != null)
        {
            //camera follow
            if (cameraPositionX != playerPositionX)
            {
                transform.position = new Vector3(playerPositionX, transform.position.y, transform.position.z);
            }
           
        }
    }
}
