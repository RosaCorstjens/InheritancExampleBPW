using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameStateType { MainMenu, Play, Pause }

public abstract class GameState
{
    public abstract void Enter();
    public abstract void Exit();
    public abstract void Update();
}

public class MainMenuState : GameState
{
    public override void Enter()
    {
        GameManager.Instance.mainMenuObject.SetActive(true);
        Cursor.visible = false;
        Time.timeScale = 0;
    }

    public override void Exit()
    {
        GameManager.Instance.mainMenuObject.SetActive(false);
        Time.timeScale = 1;
    }

    public override void Update()
    {
        if (Input.anyKeyDown)
        {
            GameManager.Instance.fsm.GotoState(GameStateType.Play);
            GameManager.Instance.StartLevel();
        }
    }
}

public class PlayState : GameState
{
    internal float totalTimeInPlay = 0f;

    public override void Enter()
    {
        GameManager.Instance.animalCounterText.gameObject.SetActive(true);
        GameManager.Instance.timeInPlayText.gameObject.SetActive(true);
        Cursor.visible = true;

        GameManager.Instance.pathNodeSpawner.currentAmount = GameManager.Instance.pathNodes.Count;
    }

    public override void Exit()
    {
        GameManager.Instance.animalCounterText.gameObject.SetActive(false);
        GameManager.Instance.timeInPlayText.gameObject.SetActive(false);
    }

    public override void Update()
    {
        totalTimeInPlay += Time.deltaTime;

        // update texts
        GameManager.Instance.animalCounterText.text = "Current animal count: " + GameManager.Instance.animals.Count;
        GameManager.Instance.timeInPlayText.text = "Seconds in play: " + totalTimeInPlay;

        // update animals and controller
        GameManager.Instance.animals.ForEach(a => a.DoUpdate());
        GameManager.Instance.controller.DoUpdate();

        // update path node amount for spawner
        GameManager.Instance.pathNodeSpawner.currentAmount = GameManager.Instance.pathNodes.Count;

        if (Input.GetKeyDown(KeyCode.Escape))
            GameManager.Instance.fsm.GotoState(GameStateType.Pause);
    }
}

public class PauseState : GameState
{
    public override void Enter()
    {
        GameManager.Instance.pauseObject.SetActive(true);
        Time.timeScale = 0;
        Cursor.visible = true;
    }

    public override void Exit()
    {
        GameManager.Instance.pauseObject.SetActive(false);
        Time.timeScale = 1;
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            GameManager.Instance.fsm.GotoState(GameStateType.Play);
    }
}