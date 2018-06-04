using UnityEngine;
using UnityEngine;
using System.Collections;
[AddComponentMenu("Camera-Control/Mouse drag Orbit with zoom 2")]
public class DragMouseOrbit : MonoBehaviour
{
		public Transform target;
		public float distance = 5.0f;
		public float xSpeed = 120.0f;
		public float ySpeed = 120.0f;
		public float yMinLimit = -20f;
		public float yMaxLimit = 30f;
		public float distanceMin = .5f;
		public float distanceMax = 15f;
		public float smoothTime = 0.0f;
		float rotationYAxis = 0.0f;
		float rotationXAxis = 0.0f;
		float velocityX = 0.0f;
		float velocityY = 0.0f;
		bool touchedBall = false;
		// Use this for initialization
		void Start()
		{
				Vector3 angles = transform.eulerAngles;
				rotationYAxis = angles.y;
				rotationXAxis = angles.x;
				// Make the rigid body not change rotation
				if (GetComponent<Rigidbody>())
				{
						GetComponent<Rigidbody>().freezeRotation = true;
				}
		}
		void LateUpdate()
		{
			// if (Input.GetMouseButton(0)) {
			// 	print("down");
			// 	checkMouseDown();
			// }
			if (!Input.GetMouseButton(0)) {
				touchedBall = false;
			}

				if (target)
				{
						if (Input.GetMouseButton(0))
						{
								checkMouseDown();
								if(!touchedBall){
									float pointer_x = Input.GetAxis("Mouse X");
									float pointer_y = Input.GetAxis("Mouse Y");
									if (Input.touchCount > 0)
									{
											pointer_x = Input.touches[0].deltaPosition.x * 0.02f;
											pointer_y = Input.touches[0].deltaPosition.y * 0.02f;
									}
									//print(Input.GetAxis("Mouse X")+","+Input.GetAxis("Mouse Y"));
								velocityX += xSpeed * pointer_x * distance * 0.02f;
								velocityY += ySpeed * pointer_y * 0.02f;
								}
						}


						rotationYAxis += velocityX;
						rotationXAxis -= velocityY;
						rotationXAxis = ClampAngle(rotationXAxis, yMinLimit, yMaxLimit);
						Quaternion fromRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
						Quaternion toRotation = Quaternion.Euler(rotationXAxis, rotationYAxis, 0);
						Quaternion rotation = toRotation;

						//distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

						RaycastHit hit;
						if (Physics.Linecast(target.position, transform.position, out hit))
						{
								distance -= hit.distance;
						}
						Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
						Vector3 position = rotation * negDistance + target.position;

						transform.rotation = rotation;
						transform.position = position;
						velocityX = Mathf.Lerp(velocityX, 0, Time.deltaTime * smoothTime);
						velocityY = Mathf.Lerp(velocityY, 0, Time.deltaTime * smoothTime);

				}
		}

		void checkMouseDown(){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast (ray, out hit))
			{
				if(hit.transform.name == "Football")
				{
					touchedBall = true;
				}
				else {
					//touchedBall = false
				}
		}
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
