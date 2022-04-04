using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{

    public List<Transform> waypoints;

    public float minDis = .1f;
    public int indice = 0;
    public Transform atual;
    public float speed = 100f;
    public float turnSpeed = 2f;
    public float size = 10f;

    // Start is called before the first frame update
    void Start()
    {
        if (waypoints.Count > 0)
        {
            atual = waypoints[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (atual != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, atual.position, speed);
            if (Vector3.Distance(transform.position, atual.position) < minDis)
            {
                indice++;
                if (indice >= waypoints.Count)
                {
                    indice = 0;
                }
                atual = waypoints[indice];
            }
            Vector3 direction = (atual.position - transform.position).normalized;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * turnSpeed);
           // transform.LookAt(atual);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        foreach(Transform v in waypoints)
        {
            Gizmos.DrawSphere(v.position, size);
        }
    }
}
