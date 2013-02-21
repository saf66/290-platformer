using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]

public class CameraController : MonoBehaviour {

	public Transform player = null;
	public float smoothness = 0.1f;
	
	private float xv = 0.0f;
	private float yv = 0.0f;

    void FixedUpdate () {
		Vector3 u = transform.position;
		Vector3 v = player.position;
		Vector3 w = player.rigidbody.velocity;
		u.x = Mathf.SmoothDamp(u.x, v.x + w.x * Time.fixedDeltaTime, ref xv, smoothness);
		u.y = Mathf.SmoothDamp(u.y, v.y + w.y * Time.fixedDeltaTime, ref yv, smoothness);
        transform.position = u;
    }
}
