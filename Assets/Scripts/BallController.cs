using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

    public float speed;
    Vector3 distance;
    private Vector3 lastMousePosition;
    private Rigidbody rb;
	//// Use this for initialization
	//void Start () {

	//}

	//// Update is called once per frame
	//void Update () {

	//}

	private void Awake()
	{
        Application.targetFrameRate = 60;
        rb = gameObject.GetComponent<Rigidbody>();
	}

    void OnMouseDown()
    {
        lastMousePosition = Input.mousePosition;
    }

	void FixedUpdate()
    {
      if (rb.position.y < 0.11f) {
        Vector3 newPosition = gameObject.transform.position;
        newPosition.y = 0.11f;
        transform.position = newPosition;
      }
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        //rb.AddForce(movement * speed * Time.deltaTime);
        //rigidbody.AddForce(movement * speed * Time.deltaTime);
    }

	private void OnMouseDrag()
	{
        distance = Input.mousePosition - lastMousePosition;
//        Debug.Log("Distance = " + distance);
	}

	private void OnMouseUp()
	{
        kickBall();
	}

    void kickBall()
    {
        rb.AddForce(-distance * speed * Time.deltaTime);
    }
}
