using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public LayerMask damageLayer;
    int maxHits = 1;
    Vector2 direction = Vector2.up;
    public float projectileSpeed = 10f;
    [SerializeField]
    float hitboxRadius = 0.5f;
    public GameObject explosionPrefab;
    ProjectileShooter launcher; //launcher which created this projectile

    void SetDirection(Vector2 direction) {
        this.direction = direction;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * projectileSpeed * Time.deltaTime);
        Collider2D hit = Physics2D.OverlapCircle(transform.position, hitboxRadius, damageLayer);
        if (hit) {
            Detonate(hit);
        }

        RemoveIfOutOfScreen();
    }

    void Detonate(Collider2D hit) {
        Destroy(hit.gameObject);
        launcher.RemoveProjectile(gameObject);
        Destroy(gameObject);
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    }

    public void SetLauncher(ProjectileShooter launcher) {
        this.launcher = launcher;
    }

    void RemoveIfOutOfScreen() { 
        if(transform.position.y > 5) {
            launcher.RemoveProjectile(gameObject);
            Destroy(gameObject);
        }
    }
}
