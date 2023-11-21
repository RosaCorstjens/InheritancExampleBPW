using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    #region VARS
    [Header("Scene References")]
    [SerializeField] internal GameObject mainMenuObject;
    [SerializeField] internal GameObject pauseObject;
    [SerializeField] internal TextMeshProUGUI timeInPlayText;
    [SerializeField] internal Transform floor;
    [SerializeField] internal Dog dog;

    [Header("Prefabs")]
    [SerializeField] internal GameObject bloodParticle;

    // state machine
    internal GameFSM fsm;

    // singleton
    // available with GameManager.Instance
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            // lazy initialization
            if (instance == null)
                instance = FindObjectOfType<GameManager>();

            return instance;
        }
    }
    #endregion

    public void Awake()
    {
        fsm = new GameFSM();
        fsm.AddState(typeof(MainMenuState), new MainMenuState(fsm));
        fsm.AddState(typeof(PlayState), new PlayState(fsm));
        fsm.AddState(typeof(PauseState), new PauseState(fsm));
    }

    public void Start()
    {
        // start in main menu state
        fsm.ChangeState(typeof(MainMenuState));
    }

    private void Update()
    {
        // update the FSM
        fsm.Update();
    }
}
