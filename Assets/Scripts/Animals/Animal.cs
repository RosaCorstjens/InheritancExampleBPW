using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public abstract class Animal : MonoBehaviour, IClickable
{
    protected new string name;
    protected int legs;

    public Animator animator;
    public new Rigidbody rigidbody;
    public NavMeshAgent navMeshAgent;
    protected AnimalFSM fsm;

    private float jumpForce = 200f;

    [SerializeField] private float runSpeedMultiplier = 2f;
    private float baseSpeed;
    private bool isRunning;

    protected float distToPlayer;

    public virtual void Start()
    {
        // setup the FSM
        SetupFSM();

        // remember the base speed
        baseSpeed = navMeshAgent.speed;

        // subscribe to the game manager
        GameManager.Instance.animals.Add(this);
    }

    protected virtual void SetupFSM()
    {
        // create FSM
        fsm = new AnimalFSM(this);
    }

    public virtual void Update()
    {
        // return if not in play
        if (GameManager.Instance.fsm.currentState.GetType() != typeof(PlayState))
            return;

        // update fsm
        fsm.Update();

        // calculate distance to player
        distToPlayer = Vector3.Distance(transform.position, Camera.main.transform.position);
    }

    #region INTERACTIONS
    public void Clicked(bool leftMB, bool rightMB)
    {

        ReactToClick(leftMB, rightMB);
    }

    protected abstract void ReactToClick(bool leftMB, bool rightMB);

    protected abstract void MakeSound();

    public virtual void Die()
    {
        // play death particle
        GameObject.Instantiate(GameManager.Instance.bloodParticle, transform.position, Quaternion.identity);

        // unsubscribe from the gamemanager
        GameManager.Instance.animals.Remove(this);

        // destroy object
        Destroy(gameObject);
    }
    #endregion

    #region MOVEMENT
    public void SetMoving(bool moving)
    {
        navMeshAgent.isStopped = !moving;
        animator.SetInteger("Walk", moving ? 1 : 0);
    }

    public void SetRun(bool run)
    {
        // switch running
        isRunning = run;

        // set speed accordingly
        if (isRunning)
            navMeshAgent.speed = baseSpeed * runSpeedMultiplier;
        else
            navMeshAgent.speed = baseSpeed;
    }

    public void Jump()
    {
        // add jump force to rb
        rigidbody.AddForce(new Vector3(0, jumpForce, 0));
    }

    public void LookAt(Vector3 lookAt)
    {
        // don't look up/down!
        lookAt.y = transform.position.y;

        // make transform look at position
        transform.LookAt(lookAt);
    }

    public void SetDestination(Vector3 destination)
    {
        // if the agent is stopped, start moving
        if (navMeshAgent.isStopped)
            SetMoving(true);

        // set the destination
        navMeshAgent.SetDestination(destination);
    }

    public float GetDistanceToDestination()
    {
        // return distance to target from agent
        return navMeshAgent.remainingDistance;
    }

    public float GetDistanceToPlayer()
    {
        // return magnitude of vector from player to me
        return (transform.position - Camera.main.transform.position).magnitude;
    }
    #endregion
}
