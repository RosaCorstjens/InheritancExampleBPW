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
    [SerializeField] internal List<GameObject> pathNodes;       // all flowers, used for wandering

    [Header("Prefabs")]
    [SerializeField] internal GameObject bloodParticle;

    // state machine
    internal GameFSM fsm;

    // references to animals, they (un)subscribe themselves
    internal List<Animal> animals;

    // references to spawners, they (un)subscribe themselves
    internal List<AbstractSpawner> spawners;

    // singleton
    // accesible with GameManager.Instance
    private static GameManager instance;
    internal static GameManager Instance
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

    private void Awake()
    {
        animals = new List<Animal>();
        spawners = new List<AbstractSpawner>();

        fsm = new GameFSM();
        fsm.AddState(typeof(MainMenuState), new MainMenuState(fsm));
        fsm.AddState(typeof(PlayState), new PlayState(fsm));
        fsm.AddState(typeof(PauseState), new PauseState(fsm));
    }

    private void Start()
    {
        // start in main menu state
        fsm.ChangeState(typeof(MainMenuState));
    }

    private void Update()
    {
        // update the FSM
        fsm.Update();
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
        List<Animal> chickens = animals.FindAll(a => a.GetType() == typeof(Chicken));

        // no chicks, srry!
        if (chickens.Count <= 0)
            return null;

        int randomIndex = Random.Range(0, chickens.Count);
        return (Chicken)chickens[randomIndex];
    }

    public void StartLevel()
    {
        // start all spawners
        spawners.ForEach(s => s.StartLevel());
    }

    public void EndLevel()
    {
        // end all spawners
        spawners.ForEach(s => s.EndLevel());

        // clean up animals
        while (animals.Count > 0)
        {
            Destroy(animals[0].gameObject);
            animals.RemoveAt(0);
        }
    }
}
