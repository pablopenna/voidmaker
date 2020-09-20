using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public struct ScreenDimensions {
        public Vector2 topRight;
        public Vector2 topLeft;
        public Vector2 bottomRight;
        public Vector2 bottomLeft;
    }
    public ScreenDimensions gameDimensions;

    void Start()
    {
        if(GameManager.instance == null) {
            GameManager.instance = this;
        } else {
            Destroy(gameObject);
        }
        
        gameDimensions = CalculateGameDimension(Camera.main);
    }

    public void GameOver() {
        print("GAME OVER!");
    }
    
    private ScreenDimensions CalculateGameDimension(Camera cam) {
        ScreenDimensions dimensions = new ScreenDimensions();
        dimensions.topRight = cam.ViewportToWorldPoint(new Vector3(1,1));
        dimensions.topLeft = cam.ViewportToWorldPoint(new Vector3(0,1));
        dimensions.bottomRight = cam.ViewportToWorldPoint(new Vector3(1,0));
        dimensions.bottomLeft = cam.ViewportToWorldPoint(new Vector3(0,0));
        return dimensions;
    }
    
    public bool IsPointInsideScreen(Vector2 position) {
        return position.x > gameDimensions.bottomLeft.x &&
            position.x < gameDimensions.bottomRight.x &&
            position.y < gameDimensions.topLeft.y &&
            position.y > gameDimensions.bottomLeft.y;
    }

    /**
    * It checks if the bound is COMPLETElY out of the screen (or partially in the screen)
    * That is why checks are like
    *   bounds.min.y > gameDimensions.topLeft.y
    * instead of
    *   bounds.max.y > gameDimensions.topLeft.y
    */
    public bool AreBoundsInsideScreen(Bounds bounds, Vector2 movement) {
        return bounds.min.y + movement.y < gameDimensions.topLeft.y &&
            bounds.max.y + movement.y > gameDimensions.bottomLeft.y &&
            bounds.min.x + movement.x < gameDimensions.topRight.x &&
            bounds.max.x + movement.x > gameDimensions.bottomLeft.x;
    }
    
    public bool AreBoundsInsideScreen(Bounds bounds) {
        return AreBoundsInsideScreen(bounds, Vector2.zero);
    }

    //DEBUG from here onward

    void OnDrawGizmos() {
        Camera cam = Camera.main;
        Vector2 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0,0));
        drawPointWithGizmos(bottomLeft);
        
        Vector2 bottomRight = cam.ViewportToWorldPoint(new Vector3(1,0));
        drawPointWithGizmos(bottomRight);
        
        Vector2 topLeft = cam.ViewportToWorldPoint(new Vector3(0,1));
        drawPointWithGizmos(topLeft);
        
        Vector2 topRight = cam.ViewportToWorldPoint(new Vector3(1,1));
        drawPointWithGizmos(topRight);
    
    }
    
    void drawPointWithGizmos(Vector2 point) { 
        float size = 1f;
        Gizmos.color = Color.red;
        //vertical line
        Gizmos.DrawLine(point + Vector2.down * size, point + Vector2.up * size);
        //horizontal line
        Gizmos.DrawLine(point + Vector2.left * size, point + Vector2.right * size);
    }
}
