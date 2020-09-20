using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, BaseEntity
{
    public float moveSpeed = 4f;
    public float moveProgression = 0.8f; //Used for Lerp
    ProjectileShooter shooter;
    BoxCollider2D collider;

    void BaseEntity.Destroy()
    {
        GameManager.instance.GameOver();
    }

    // Start is called before the first frame update
    void Start()
    {
        shooter = GetComponent<ProjectileShooter>();
        collider = GetComponent<BoxCollider2D>();
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
        Bounds futureBounds = collider.bounds;
        bool willBeInsideScreen = GameManager.instance.AreBoundsInsideScreen(collider.bounds, movement);
        if (willBeInsideScreen) { 
            transform.Translate(movement);
        }
    }
}
