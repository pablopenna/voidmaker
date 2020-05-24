using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    [SerializeField]
    GameObject projectilePrefab;
    [SerializeField]
    GameObject shootingPoint;
    List<GameObject> launchedProjectiles;
    public int maxLaunchedProjectiles = 2;

    // Start is called before the first frame update
    void Start()
    {
        launchedProjectiles = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && launchedProjectiles.Count < maxLaunchedProjectiles) {
            GameObject projectile = Instantiate(projectilePrefab, shootingPoint.transform.position, Quaternion.identity);
            Projectile projectileScript = projectile.GetComponent<Projectile>();
            projectileScript.SetLauncher(this);
            launchedProjectiles.Add(projectile);
        }
    }

    public void RemoveProjectile(GameObject projectile) {
        this.launchedProjectiles.Remove(projectile);
    }
}
