using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState
{
    protected GameFSM fsm;

    public GameState(GameFSM fsm) { this.fsm = fsm; }

    public virtual void Enter() { }
    public virtual void Execute() { }
    public virtual void Exit() { }
}

public class MainMenuState : GameState
{
    public MainMenuState(GameFSM fsm) : base(fsm) { }

    public override void Enter()
    {
        GameManager.Instance.mainMenuObject.SetActive(true);
        Cursor.visible = false;
        Time.timeScale = 0;
    }

    public override void Execute()
    {
        if (Input.anyKeyDown)
            fsm.ChangeState(typeof(PlayState));
    }

    public override void Exit()
    {
        GameManager.Instance.mainMenuObject.SetActive(false);
        Time.timeScale = 1;
    }
}

public class PlayState : GameState
{
    private float timeInPlay = 0f;

    public PlayState(GameFSM fsm) : base(fsm) { }

    public override void Enter()
    {
        timeInPlay = 0f;

        Cursor.visible = true;

        GameManager.Instance.timeInPlayText.gameObject.SetActive(true);
    }

    public override void Execute()
    {
        // update time
        timeInPlay += Time.deltaTime;

        // update texts
        GameManager.Instance.timeInPlayText.text = "Seconds in play: " + timeInPlay;

        // goto pause on espace pressed
        if (Input.GetKeyDown(KeyCode.Escape))
            fsm.ChangeState(typeof(PauseState));
    }

    public override void Exit()
    {
        GameManager.Instance.timeInPlayText.gameObject.SetActive(false);
    }
}

public class PauseState : GameState
{
    public PauseState(GameFSM fsm) : base(fsm) { }

    public override void Enter()
    {
        GameManager.Instance.pauseObject.SetActive(true);
        Time.timeScale = 0;
        Cursor.visible = true;
    }

    public override void Execute()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            fsm.ChangeState(typeof(PlayState));
    }

    public override void Exit()
    {
        GameManager.Instance.pauseObject.SetActive(false);
        Time.timeScale = 1;
    }
}