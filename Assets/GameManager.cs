using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public struct Interval {
    public int Min, Max;
}

public class GameManager : MonoBehaviour
{
    public GameObject Obstacle;
    public List<GameObject> Obstacles;

    [SerializeField] Interval secRangeBetweenTargets;
    private float timeLeftObstacleSpawn;

    public float obstacleSpawnBuffer;
    public float obstDespawnTimer = 5.5f;

    public Interval Speed;

    void Start() {
        Obstacles = new();
        timeLeftObstacleSpawn = UnityEngine.Random.Range(secRangeBetweenTargets.Min, secRangeBetweenTargets.Max);
    }

    void Update() {
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

        GameObject newObst = Instantiate(Obstacle, new Vector3(leftEdge - obstacleSpawnBuffer, 0, 0), Quaternion.identity);
        Obstacles.Add(newObst);
        newObst.GetComponent<ObstacleController>().Speed = UnityEngine.Random.Range(Speed.Min, Speed.Max);
        StartCoroutine(DeleteObstacle(newObst));
    }

    void MoveObstacles() {
        foreach (GameObject obst in Obstacles) {
            if (obst == null) continue;
            obst.GetComponent<ObstacleController>().MoveObst();
        }
    }

    IEnumerator DeleteObstacle(GameObject obstsavlefsef) {
        yield return new WaitForSeconds(obstDespawnTimer);
        Obstacles.Remove(obstsavlefsef);
        Destroy(obstsavlefsef);
    }
}
