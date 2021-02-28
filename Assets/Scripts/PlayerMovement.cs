using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 100f;

    private float lastPositionX;

    private MoveDirection moveDirection;
    private Vector2 directionVector {
        get {
            return moveDirection == MoveDirection.Right ? Vector2.right : Vector2.left;
        }
    }
    private Transform _transform;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private bool startAction = false;

    private void Awake()
    {
        _transform = this.transform;
        _rigidbody = this.GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
        _rigidbody.isKinematic = true;
    }

    public void SetStartAction(bool active) 
    {
        startAction = active;
        _rigidbody.isKinematic = !active;
    }


    private void FixedUpdate()
    {
        if (!startAction)
            return;

        _rigidbody.AddRelativeForce(directionVector * speed - _rigidbody.velocity);
        if (Input.GetKeyDown(KeyCode.Space)){
            if (Mathf.Abs(_rigidbody.velocity.y) < 0.01f)
            {
                Jump();
            }
            else if(Mathf.Abs(_transform.position.x - lastPositionX) < 0.001f){ 
                JumpAndChangeDirection();
            }

        }
        lastPositionX = _transform.position.x;
    }
    private void LateUpdate()
    {
        if (Mathf.Abs(_rigidbody.velocity.y) < 0.01f)
        {
            if (Mathf.Abs(_transform.position.x - lastPositionX) < 0.001f)
            {
                _animator.SetBool("Run", false);
            }
            else{
                _animator.SetBool("Run", true);
            }

        }
        else
        {
                _animator.SetBool("Run", false);
            
        }
    }

    private void Jump() {
        _animator.SetTrigger("Jump");
        _rigidbody.AddRelativeForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x * 0.5f, _rigidbody.velocity.y);
    }
    private void JumpAndChangeDirection() {
        _animator.SetTrigger("Jump");
        _rigidbody.AddForce((Vector2.up - directionVector * 0.25f) * jumpForce , ForceMode2D.Impulse);
        ChangeDirection();
    }

    private void ChangeDirection() {
        moveDirection = moveDirection == MoveDirection.Right ? MoveDirection.Left : MoveDirection.Right;
        _transform.localScale = new Vector3(directionVector.x, 1, 1);
    }
}

public enum MoveDirection { 
    Right,
    Left
}