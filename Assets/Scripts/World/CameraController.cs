using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float smoothing;
    public Vector2 minPosition;
    public Vector2 maxPosition;
    public bool maintainWidth = true;
    private float defaultWidth;

    // Start is called before the first frame update
    void Start()
    {
        //maintains perfect width for default resolution
        defaultWidth = Camera.main.orthographicSize * Camera.main.aspect;
    }

    void Update() 
    {
        //changes width depending on set aspect ratio
        if (maintainWidth) 
        {
            Camera.main.orthographicSize = defaultWidth / Camera.main.aspect;
        }
    }

    // LateUpdate is called once per frame, but after Update() function
    void FixedUpdate()
    {
        if (target != null)
        {
            //makes sure the position of the camera is following the player by finding them and the view stays within a certain size (creating a zelda-eske feel)
            if (transform.position != target.position) {

                Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

                //clamps camera between certain points
                targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
                targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);

                Vector3 position = new Vector3(float.Parse(transform.position.x.ToString("0.000")), float.Parse(transform.position.y.ToString("0.000")), transform.position.z);

                //creates camera lag for smoother camera
                transform.position = Vector3.Lerp(position, targetPosition, smoothing);
            }
        }
    }
}
