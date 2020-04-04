using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float MovementSpeed;
    public Rigidbody2D CharacterRigidbody;
    public Vector2 JumpForce;
    public BoxCollider2D CharacterCollider;

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = GetInputMovementVector() * MovementSpeed * Time.deltaTime;
        transform.position += new Vector3(movement.x, movement.y, 0);

        if (IsTouchingWorld())
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
        if (Input.GetKeyDown(KeyCode.Space))
            CharacterRigidbody.AddForce(JumpForce);
    }

    bool IsTouchingWorld()
    {
        Vector2 point = CharacterCollider.bounds.center;
        Vector2 size = CharacterCollider.bounds.size;
        float angle = 0;

        const int worldLayer = 8;
        int layerMask = 1 << worldLayer;

        Collider2D overlappedCollider = Physics2D.OverlapBox(point, size, angle, layerMask);

        // Physics2D.OverlapBox() checks if anything is overlapping with the box (in this case, the character). If it is NOT, then it returns null
        // To make it so that overlappedCollider only looks at the world and doesn't collide with itself, you used a layer mask

        // There are 32 layers in the world, because integers have 32 bits (a bit is a 1 or 0), so each layer is associated with a bit. To select a bit, it has to be 
        // equal to 1 (not zero). So, in this case, since our world layer (the layer we want to interact with) is layer 8, we want bit 8 to be 1, but
        // all the other layers to be 0s.
        // To do this, we take the integer 1, since it is 31 0s follwed by one 1, and shift the bits to the left 8 times (1 << worldLayer). Now, the 8th bit is a 1, and 
        // is selected, but none of the other layers are.

        if (overlappedCollider == null)
            return false;

        else
            return true;
    }
}
