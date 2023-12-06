using agent;
using board;
using JetBrains.Annotations;
using ui;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private int MAX_AGENTS = 4;
    [SerializeField] public int MAX_STEPS_AGENT = 300;
    [SerializeField] public int ROWS = 10;
    [SerializeField] public int COLUMNS = 10;
    [SerializeField] public int INITIAL_DONUT_WIDTH = 3;
    [SerializeField] public int INITIAL_DONUT_HEIGHT = 3;
    [SerializeField] public float WALK_THRESHOLD = 0.5f;


    [SerializeField] public Plane corridorSquare;
    [SerializeField] public GameObject wallSquare;
    [SerializeField] public GameObject foodSquare;
    [SerializeField] public GameObject bigFood;
    [SerializeField] public Transform player;

    [CanBeNull] private GameDrawer _game;
    private Prefabs _prefabs;

    private void Start()
    {
        wallSquare.SetActive(false);
        foodSquare.SetActive(false);
        bigFood.SetActive(false);
        _prefabs = new Prefabs(corridorSquare, wallSquare, foodSquare, bigFood, player, InstantiateObject,
            DestroyObject);
        GenerateGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) GenerateGame();
    }

    private void GenerateGame()
    {
        _game?.Destroy();
        var maze = GetRandomMaze();
        _game = new GameDrawer(_prefabs, maze);
        _game.StartNewGame(gameObject);
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
}