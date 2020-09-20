using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject shootingPoint;
    List<GameObject> launchedProjectiles;
    public int maxLaunchedProjectiles = 2;
    public LayerMask damageLayer;

    // Start is called before the first frame update
    void Start()
    {
        launchedProjectiles = new List<GameObject>();
    }

    public void Shoot(Vector2 direction){
        if (launchedProjectiles.Count < maxLaunchedProjectiles){
            CreateProjectile(direction);
        }
    }

    void CreateProjectile(Vector2 dir) {
        GameObject projectile = Instantiate(projectilePrefab, shootingPoint.transform.position, Quaternion.identity);
        //projectile.transform.parent = transform; //Set "shooter" as parent of the projectile
        projectile.transform.parent = GameManager.instance.transform; //Set "shooter" as parent of the projectile
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        projectileScript.SetLauncher(this);
        projectileScript.damageLayer = damageLayer;
        projectileScript.direction = dir;
        launchedProjectiles.Add(projectile);
        projectile.SetActive(true);
    }

    public void RemoveProjectile(GameObject projectile) {
        this.launchedProjectiles.Remove(projectile);
    }
}
