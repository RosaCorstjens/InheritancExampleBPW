using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum AnimalStateType { Idle, Wander, Flee, Chase }

public abstract class AnimalState
{
    protected AnimalFSM owner;

    internal UnityEvent onEnter = new UnityEvent();
    internal UnityEvent onExit = new UnityEvent();
    
    public void Initialize(AnimalFSM owner)
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

public class AnimalIdle : AnimalState
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

public class AnimalWander : AnimalState
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
        owner.NavMeshAgent.SetDestination(GameManager.Instance.GetRandomDestination());
    }
}

public class AnimalFlee : AnimalState
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
        Vector3 direction = owner.Transform.position - GameManager.Instance.controller.transform.position;
        direction.Normalize();

        // set the destination in that direction from this position
        owner.NavMeshAgent.SetDestination(owner.Transform.position + (direction * 10));
    }
}

public class AnimalChase : AnimalState
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
        // chase from player

        // set the destination to the player
        owner.NavMeshAgent.SetDestination(GameManager.Instance.controller.transform.position);
    }
}


