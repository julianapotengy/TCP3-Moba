using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject owner;
    private Vector3 offset;
    private Vector3 position;
    private bool lockedCamera;
    public int boundary;
    public int moveSpeed;
    private int screenBoundsWidth;
    private int screenBoundsHeight;

    void Start()
    {
        offset = this.transform.position - owner.transform.position;
        position = this.transform.position;
        lockedCamera = false;
        boundary = 50;
        moveSpeed = 15;
        screenBoundsWidth = Screen.width;
        screenBoundsHeight = Screen.height;
    }
    
    void LateUpdate()
    {
        Rect screenRect = new Rect(0, 0, Screen.width, Screen.height);
        if (!screenRect.Contains(Input.mousePosition))
            return;

        if (Input.GetKeyDown(InputManager.IM.lockCamera))
        {
            lockedCamera = !lockedCamera;
        }

        if(lockedCamera)
        {
            this.transform.position = owner.transform.position + offset;
            position = this.transform.position;
        }
        else
        {
            if (Input.mousePosition.x > screenBoundsWidth - boundary)
            {
                position.x -= moveSpeed * Time.deltaTime;
            }
            if (Input.mousePosition.x < 0 + boundary)
            {
                position.x += moveSpeed * Time.deltaTime;
            }
            if (Input.mousePosition.y > screenBoundsHeight - 10)
            {
                position.z -= moveSpeed * Time.deltaTime;
            }
            if (Input.mousePosition.y < 0 + boundary)
            {
                position.z += moveSpeed * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                this.transform.position = owner.transform.position + offset;
                position = this.transform.position;
            }
            else this.transform.position = position;
        }
    }
}