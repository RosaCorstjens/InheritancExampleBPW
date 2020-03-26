using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public abstract class Animal : MonoBehaviour, IClickable
{
    protected new string name;
    protected int legs;

    protected Animator animator;
    protected new Rigidbody rigidbody;
    protected NavMeshAgent navMeshAgent;
    protected AnimalFSM fsm;

    private float jumpForce;
    private float pushForce;

    [SerializeField] private float runSpeedMultiplier = 2f;
    private float baseSpeed;
    private bool isRunning;

    private bool initialized = false;

    public virtual void Start()
    {
        // subscribe to the game manager
        GameManager.Instance.animals.Add(this);
    }

    protected virtual void Initialize()
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
        fsm = new AnimalFSM();
        fsm.Initialize(this.gameObject);

        initialized = true;
    }

    public virtual void DoUpdate()
    {
        if (!initialized)
            Initialize();

        fsm.UpdateState();
    }

    public void Clicked(bool leftMB, bool rightMB)
    {
        ReactToClick(leftMB, rightMB);
    }

    protected virtual void ReactToClick(bool leftMB, bool rightMB)
    {
        // specific behavior implemented in subclasses
    }

    protected void LookAtPlayer()
    {
        // calculates position to look at
        // and sets transform
        Vector3 lookAt = GameManager.Instance.controller.transform.position;
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

    public virtual void Die()
    {
        // play death particle
        GameObject.Instantiate(GameManager.Instance.bloodParticle, transform.position, Quaternion.identity);

        Destroy();
    }

    public virtual void Destroy()
    {
        // unsubscribe from the gamemanager
        GameManager.Instance.animals.Remove(this);

        Destroy(this.gameObject);
    }
}
