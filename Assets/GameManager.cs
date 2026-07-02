using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Interval {
    public float Min, Max;
}

public class GameManager : MonoBehaviour {
    private int score;
    public GameObject Obstacle;
    public List<GameObject> Obstacles;

    [SerializeField] Interval secRangeBetweenTargets;
    [SerializeField] Interval HightRange;
    private float timeLeftObstacleSpawn;

    public float obstacleSpawnBuffer;
    public float obstDespawnTimer = 5.5f;

    public Interval Speed;
    public GameObject player;

    public enum GameState {
        MENU,
        STARTING,
        PLAYING,
        PLAYER_DEATH,
    }

    private GameState gState = GameState.MENU;


    void Start() {
        Obstacles = new();
        player.SetActive(false);
        timeLeftObstacleSpawn = UnityEngine.Random.Range(secRangeBetweenTargets.Min, secRangeBetweenTargets.Max);
    }

    void Update() {
        switch (gState) {
            case GameState.MENU:
                if (Input.GetKeyDown(KeyCode.Space)) {
                    gState = GameState.STARTING;
                }
                break;
            case GameState.STARTING:
                Debug.Log("Game starting!");
                player.SetActive(true);
                gState = GameState.PLAYING;
                break;
            case GameState.PLAYING:
                UpdateObst();
                break;
            case GameState.PLAYER_DEATH:
                Debug.Log("Player Death");
                break;

        }
    }

    void UpdateObst() {
        timeLeftObstacleSpawn -= Time.deltaTime;
        if (timeLeftObstacleSpawn < 0) {
            SpawnObstacle();
            timeLeftObstacleSpawn = UnityEngine.Random.Range(secRangeBetweenTargets.Min, secRangeBetweenTargets.Max);
            Debug.Log("Spawning new object. Time left until next spawn: " + timeLeftObstacleSpawn);
        }
        MoveObstacles();
    }

    void SpawnObstacle() {
        Camera cam = Camera.main;

        float verticalExtent = cam.orthographicSize;
        float horizontalExtent = cam.aspect * verticalExtent;

        float leftEdge = cam.transform.position.x - horizontalExtent;
        float rightEdge = cam.transform.position.x + horizontalExtent;
        float topEdge = cam.transform.position.x + verticalExtent;
        float bottomEdge = cam.transform.position.x - verticalExtent;

        GameObject newObst = Instantiate(Obstacle, new Vector3(leftEdge - obstacleSpawnBuffer, UnityEngine.Random.Range(HightRange.Min, HightRange.Max), 0), Quaternion.identity);
        Obstacles.Add(newObst);
        newObst.GetComponent<ObstacleController>().Speed = (int)UnityEngine.Random.Range(Speed.Min, Speed.Max);
        StartCoroutine(DeleteObstacle(newObst));
    }

    void MoveObstacles() {
        foreach (GameObject obst in Obstacles) {
            if (obst == null) continue;
            obst.GetComponent<ObstacleController>().MoveObst();
        }
    }

    IEnumerator DeleteObstacle(GameObject obst) {
        yield return new WaitForSeconds(obstDespawnTimer);
        Obstacles.Remove(obst);
        Destroy(obst);
    }

    public void AddScore(int amount) {
        score += amount;
    }

    public void EndGame() {
        gState = GameState.PLAYER_DEATH;

        foreach (GameObject obst in Obstacles) {
            obst.SetActive(false);
        }

        player.SetActive(false);
    }
}
