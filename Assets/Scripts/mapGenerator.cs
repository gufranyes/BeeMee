using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapGenerator : MonoBehaviour
{
    [Header("Map")]
    public GameObject ceiling;
    public GameObject floor;
    public GameObject prevCeiling;
    public GameObject prevFloor;

    [Header("Actor")]
    public playerController player;

    [Header("Obstacle")]
    private GameObject obstacle1;
    private GameObject obstacle2;
    private GameObject obstacle3;
    private GameObject obstacle4;
    public GameObject obstaclePrefab;

    [Header("Obstacle Properties")]
    public float minObstacleY;
    public float maxObstacleY;
    public float minObstacleSpacing;
    public float maxObstacleSpacing;
    public float minScalingY;
    public float maxScalingY;

    [Header("Collectible")] 
    private GameObject collectible;
    public GameObject collectiblePrefab;

    // Start is called before the first frame update
    void Start()
    {
        obstacle1 = GenerateObstacle(player.transform.position.x + 10f);
        obstacle2 = GenerateObstacle(obstacle1.transform.position.x);
        obstacle3 = GenerateObstacle(obstacle2.transform.position.x);
        obstacle4 = GenerateObstacle(obstacle3.transform.position.x);

        collectible = GenerateCollectible(300);
    }

    GameObject GenerateObstacle(float referenceX)
    {
        GameObject obstacle = GameObject.Instantiate(obstaclePrefab);
        SetTransform(obstacle, referenceX, true);
        return obstacle;
    }

    GameObject GenerateCollectible(float referenceX)
    {
        GameObject collectible = GameObject.Instantiate(collectiblePrefab);
        SetTransform(collectible, referenceX, false);
        return collectible;
    }

    void SetTransform(GameObject obstacle, float referenceX, bool check)
    {
        obstacle.transform.position = new Vector3(referenceX + Random.Range(minObstacleSpacing, maxObstacleSpacing), Random.Range(minObstacleY, maxObstacleY), 0);
        if (check)
        {
            obstacle.transform.localScale = new Vector3(obstacle.transform.localScale.x, Random.Range(minScalingY, maxScalingY), obstacle.transform.localScale.z);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x > floor.transform.position.x)
        {
            var tempCeiling = prevCeiling;
            var tempFloor = prevFloor;
            prevCeiling = ceiling;
            prevFloor = floor;
            tempCeiling.transform.position += new Vector3(80, 0, 0);
            tempFloor.transform.position += new Vector3(80, 0, 0);
            ceiling = tempCeiling;
            floor = tempFloor;
        }

        if (player.transform.position.x > obstacle2.transform.position.x)
        {
            var tempObstacle = obstacle1;
            obstacle1 = obstacle2;
            obstacle2 = obstacle3;
            obstacle3 = obstacle4;

            SetTransform(tempObstacle, obstacle3.transform.position.x, true);
            obstacle4 = tempObstacle;
        }

        if (player.lives < 3)
        {
            if (player.transform.position.x > collectible.transform.position.x)
            {
                var tempCollectible = collectible;
                SetTransform(collectible, obstacle4.transform.position.x, false);
                
            }
        }
    }
}
