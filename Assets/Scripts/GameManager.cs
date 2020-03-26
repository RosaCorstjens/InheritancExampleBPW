using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    // can only be accessed in this class
    private static GameManager instance;

    // is available from all throughout the code with GameManager.Instance
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<GameManager>();

            return instance;
        }
    }

    // scene references
    [Header("UI objects")]
    [SerializeField] internal GameObject mainMenuObject;
    [SerializeField] internal GameObject pauseObject;
    [SerializeField] internal TextMeshProUGUI animalCounterText;
    [SerializeField] internal TextMeshProUGUI timeInPlayText;

    [Header("Other scene references")]
    [SerializeField] internal CameraController controller;
    [SerializeField] internal SpawnerOverTimeGroup pathNodeSpawner;
    [SerializeField] internal Transform floor;

    [Header("Prefabs")]
    // ref to blood partical, used by animals
    [SerializeField] internal GameObject bloodParticle;

    // references to animals
    internal List<Animal> animals;
    internal List<Chicken> chickens;

    // references to spawners
    internal List<AbstractSpawner> spawners;

    // references to all flowers acting as nodes for paths
    internal List<GameObject> pathNodes;

    // state machine
    internal GameFSM fsm;

    public void Awake()
    {
        animals = new List<Animal>();
        chickens = new List<Chicken>();
        pathNodes = new List<GameObject>();
        spawners = new List<AbstractSpawner>();

        fsm = new GameFSM();
        fsm.Initialize();
    }

    public void Start()
    {
        GotoMainMenu();
    }

    private void Update()
    {
        fsm.UpdateState();
    }

    public Vector3 GetRandomDestination()
    {
        if (pathNodes.Count <= 0)
            return Vector3.zero;

        int randomIndex = Random.Range(0, pathNodes.Count);
        return pathNodes[randomIndex].transform.position;
    }

    public Chicken GetRandomChicken()
    {
        if (chickens.Count <= 0)
            return null;

        int randomIndex = Random.Range(0, chickens.Count);
        return chickens[randomIndex];
    }

    public void StartLevel()
    {
        ((PlayState)fsm.GetState(GameStateType.Play)).totalTimeInPlay = 0;

        spawners.ForEach(s => s.StartLevel());
    }

    public void EndLevel()
    {
        spawners.ForEach(s => s.EndLevel());

        while(animals.Count > 0)
            animals[0].Destroy();
        while (pathNodes.Count > 0)
        {
            Destroy(pathNodes[0]);
            pathNodes.RemoveAt(0);
        }
    }

    #region GOTO_STATE
    public void GotoMainMenu()
    {
        fsm.GotoState(GameStateType.MainMenu);
        EndLevel();
    }

    public void GotoPlay()
    {
        fsm.GotoState(GameStateType.Play);
    }

    public void GotoPause()
    {
        fsm.GotoState(GameStateType.Pause);
    }
    #endregion
}
