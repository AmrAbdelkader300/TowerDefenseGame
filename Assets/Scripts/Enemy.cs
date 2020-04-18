using System.Xml.Serialization;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;

    private Transform target;
    private int waypointIndex =0;

    private void Start()
    {
        target = WayPoints.getWayPoint(waypointIndex);
    }

    private void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.05f)
        {
            GetNextWaypoint();
        }
    }

    private void GetNextWaypoint()
    {
        if (waypointIndex < WayPoints.getCount()-1)
        {
            ++waypointIndex;
            target = WayPoints.getWayPoint(waypointIndex);
            
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
