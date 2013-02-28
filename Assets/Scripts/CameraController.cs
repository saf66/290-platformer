/*
 * Author: Sean Fox (saf66)
 * Description:
 *    This class is responsible for making the camera follow player movement.
 */

using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(Camera))]

public class CameraController : MonoBehaviour {

	public Transform player = null;			// the object to track with the camera
	public const float smoothness = 0.1f;	// damping parameter
	
	private float xv = 0.0f;				// current horizontal velocity
	private float yv = 0.0f;				// current vertical velocity

    void FixedUpdate () {
		// get the camera position, player position, and player velocity
		Vector3 u = transform.position;
		Vector3 v = player.position;
		Vector3 w = player.rigidbody.velocity;
		// smoothly translate the camera based on the player position and velocity
		u.x = Mathf.SmoothDamp(u.x, v.x + w.x * Time.fixedDeltaTime, ref xv, smoothness);
		u.y = Mathf.SmoothDamp(u.y, v.y + w.y * Time.fixedDeltaTime, ref yv, smoothness);
        transform.position = u;
    }
}
