using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DogState
{
    protected DogFSM fsm;

    public DogState(DogFSM fsm) { this.fsm = fsm; }

    public virtual void Enter() { }
    public virtual void Execute() { }
    public virtual void Exit() { }
}

public class DogIdle : DogState
{
    public DogIdle(DogFSM fsm) : base(fsm) { }

    public override void Enter()
    {
        base.Enter();

        // stop moving the dog
        fsm.owner.SetMoving(false);
    }

    public override void Execute()
    {
        base.Execute();

        // TODO: check for state changes
        //if(fsm.owner.GetDistanceToPlayer() > fsm.owner.chaseStartDist)
        //    fsm.ChangeState(typeof(DogChasePlayer));
    }

    public override void Exit()
    {
        base.Exit();
    }
}

