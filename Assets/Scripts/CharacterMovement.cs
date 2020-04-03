using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float MovementSpeed;
    public Rigidbody2D CharacterRigidbody;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = GetInputMovementVector() * MovementSpeed * Time.deltaTime;
        transform.position += new Vector3(movement.x, movement.y, 0);

        RunJumping();
    }

    Vector2 GetInputMovementVector()
    {
        Vector2 movement = new Vector2();

        if (Input.GetKey(KeyCode.D))
            movement.x++;

        if (Input.GetKey(KeyCode.A))
            movement.x--;

        return movement;
    }

    void RunJumping()
    {
        Vector2 force = new Vector2(0, 100);

        if (Input.GetKeyDown(KeyCode.Space))
            CharacterRigidbody.AddForce(force);
    }
}
