using agent;
using board;
using JetBrains.Annotations;
using ui;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public delegate void GameFinished();

    [SerializeField] private int MAX_AGENTS = 4;
    [SerializeField] public int MAX_STEPS_AGENT = 300;
    [SerializeField] public int ROWS = 10;
    [SerializeField] public int COLUMNS = 10;
    [SerializeField] public int INITIAL_DONUT_WIDTH = 3;
    [SerializeField] public int INITIAL_DONUT_HEIGHT = 3;
    [SerializeField] public float WALK_THRESHOLD = 0.5f;
    [SerializeField] public GameObject wallSquare;
    [SerializeField] public GameObject foodSquare;
    [SerializeField] public GameObject bigFood;
    [SerializeField] public GameObject breakableWallSquare;
    [SerializeField] public Transform player;
    [SerializeField] public GameObject menuBackground;

    [CanBeNull] private GameDrawer _game;
    private Prefabs _prefabs;


    private AudioSource audioSource;
    private int bigFoodEaten;


    [SerializeField] public Plane corridorSquare;
    private int foodEaten;

    private void Start()
    {
        wallSquare.SetActive(false);
        foodSquare.SetActive(false);
        bigFood.SetActive(false);
        breakableWallSquare.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        _prefabs = new Prefabs(corridorSquare, wallSquare, foodSquare, bigFood, player, breakableWallSquare,
            InstantiateObject,
            DestroyObject);
        GenerateGame();
        Fruit.OnFruitEaten += () =>
        {
            foodEaten += 1;
            Debug.LogWarning("Counter: " + foodEaten + " game: " + _game.GetFood());
        };
        BigFruit.OnBigFruitEaten += () =>
        {
            bigFoodEaten += 1;
            Debug.LogWarning("Counter: " + bigFoodEaten + " game: " + _game.GetBigFood());
        };
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) GenerateGame();
        if (Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene("MenuScene");
        if (Input.GetKeyDown(KeyCode.P)) PauseGame();
        if (_game != null && bigFoodEaten == _game.GetBigFood() && foodEaten == _game.GetFood()) GenerateGame();
    }

    public static event GameFinished onGameFinished;

    private void GenerateGame()
    {
        _game?.Destroy();
        var maze = GetRandomMaze();
        _game = new GameDrawer(_prefabs, maze);
        _game.StartNewGame(gameObject);
        foodEaten = 0;
        bigFoodEaten = 0;
        onGameFinished?.Invoke();
    }

    private Maze GetRandomMaze()
    {
        var maze = new Maze(ROWS - 2, COLUMNS - 2);
        var initialDonut = new Donut(INITIAL_DONUT_WIDTH, INITIAL_DONUT_HEIGHT);
        var initialPositions = Cycle.Apply(initialDonut, maze);
        var agents = new Agents(MAX_AGENTS, MAX_STEPS_AGENT, WALK_THRESHOLD);
        agents.Walk(maze, initialPositions);
        return maze.Resize(ROWS, COLUMNS);
    }


    private static GameObject InstantiateObject(GameObject prefab)
    {
        return Instantiate(prefab);
    }


    private static void DestroyObject(GameObject obj)
    {
        Destroy(obj);
    }

    private void PauseGame()
    {
        menuBackground.SetActive(true);
        audioSource.Pause();
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        audioSource.Play();
    }
}