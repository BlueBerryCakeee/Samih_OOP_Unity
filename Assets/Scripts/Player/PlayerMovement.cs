using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{ 
    [SerializeField] Vector2 maxSpeed;
    [SerializeField] Vector2 timeToFullSpeed;
    [SerializeField] Vector2 timeToStop;
    [SerializeField] Vector2 stopClamp;
    Vector2 moveDirection;
    Vector2 moveVelocity;
    Vector2 moveFriction;
    Vector2 stopFriction;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveVelocity = 2 * maxSpeed/timeToFullSpeed;
        moveFriction = (-2) * maxSpeed/(timeToFullSpeed * timeToFullSpeed);
        stopFriction = (-2) * maxSpeed/(timeToStop * timeToStop);
    }

    public void Move()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        moveDirection = new Vector2(inputX, inputY);

        Vector2 friction = GetFriction();
        rb.velocity = new Vector2(
            moveVelocity.x * moveDirection.x + friction.x * moveDirection.x,
            moveVelocity.y * moveDirection.y + friction.y * moveDirection.y
        );
    }
    public Vector2 GetFriction()
    {
        if (moveDirection == Vector2.zero)
        {
            return stopFriction;
        }
        else
        {
            return moveFriction;
        }
    }

    public bool isMoving()
    {
        return rb.velocity != Vector2.zero;
    }
}
