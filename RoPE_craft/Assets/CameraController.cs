using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject CameraTarget;
    [SerializeField, Range(1f, 500f)] private float wheelSpeed = 100f;
    [SerializeField, Range(0.1f, 1f)] private float moveSpeed = 0.3f;
    [SerializeField, Range(0.1f, 1f)] private float rotateSpeed = 1f;
    private Vector3 preMousePosition;


    
    private bool SmallWindow,dragMode;

    private void Start()
    {
        CameraTarget = GameObject.Find("CameraEmpty");
        LookCameraTarget();
        dragMode = false;
    }

    private void Update()
    {
        //Debug.Log(Input.mousePosition);
        if(Input.mousePosition.x < (200f*(float)Screen.width / 1138f) && Input.mousePosition.y < (160f * (float)Screen.height / 530f))
        {
            SmallWindow = true;
        }
        else{
            SmallWindow = false;
        }

        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        if(scrollWheel != 0.0f)
        {
            MouseWheel(scrollWheel);
        }
 
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            if(!SmallWindow)
            {
                dragMode = true;
                preMousePosition = Input.mousePosition;
            }
        }
        if(Input.GetMouseButtonUp(0)||Input.GetMouseButtonUp(1))
        {
            dragMode = false;
        }
        MouseDrag(Input.mousePosition);

        return;
    }

    private void MouseWheel(float delta)//前進/後退
    {
        this.transform.position += this.transform.forward * delta * wheelSpeed;
        return;
    }

    private void MouseDrag(Vector3 mousePosition)
    {
        Vector3 diff = mousePosition - preMousePosition;
        if (diff.magnitude < Vector3.kEpsilon)
        {
            return;
        }

        float d = distance();
        if (Input.GetMouseButton(0) && dragMode)//回転移動
        {
            this.transform.Translate(-diff * Time.deltaTime * rotateSpeed * d);
            LookCameraTarget();
            this.transform.position += this.transform.forward * ((transform.position - CameraTarget.transform.position).magnitude - d);//直線移動と曲線移動の誤差修正
        }
        else if (Input.GetMouseButton(1) && dragMode)//平行移動
        {
            this.transform.Translate(-diff * Time.deltaTime * moveSpeed * d);
            CameraTarget.transform.Translate(-diff * Time.deltaTime * moveSpeed * d);
            LookCameraTarget();
        }

        preMousePosition = mousePosition;
        return;
    }

    private float distance()
    {
        return (transform.position - CameraTarget.transform.position).magnitude;
    }

    private void LookCameraTarget()
    {
        transform.LookAt(CameraTarget.transform);
        CameraTarget.transform.rotation = transform.rotation;
        return;
    }
}
