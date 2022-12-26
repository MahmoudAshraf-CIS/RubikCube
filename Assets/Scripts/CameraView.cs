using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraView : MonoBehaviour
{
    // Update is called once per frame
    [SerializeField]
    Animator animator;
    public GameObject touchPoint;
    public float rotationSpeed = 0.5f, zoomSpeed = 30;
    public float verticalMin = -45, verticalMax = 45;
    public float zoomMin = -4, zoomMax = -2;
    
    Vector2 touchStart, touchEnd;
    Vector3 tempRotation;
    Vector2 delta;
    public bool active = true;

    private void Start()
    {
        transform.rotation = Quaternion.Euler(45, 45, 0);
        active = true;
        //animator.enabled = true;
    }
    void Update()
    {

        if (animator.enabled)
            return;
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            touchPoint.transform.position = touchStart;
            touchPoint.SetActive(true);
            Ray ray = Camera.main.ScreenPointToRay(touchStart);
            if (Physics.Raycast(ray,10))
                active = false;
            else
                active = true;
        }
        if (Input.GetMouseButton(0) && active)
        {
            touchEnd = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            delta = touchEnd - touchStart;
            delta = delta * Time.deltaTime;
            delta.Normalize();
            tempRotation = transform.localRotation.eulerAngles;
            tempRotation.y += delta.x * rotationSpeed * Time.deltaTime;
            tempRotation.x -= delta.y * rotationSpeed * Time.deltaTime;
            tempRotation.x = ClampAngle(tempRotation.x, verticalMin, verticalMax);
            
            transform.localRotation = Quaternion.Euler(tempRotation);
        }
        if (Input.GetMouseButtonUp(0))
        {
            active = false;
            touchPoint.SetActive(false);
        }

        if (Input.mouseScrollDelta.magnitude != 0)
        {
            Camera.main.gameObject.transform.localPosition = new Vector3(0, 0,
                Mathf.Clamp(Input.mouseScrollDelta.y *Time.deltaTime* zoomSpeed + Camera.main.gameObject.transform.localPosition.z, zoomMin,zoomMax)
                );
        }
 
        // maybe for android 
        //if (Input.touchCount == 1 )
        //{
        //    Touch touch = Input.GetTouch(0);
        //    touch.deltaPosition.Normalize();
        //    tempRotation = transform.localRotation.eulerAngles;
        //    tempRotation.y += touch.deltaPosition.x * speed;
        //    tempRotation.x -= touch.deltaPosition.y * speed;
        //    tempRotation.x = ClampAngle(tempRotation.x, verticalMin, verticalMax);
        //    transform.localRotation = Quaternion.Euler(tempRotation);
        //}
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < 90 || angle > 270)
        {       // if angle in the critic region...
            if (angle > 180) angle -= 360;  // convert all angles to -180..+180
            if (max > 180) max -= 360;
            if (min > 180) min -= 360;
        }
        angle = Mathf.Clamp(angle, min, max);
        if (angle < 0) angle += 360;  // if angle negative, convert to 0..360
        return angle;
    }

    public void SetAnimate(bool b)
    {
        if (!b)
        {
            transform.localPosition = Vector3.zero;
            transform.rotation = Quaternion.Euler(15,-45,0);
        }
        animator.enabled = b;
    }
}
