using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour, BaseEntity
{

    GameObject target;
    ProjectileShooter shooter;
    public float horizontalDistanceToPlayerForShooting = 10f;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player");
        shooter = GetComponent<ProjectileShooter>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalDistanceToPlayer = Mathf.Abs(transform.position.x - target.transform.position.x);
        if(horizontalDistanceToPlayer < horizontalDistanceToPlayerForShooting) {
            shooter.Shoot(Vector2.down);
        }
    }

    void BaseEntity.Destroy() {
        //Destroy(gameObject);
        print("Dead");
    }
}
