using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum StateType { Idle, Wander, Flee }

public abstract class State
{
    protected FSM owner;

    internal UnityEvent onEnter = new UnityEvent();
    internal UnityEvent onExit = new UnityEvent();
    
    public void Initialize(FSM owner)
    {
        this.owner = owner;
    }

    public virtual void Enter()
    {
        onEnter.Invoke();
    }

    public abstract void Update();

    public virtual void Exit()
    {
        onExit.Invoke();
    }
}

public class AnimalIdle : State
{
    public override void Enter()
    {
        base.Enter();

        owner.NavMeshAgent.isStopped = true;
        owner.Animator.SetInteger("Walk", 0);
    }

    public override void Exit()
    {
        base.Exit();

        owner.NavMeshAgent.isStopped = false;
    }

    public override void Update()
    {

    }
}

public class AnimalWander : State
{
    public override void Enter()
    {
        base.Enter();

        owner.Animator.SetInteger("Walk", 1);
        SetDestination();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        // reached destination?
        if(owner.NavMeshAgent.remainingDistance < 1f)
        {
            SetDestination();
        }
    }

    protected void SetDestination()
    {
        // set a random destination
        owner.NavMeshAgent.SetDestination(CameraController.GetRandomDestination());
    }
}

public class AnimalFlee : State
{
    public override void Enter()
    {
        base.Enter();
        owner.Animator.SetInteger("Walk", 1);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        // run away from player

        // calculate the direction vector
        Vector3 direction = owner.Transform.position - CameraController.myTransform.position;
        direction.Normalize();

        // set the destination in that direction from this position
        owner.NavMeshAgent.SetDestination(owner.Transform.position + (direction * 10));
    }
}


