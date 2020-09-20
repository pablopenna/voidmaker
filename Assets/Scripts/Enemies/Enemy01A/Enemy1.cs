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

    //Idle movement 
    Vector2 trajectory;
    float distanceToParent;
    [SerializeField]
    float maxDistance2Spawn = 0.1f;
    bool isGoingTowardsParent;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player");
        shooter = GetComponent<ProjectileShooter>();
        spawnPoint = transform.position;
        state = EnemyState.IDLE;

        //Idle movement
        trajectory = (Vector2)transform.parent.position - this.spawnPoint;
        isGoingTowardsParent = true;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDistanteToTarget();
        UpdateDistanteToSpawn();
        UpdateDistanteToParent();

        //CalculateState();

        if (state == EnemyState.ATTACKING) {
            MoveTowardsPlayer();
        } else if(state == EnemyState.RETURNING) {
            MoveToSpawn();
        }
        this.CheckShoot();
        IdleMovement();
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
        distanceToTarget = Vector2.Distance(target.transform.position, transform.position);
    }

    void UpdateDistanteToSpawn() {
        distanceToSpawn = Vector2.Distance(spawnPoint, transform.position);
        //print(Mathf.Approximately(distanceToSpawn, 0f));
    }

    void UpdateDistanteToParent() {
        distanceToParent = Vector2.Distance(transform.parent.position, transform.position);
    }


    void MoveTowardsPlayer() {
        MoveTowardsPoint(target.transform.position, true);
    }

    void MoveToSpawn() {
        MoveTowardsPoint(spawnPoint, false);
    }

    void MoveTowardsPoint(Vector2 targetPoint, float lMoveSpeed, bool addSideMovement = false) {
        Vector2 direction = targetPoint - (Vector2)transform.position;
        direction.Normalize();
        if (addSideMovement) {
            float sideMovement = Mathf.Sin(Time.time);
            direction = new Vector2(direction.x + sideMovement, direction.y + sideMovement);
            direction.Normalize();
        }
        transform.Translate(direction * lMoveSpeed * Time.deltaTime);
    }

    void MoveTowardsPoint(Vector2 targetPoint, bool addSideMovement = false) {
        MoveTowardsPoint(targetPoint, this.moveSpeed, addSideMovement);
    }

    void IdleMovement() {
        if (distanceToSpawn < 0.1f) {
            isGoingTowardsParent = true;
        }
        else if (distanceToSpawn >= maxDistance2Spawn || Mathf.Approximately(distanceToParent, 0f)) {
            isGoingTowardsParent = false;
        }

        if (isGoingTowardsParent) {
            MoveTowardsPoint(transform.parent.position, 0.1f);
        } else {
            MoveTowardsPoint(spawnPoint, 0.1f);
        }
    }

    void CheckShoot() {
        float horizontalDistanceToPlayer = Mathf.Abs(transform.position.x - target.transform.position.x);
        if (horizontalDistanceToPlayer < horizontalDistanceToPlayerForShooting)
        {
            shooter.Shoot(Vector2.down);
        }
    }

    void BaseEntity.Destroy() {
        Destroy(gameObject);
    }
}
