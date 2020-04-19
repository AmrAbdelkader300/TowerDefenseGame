using UnityEngine;
using UnityEditor;
using System.Collections;

public class Turret : MonoBehaviour
{
    [Header("Attributes")]
    public float range = 2f;
    public float fireRate = 1f;
    public float turnSpeed = 10;

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";
    public Transform part2Rotate;
    public GameObject bulletPrefab;
    public Transform firePoint;

    private Transform target = null;
    private float fireCountdown = 0f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget ()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach(GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        } else
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) { return; }
       //float yDir = target.position.y - transform.position.y;
        //Vector3 dir = new Vector3(0f, yDir);
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(part2Rotate.rotation, lookRotation, Time.deltaTime*turnSpeed).eulerAngles;
        part2Rotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    private void Shoot()
    {
        GameObject bulletGo = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGo.GetComponent<Bullet>();
        if (bullet != null) { bullet.Seek(target); }
    }

    private void OnDrawGizmosSelected()
    {
        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawWireDisc(transform.position, transform.up, range);
    }
}
