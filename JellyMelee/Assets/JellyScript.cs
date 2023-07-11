using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyScript : MonoBehaviour
{
  // Params
  public Rigidbody2D jellyRigidBody;
  public BoxCollider2D jellyBoxCollider;
  public float moveSpeed;
  public float jumpSpeed;
  public float fastfallSpeed;
  public float hyperJumpMaxSpeed;
  public float hyperJumpAcc;

  public LayerMask jumpableGround;

  // Controls
  public KeyCode moveRightKey = KeyCode.RightArrow;
  public KeyCode moveLeftKey = KeyCode.LeftArrow;
  public KeyCode jumpKey = KeyCode.UpArrow;
  public KeyCode fastfallKey = KeyCode.DownArrow;
  public KeyCode hyperJumpKey = KeyCode.Space;

  // Start is called before the first frame update
  void Start()
  {
    moveSpeed = 40.0f;
    jumpSpeed = 70.0f;
    fastfallSpeed = 30.0f;
    hyperJumpMaxSpeed = 100.0f;
    hyperJumpAcc = 20.0f;
  }

  private bool IsGrounded()
  {
    return Physics2D.BoxCast(
      jellyBoxCollider.bounds.center, jellyBoxCollider.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKey(moveRightKey)) {
      transform.position += Vector3.right * moveSpeed * Time.deltaTime;
    } else if (Input.GetKey(moveLeftKey)) {
      transform.position += Vector3.left * moveSpeed * Time.deltaTime;
    }

    if (Input.GetKeyDown(jumpKey) && IsGrounded()) {
      jellyRigidBody.velocity += Vector2.up * jumpSpeed;
    }

    if (Input.GetKey(fastfallKey)) {
      jellyRigidBody.velocity += Vector2.down * fastfallSpeed;
    }

    // if (Input.GetKey(hyperJumpKey)) {

    // }
  }
}
