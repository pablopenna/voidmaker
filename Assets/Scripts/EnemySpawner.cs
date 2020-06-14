using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemyAPrefab;
    List<Vector2> spawnPoints;
    public int spawnColumns = 5;
    public int spawnRows = 2;

    // Start is called before the first frame update
    void Start()
    {
        Camera cam = Camera.main;
        float camWidth = cam.aspect * 2f * cam.orthographicSize;
        float camHeight = 2f * cam.orthographicSize;

        float spawnWidth = camWidth;
        float spawnHeight = camHeight / 2;

        Vector3 initPoint = cam.ScreenToWorldPoint(Vector3.zero); //bottom left
        initPoint = new Vector3(initPoint.x, initPoint.y + camHeight, initPoint.z); // top left
        //Instantiate(enemyAPrefab,initPoint,Quaternion.identity);

        transform.position = new Vector2(initPoint.x + spawnWidth / 2, initPoint.y - spawnHeight / 2);

        spawnPoints = new List<Vector2>();
        GenerateSpawnPoints(initPoint, spawnWidth, spawnHeight, this.spawnColumns, this.spawnRows);
        SpawnEnemies();
    }

    //Init point is assumed to be the top-left of the spawn
    void GenerateSpawnPoints(Vector2 initPoint, float spawnWidth, float spawnHeight, int spawnColumns, int spawnRows) {
        float columnWidth = spawnWidth / spawnColumns;
        float columnOffset = columnWidth / 2; //offset to place spawn in the middle of the column
        float rowHeight = spawnHeight / spawnRows;
        float rowOffset = rowHeight / 2; //offset to place spawn in the middle of the row

        for(int rowIdx=0; rowIdx<spawnRows; rowIdx++) {
            for (int colIdx = 0; colIdx<spawnColumns; colIdx++) {
                Vector2 spawnPoint = new Vector2(initPoint.x+(columnWidth*colIdx)+columnOffset, 
                    initPoint.y - (rowHeight*rowIdx) - rowOffset);
                this.spawnPoints.Add(spawnPoint);
            }
        }
    }

    void SpawnEnemies() {
        foreach (Vector2 spawnPoint in this.spawnPoints){
            GameObject enemySpawned = Instantiate(enemyAPrefab, spawnPoint, Quaternion.identity);
            enemySpawned.transform.parent = this.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDrawGizmos()
    {
        if (this.spawnPoints != null)
        {
            Gizmos.color = Color.red;
            float size = .3f;
            for (int i = 0; i < this.spawnPoints.Count; i++)
            {
                Vector2 spawnPoint = this.spawnPoints[i];
                //vertical line
                Gizmos.DrawLine(spawnPoint + Vector2.down * size, spawnPoint + Vector2.up * size);
                //horizontal line
                Gizmos.DrawLine(spawnPoint + Vector2.left * size, spawnPoint + Vector2.right * size);
            }
        }
    }
}
