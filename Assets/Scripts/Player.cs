using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, BaseEntity
{
    public float moveSpeed = 4f;
    ProjectileShooter shooter;

    void BaseEntity.Destroy()
    {
        GameManager.instance.GameOver();
    }

    // Start is called before the first frame update
    void Start()
    {
        shooter = GetComponent<ProjectileShooter>();
    }

    // Update is called once per frame
    void Update()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float xMovement = xInput * this.moveSpeed * Time.deltaTime;
        transform.Translate(new Vector2(xMovement, 0f));

        if (Input.GetKeyDown(KeyCode.Space)){
            shooter.Shoot(Vector2.up);
        }
    }
}
