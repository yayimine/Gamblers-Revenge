using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Update is called once per frame
    public virtual void LateUpdate()
    {
        if (PlayerController.instance == null) return; // If the player doesn't exist, do nothing
        transform.position = new Vector3(PlayerController.instance.transform.position.x, //take the player's x position
            PlayerController.instance.transform.position.y, //take the player's y position
            transform.position.z); //keep the original z position for the camera
    }
}


