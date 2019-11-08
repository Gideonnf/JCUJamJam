using System.Collections; 
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float dampTime = 0.15f;

    private Vector3 velocity = Vector3.zero;

    public Transform target1;
    public Transform target2;

    Vector3 Midpoint;
    Vector3 distance;
    float camDistance;

    public Vector3 CamOffset;
    public float bounds;

    Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = transform.GetComponent<Camera>();
        Midpoint = (target2.position - target1.position) / 2;
        CamOffset = transform.position - Midpoint; 
        camDistance = 10.0f;
        bounds = 12.0f;
    }

    // Update is called once per frame
    void Update()
    {
        distance = target1.position - target2.position;

        if (camDistance >= 19.0f)
            camDistance = 19.0f;
        if (camDistance <= 10.0f)
            camDistance = 10.0f;
        if (distance.x < 0)
            distance.x = distance.x * -1;
        if (distance.z < 0)
            distance.z = distance.z * -1;


        if (distance.x > 15.0f)
        {
            CamOffset.x = distance.x * 0.3f;
            if (CamOffset.x >= 8.5f)
                CamOffset.x = 8.5f;
        }
        else if (distance.x < 14.0f)
        {
            CamOffset.x = distance.x * 0.3f;
        }
     if (distance.z < 14.0f)
        {
            CamOffset.x = distance.z * 0.3f;
        }


        //float MidX = (target2.position.x + target1.position.x) / 2;
        //float MidY = (target2.position.y + target1.position.y) / 2;
        //float MidZ = (target2.position.z + target1.position.z) / 2;



        Midpoint = (target1.position - target2.position) / 2;


        //Vector3 point = camera.WorldToViewportPoint(Midpoint); point.y = transform.position.y;
        // point.x += CamOffset.x;
        //point.z += CamOffset.z;
        //Vector3 dest = point + camera.ViewportToWorldPoint(new Vector3(0.0f, CamOffset.y, 0.0f));
        //transform.position = Vector3.SmoothDamp(transform.position, point, ref velocity, dampTime);
        //Vector3 delta = Midpoint - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, camDistance + CamOffset.x)); //(new Vector3(0.5, 0.5, point.z));
        //Debug.Log("target point :" +  point);
        //Debug.Log("Current Point :" + transform.position);
        Vector3 point = camera.WorldToViewportPoint(Midpoint);
        Vector3 delta = Midpoint - camera.ViewportToWorldPoint(new Vector3(0.7f, 0.5f, camDistance + CamOffset.x)); //(new Vector3(0.5, 0.5, point.z));
        Vector3 destination = transform.position + delta;
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
    }
}
