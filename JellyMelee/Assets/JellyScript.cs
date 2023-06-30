using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyScript : MonoBehaviour
{
    // Params
    public Rigidbody2D jellyRigidBody;
    public float moveSpeed;
    public float jumpSpeed;
    public float groundFriction;

    // Controls
    public KeyCode moveRightKey = KeyCode.RightArrow;
    public KeyCode moveLeftKey = KeyCode.LeftArrow;
    public KeyCode jumpKey = KeyCode.UpArrow;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 30.0f;
        jumpSpeed = 50.0f;
        groundFriction = 0.9f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(moveRightKey)) {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        } else if (Input.GetKey(moveLeftKey)) {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        } else {
            if (Mathf.Abs(jellyRigidBody.velocity.x) > 0.0) {
                jellyRigidBody.velocity.Set(jellyRigidBody.velocity.x * groundFriction, jellyRigidBody.velocity.y);
            }
        }

        if (Input.GetKeyDown(jumpKey)) {
            jellyRigidBody.velocity += Vector2.up * jumpSpeed;
        } 
    }
}
