﻿using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Mouse Orbit with zoom")]
public class MouseOrbitImproved : MonoBehaviour
{

    public Transform target;
    public float distance = 5.0f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;

    public float yMinLimit = 3.0f;
    public float yMaxLimit = 3.0f;

    public float xMinLimit = 15f;
    public float xMaxLimit = 15f;

    public float distanceMin = .5f;
    public float distanceMax = 15f;

    private Rigidbody rigidbody;

    float x = 0.0f;
    float y = 3.0f;

    // Use this for initialization
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        Debug.Log("camera angles: " + angles);
        x = angles.y;
        y = angles.x;

        rigidbody = GetComponent<Rigidbody>();

        // Make the rigid body not change rotation
        if (rigidbody != null)
        {
            rigidbody.freezeRotation = true;
        }

        if (target) {
          positionCamera();
        }
    }

    void LateUpdate()
    {
        if (target && Input.GetMouseButton(0))
        {
          positionCamera();
        }
    }

    void positionCamera(){
      x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
//            x = ClampAngle(x, xMinLimit, xMaxLimit);
      y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
      y = ClampAngle(y, yMinLimit, yMaxLimit);
      Quaternion rotation = Quaternion.Euler(y, x, 0);

      //distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);
      distance = Mathf.Clamp(distance, distanceMin, distanceMax);
      //distance = 0.0f;

      RaycastHit hit;
      if (Physics.Linecast(target.position, transform.position, out hit))
      {
          distance -= hit.distance;
      }
      print("distance: " + distance);
      print("rotation: " + rotation);
      float targetY = target.position.y;
      Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
      print("camera position 1: " + transform.position);
      Vector3 position = rotation * negDistance + target.position;

      print("camera position 2: " + position);
      print("target position 2: " + target.position);
      transform.rotation = rotation;
      transform.position = position;
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}
