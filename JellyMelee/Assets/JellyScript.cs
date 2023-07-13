using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyScript : MonoBehaviour
{
  // Params
  public Rigidbody2D jellyRigidBody;
  public BoxCollider2D jellyBoxCollider;
  public LayerMask groundLayer;

  public float moveSpeed;
  
  public float jumpSpeed;
  public float maxJumpDuration_s;
  private float jumpTime;
  public float fastfallSpeed;

  public float hyperJumpAcc;
  public float hyperJumpMaxSpeed;
  private float hyperJumpTime;
  private bool hyperJumping;

  public float minY = -31.29f;

  // Controls
  public KeyCode moveRightKey = KeyCode.RightArrow;
  public KeyCode moveLeftKey = KeyCode.LeftArrow;
  public KeyCode jumpKey = KeyCode.UpArrow;
  public KeyCode fastfallKey = KeyCode.DownArrow;
  public KeyCode hyperJumpKey = KeyCode.Space;

  // Start is called before the first frame update
  void Start()
  {
    moveSpeed = 50.0f;
    jumpSpeed = 50.0f;
    maxJumpDuration_s = 0.3f;
    fastfallSpeed = 30.0f;
    hyperJumpMaxSpeed = 500.0f;
    hyperJumpAcc = 300.0f;
  }

  private bool IsGrounded()
  {
    return Physics2D.BoxCast(
      jellyBoxCollider.bounds.center, jellyBoxCollider.bounds.size, 0f, Vector2.down, 1.0f, groundLayer);
  }

  // Update is called once per frame
  void Update()
  {
    // Left/Right movement
    if (!hyperJumping) {
      if (Input.GetKey(moveRightKey)) {
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
      } else if (Input.GetKey(moveLeftKey)) {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
      }
    }
    // Air movement
    if (!IsGrounded()) {
      if (Input.GetKey(fastfallKey)) {
        jellyRigidBody.velocity += Vector2.down * fastfallSpeed;
      }
    }

    // Jumping
    if (!hyperJumping) {
      if (Input.GetKeyDown(jumpKey) && IsGrounded()) {
        jumpTime = Time.time;
        // Debug.Log("jumpTime: " + jumpTime);
      }
      if (Input.GetKey(jumpKey) && Time.time < jumpTime + maxJumpDuration_s) {
        // Debug.Log("Time: " + Time.time);
        jellyRigidBody.velocity = Vector2.up * jumpSpeed;
      }
    }

    // Hyper jumping
    if (IsGrounded()) {
      if (Input.GetKeyDown(hyperJumpKey)) {
        Debug.Log("Hyper jump key pressed");
        hyperJumpTime = Time.time;
        hyperJumping = true;
      }
      if (Input.GetKeyUp(hyperJumpKey) && hyperJumping) {
        Debug.Log("Hyper Jump key released, total time (s): " + (Time.time - hyperJumpTime));
        float hyperJumpSpeed = Math.Min(hyperJumpAcc * (Time.time - hyperJumpTime), hyperJumpMaxSpeed);
        Debug.Log("Hyper Jump speed: " + hyperJumpSpeed);
        jellyRigidBody.velocity = Vector2.up * hyperJumpSpeed;

        hyperJumping = false;
      }
    }

    // Prevent clipping
    if (jellyRigidBody.position.y < minY) {
      jellyRigidBody.position = new (jellyRigidBody.position.x, minY);
    }
  }
}