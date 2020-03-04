using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    protected new string name;
    protected int legs;

    protected Animator animator;
    protected new Rigidbody rigidbody;

    private float jumpForce;
    private float pushForce;

    protected virtual void Start()
    {
        jumpForce = 200;
        pushForce = 50;

        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }

    protected virtual void Update()
    {
        
    }

    public void Clicked()
    {
        ReactToClick();
    }

    protected virtual void ReactToClick()
    {
        // add a little push from the player
        Vector3 pushDirection = transform.position - CameraController.myTransform.position;
        rigidbody.AddForce(pushForce * pushDirection);
        
        // more specific behavior implemented in subclasses
    }

    protected void LookAtPlayer()
    {
        // look at player
        Vector3 lookAt = CameraController.myTransform.position;
        lookAt.y = transform.position.y;
        transform.LookAt(lookAt);
    }

    protected virtual void MakeSound()
    {

    }

    protected virtual void Jump()
    {
        rigidbody.AddForce(new Vector3(0, jumpForce, 0));
    }
}
