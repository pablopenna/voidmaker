using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, BaseEntity
{
    public float moveSpeed = 4f;
    public float moveProgression = 0.8f; //Used for Lerp
    ProjectileShooter shooter;
    BoxCollider2D boxCollider;

    void BaseEntity.Destroy()
    {
        GameManager.instance.GameOver();
    }

    // Start is called before the first frame update
    void Start()
    {
        shooter = GetComponent<ProjectileShooter>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();

        if (Input.GetKeyDown(KeyCode.Space)){
            shooter.Shoot(Vector2.up);
        }
    }
    
    private void MovePlayer() {
        float xInput = Input.GetAxisRaw("Horizontal");
        float xMovement = xInput * this.moveSpeed * Time.deltaTime;
        float yInput = Input.GetAxisRaw("Vertical");
        float yMovement = yInput * this.moveSpeed * Time.deltaTime;
        Vector2 movement = new Vector2(xMovement, yMovement);
        Vector2 clampedMovement = ClampMovementWithScreenBorders(movement, boxCollider.bounds);
        transform.Translate(clampedMovement);
        
    }
    
    private Vector2 ClampMovementWithScreenBorders(Vector2 movement, Bounds bounds) { 
        GameManager.DistanceToScreenBorder distances = GameManager.instance.GetBoundsDistanceToBorder(bounds);
        Vector2 clampedMovement = new Vector2(movement.x, movement.y);
        //Clamp left
        if(movement.x<0 && distances.distanceLeft < Mathf.Abs(movement.x)) {
            clampedMovement.x = -distances.distanceLeft;
        }
        //Clamp right
        if(movement.x>0 && distances.distanceRight < movement.x) {
            clampedMovement.x = distances.distanceRight;
        }
        //Clamp top
        if(movement.y>0 && distances.distanceTop < movement.y) {
            clampedMovement.y = distances.distanceTop;
        }
        //Clamp bottom
        if(movement.y<0 && distances.distanceBottom < Mathf.Abs(movement.y)) {
            clampedMovement.y = -distances.distanceBottom;
        }
        return clampedMovement;
    }
}
