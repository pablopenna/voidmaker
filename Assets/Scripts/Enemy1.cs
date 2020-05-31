using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour, BaseEntity
{
    Vector2 spawnPoint;
    float distanceToSpawn;
    GameObject target;
    float distanceToTarget;
    ProjectileShooter shooter;
    public float horizontalDistanceToPlayerForShooting = 10f;
    public float moveSpeed = 2f;

    public enum EnemyState { IDLE, ATTACKING, RETURNING };
    EnemyState state;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player");
        shooter = GetComponent<ProjectileShooter>();
        spawnPoint = transform.position;
        state = EnemyState.IDLE;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDistanteToTarget();
        UpdateDistanteToSpawn();

        //CalculateState();

        if(state == EnemyState.ATTACKING) {
            MoveTowardsPlayer();
        } else if(state == EnemyState.RETURNING) {
            MoveToSpawn();
        }

    }

    void CalculateState()
    {
        if (state == EnemyState.IDLE){
            state = EnemyState.ATTACKING;
        } else if (distanceToTarget < 1f && state == EnemyState.ATTACKING) {
            state = EnemyState.RETURNING;
        } else if (distanceToSpawn < 1f && state == EnemyState.RETURNING) {
            state = EnemyState.IDLE;
        }
    }

    void UpdateDistanteToTarget() {
        Vector2 direction = target.transform.position - transform.position;
        distanceToTarget = direction.magnitude;
    }

    void UpdateDistanteToSpawn() {
        Vector2 direction = spawnPoint - (Vector2)transform.position;
        distanceToSpawn = direction.magnitude;
    }


    void MoveTowardsPlayer() {
        MoveTowardsPoint(target.transform.position, true);
    }

    void MoveToSpawn() {
        MoveTowardsPoint(spawnPoint, false);
    }

    void MoveTowardsPoint(Vector2 targetPoint, bool addSideMovement = false) {
        Vector2 direction = targetPoint - (Vector2)transform.position;
        direction.Normalize();
        if (addSideMovement) {
            float sideMovement = Mathf.Sin(Time.time);
            direction = new Vector2(direction.x + sideMovement, direction.y + sideMovement);
            direction.Normalize();
        }
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    void CheckShoot() {
        float horizontalDistanceToPlayer = Mathf.Abs(transform.position.x - target.transform.position.x);
        if (horizontalDistanceToPlayer < horizontalDistanceToPlayerForShooting)
        {
            shooter.Shoot(Vector2.down);
        }
    }

    void BaseEntity.Destroy() {
        //Destroy(gameObject);
        print("Dead");
    }
}
