using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Keeps the attached object (usually the camera) positioned over the player
/// every frame.  The script only follows the player's x and y position while
/// preserving the current z depth so the camera stays in the correct plane.
/// </summary>
public class FollowPlayer : MonoBehaviour
{
    /// <summary>
    /// Called after all Update calls; repositions the object to follow the
    /// player if they exist in the scene.
    /// </summary>
    public virtual void LateUpdate()
    {
        if (PlayerController.instance == null) return; // If the player doesn't exist, do nothing
        transform.position = new Vector3(
            PlayerController.instance.transform.position.x, // take the player's x position
            PlayerController.instance.transform.position.y, // take the player's y position
            transform.position.z); // keep the original z position for the camera
    }
}


