using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dog : MonoBehaviour
{
    public Animator animator;
    public Rigidbody rigidbody;
    public NavMeshAgent navMeshAgent;
    private DogFSM fsm;

    private float jumpForce = 200f;
    private float pushForce = 50f;

    private float runSpeedMultiplier = 2f;
    private float baseSpeed;
    private bool isRunning;

    public float chaseUntilDist = 5f;
    public float chaseStartDist = 10f;

    public void Start()
    {
        // remember the base speed
        baseSpeed = navMeshAgent.speed;

        // create and setup the new FSM
        fsm = new DogFSM(this);
        fsm.AddState(typeof(DogIdle), new DogIdle(fsm));
        fsm.AddState(typeof(DogChasePlayer), new DogChasePlayer(fsm));

        // goto idle state for starters
        fsm.ChangeState(typeof(DogIdle));
    }

    public void Update()
    {
        // return if not in play
        if (GameManager.Instance.fsm.currentState.GetType() != typeof(PlayState))
            return;

        // update fsm
        fsm.Update();
    }

    public void LookAtPlayer(Vector3 lookAt)
    {
        // don't look up/down!
        lookAt.y = transform.position.y;

        // make transform look at position
        transform.LookAt(lookAt);
    }

    public void SetMoving(bool moving)
    {
        navMeshAgent.isStopped = !moving;
        animator.SetInteger("Walk", moving ? 1 : 0);
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
        return navMeshAgent.remainingDistance;
    }

    public float GetDistanceToPlayer()
    {
        return (transform.position - Camera.main.transform.position).magnitude;
    }

    public void Jump()
    {
        // add jump force to rb
        rigidbody.AddForce(new Vector3(0, jumpForce, 0));
    }

    public void ToggleRun()
    {
        // switch running
        isRunning = !isRunning;

        // set speed accordingly
        if (isRunning)
            navMeshAgent.speed = baseSpeed * runSpeedMultiplier;
        else
            navMeshAgent.speed = baseSpeed;
    }

    public void Die()
    {
        // play death particle
        GameObject.Instantiate(GameManager.Instance.bloodParticle, transform.position, Quaternion.identity);

        // destroy me
        Destroy(gameObject);
    }
}
