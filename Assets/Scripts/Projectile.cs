using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public LayerMask damageLayer;
    public Vector2 direction = Vector2.up;
    public float projectileSpeed = 10f;
    public float hitboxRadius = 0.5f;
    public GameObject explosionPrefab;
    ProjectileShooter launcher; //launcher which created this projectile

    void SetDirection(Vector2 dir) {
        this.direction = dir;
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
        //Destroy(hit.gameObject);
        HitHandler hitHandler = hit.GetComponent<HitHandler>();
        if (hitHandler) {
            hitHandler.Hit();
        }

        launcher.RemoveProjectile(gameObject);
        Destroy(gameObject);
        Instantiate(explosionPrefab, hit.transform.position, Quaternion.identity);
    }

    public void SetLauncher(ProjectileShooter launcher) {
        this.launcher = launcher;
    }

    void RemoveIfOutOfScreen() { 
        if(Mathf.Abs(transform.position.y) > 5) {
            launcher.RemoveProjectile(gameObject);
            Destroy(gameObject);
        }
    }

    void OnDrawGizmos() {
        //DEBUG
        //Gizmos.DrawSphere(transform.position, hitboxRadius);
    }
}
