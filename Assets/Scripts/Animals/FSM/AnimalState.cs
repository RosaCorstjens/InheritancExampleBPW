using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class AnimalState
{
    public enum Type { None, Idle, Chase, Wander, Flee }

    protected AnimalFSM fsm;

    public AnimalState(AnimalFSM fsm) { this.fsm = fsm; }

    public abstract void Enter();
    public abstract void Execute();
    public abstract void Exit();
}

public class Idle : AnimalState
{
    public Idle(AnimalFSM fsm) : base(fsm) { }

    public override void Enter()
    {
        // stop moving the animal
        fsm.owner.SetMoving(false);
    }

    public override void Execute() { }

    public override void Exit() { }
}

public class Wander : AnimalState
{
    public Wander(AnimalFSM fsm) : base(fsm) { }

    public override void Enter()
    {
        // sets a rndm destination
        SetDestination();
    }

    public override void Execute()
    {
        // reached destination? set a new one
        if (fsm.owner.GetDistanceToDestination() < 1f)
            SetDestination();
    }

    public override void Exit() { }

    private void SetDestination()
    {
        // set a random destination
        fsm.owner.SetDestination(GameManager.Instance.GetRandomDestination());
    }
}

public class Chase : AnimalState
{
    public Chase(AnimalFSM fsm) : base(fsm) { }

    private Transform chaseTarget = null;

    public override void Enter() { }

    public override void Execute()
    {
        // nothing to do without a target!
        if (chaseTarget == null)
        {
            fsm.owner.SetMoving(false);
            return;
        }

        // set the destination to the chase transform
        fsm.owner.SetDestination(chaseTarget.position);
    }

    public override void Exit() { }

    public void SetChaseTarget(Transform chaseTarget)
    {
        this.chaseTarget = chaseTarget;
    }
}

public class Flee : AnimalState
{
    public Flee(AnimalFSM fsm) : base(fsm) { }

    private Transform fleeTarget = null;

    public override void Enter() 
    {
        // run bitch run!
        fsm.owner.SetRun(true);
    }

    public override void Execute()
    {
        if (fleeTarget == null)
        {
            fsm.owner.SetMoving(false);
            return;
        }
            
        // calculate the vector away from the flee target
        // normalize it to only get the direction
        Vector3 direction = (fsm.owner.transform.position - fleeTarget.position).normalized;

        // set the destination in that direction 
        fsm.owner.SetDestination(fsm.owner.transform.position + (direction * 10));
    }

    public override void Exit()
    {
        // don't run
        fsm.owner.SetRun(false);
    }
}

