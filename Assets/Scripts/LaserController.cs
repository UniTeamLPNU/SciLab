using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    private LineRenderer lr;

    void Start() //REPLACE START WITH PUBLIC VARIABLE???
    {
        lr = GetComponent<LineRenderer>();
    }
    
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            lr.SetPosition(1, transform.InverseTransformPoint(hit.point));
            lr.SetPosition(2, new Vector3(0, 0, 0));
            if (hit.collider.gameObject.name == "Mirror")
            {
                lr.SetPosition(2, transform.InverseTransformPoint(Vector3.Reflect(hit.point-this.transform.position, hit.normal) * 5000 + hit.point));
            }
            else if (hit.collider.gameObject.name == "Prism")
            {
                lr.SetPosition(2, (Vector3.Reflect(hit.point - this.transform.position, hit.normal) * 5000 + hit.point));
            }
        }
        else
        {
            lr.SetPosition(1, new Vector3(0, 0, 5000));
            lr.SetPosition(2, new Vector3(0, 0, 0));
        }
    }
}
