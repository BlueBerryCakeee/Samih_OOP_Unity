using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Vector2 maxSpeed;
    [SerializeField] Vector2 timeToFullSpeed;
    [SerializeField] Vector2 timeToStop;
    [SerializeField] Vector2 stopClamp;

    Vector2 playerPosition;
    Vector2 moveDirection;
    Vector2 moveVelocity;
    Vector2 moveFriction;
    Vector2 stopFriction;
    Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveVelocity = (2 * maxSpeed) / timeToFullSpeed;
        moveFriction = (-2 * maxSpeed) / (timeToFullSpeed * timeToFullSpeed);
        stopFriction = (-2 * maxSpeed) / (timeToStop * timeToStop);
    }


    public void Move()
    {

        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));


        if (moveDirection.magnitude > 1)
        {
            moveDirection.Normalize();
        }


        moveVelocity = moveDirection * maxSpeed;


        Vector2 friction = GetFriction();
        moveVelocity -= friction * Time.deltaTime;


        moveVelocity.x = Mathf.Clamp(moveVelocity.x, -maxSpeed.x, maxSpeed.x);
        moveVelocity.y = Mathf.Clamp(moveVelocity.y, -maxSpeed.y, maxSpeed.y);


        rb.velocity = moveVelocity;


        if (Mathf.Abs(rb.velocity.x) < stopClamp.x && Mathf.Abs(rb.velocity.y) < stopClamp.y)
        {
            rb.velocity = Vector2.zero;
        }

        MoveBound();
    }

    Vector2 GetFriction()
    {
        if (moveDirection.magnitude > 0)
        {
            return new Vector2(
                moveDirection.x != 0 ? moveFriction.x : 0,
                moveDirection.y != 0 ? moveFriction.y : 0
            );
        }
        else
        {
            return new Vector2(
                moveVelocity.x != 0 ? stopFriction.x : 0,
                moveVelocity.y != 0 ? stopFriction.y : 0
            );
        }
    }

    void MoveBound()
    {
        Vector3 pos = transform.position;
        Vector3 minBounds = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        Vector3 maxBounds = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));
        float minX = minBounds.x + 0.5f;
        float minY = minBounds.y + 0.5f;
        float maxX = maxBounds.x - 0.5f;
        float maxY = maxBounds.y - 0.5f;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.x = Mathf.Clamp(pos.x, minBounds.x, maxX);
        pos.y = Mathf.Clamp(pos.y, minBounds.y, maxY);

        transform.position = pos;
    }

    public bool IsMoving()
    {
        return rb.velocity.magnitude > 0.000001f;
    }
}