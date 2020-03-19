using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Animal : MonoBehaviour, IClickable
{
    protected new string name;
    protected int legs;

    protected Animator animator;
    protected new Rigidbody rigidbody;
    protected NavMeshAgent navMeshAgent;
    protected FSM fsm;

    private float jumpForce;
    private float pushForce;

    [SerializeField] private float runSpeedMultiplier = 2f;
    private float baseSpeed;
    private bool isRunning;

    protected virtual void Start()
    {
        jumpForce = 200;
        pushForce = 50;

        // get references to components
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        // remember the base speed
        baseSpeed = navMeshAgent.speed;

        // create and initialize the new FSM
        fsm = new FSM();
        fsm.Initialize(this.gameObject);
    }

    protected virtual void Update()
    {
        fsm.UpdateState();
    }

    public void Clicked()
    {
        ReactToClick();
    }

    protected virtual void ReactToClick()
    {
        // add a little push from the player
        // Vector3 pushDirection = transform.position - CameraController.myTransform.position;
        // rigidbody.AddForce(pushForce * pushDirection);
        
        // more specific behavior implemented in subclasses
    }

    protected void LookAtPlayer()
    {
        // calculates position to look at
        // and sets transform
        Vector3 lookAt = CameraController.myTransform.position;
        lookAt.y = transform.position.y;
        transform.LookAt(lookAt);
    }

    protected abstract void MakeSound();

    protected virtual void Jump()
    {
        rigidbody.AddForce(new Vector3(0, jumpForce, 0));
    }

    protected virtual void ToggleRun()
    {
        isRunning = !isRunning;

        // set speed accordingly
        if (isRunning)
            navMeshAgent.speed = baseSpeed * runSpeedMultiplier;
        else
            navMeshAgent.speed = baseSpeed;
    }
}
