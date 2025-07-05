using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera firstPersonCamera;
    public Camera thirdPersonCamera;

    void Start()
    {
        firstPersonCamera.enabled = false; 
        thirdPersonCamera.enabled = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))  
        {
            bool isFirstActive = firstPersonCamera.enabled;
            firstPersonCamera.enabled = !isFirstActive;
            thirdPersonCamera.enabled = isFirstActive;
        }
    }
}
