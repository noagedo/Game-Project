using UnityEngine;

public class MultiCameraMouseController : MonoBehaviour
{
    public enum CameraMode { FirstPerson, ThirdPerson }
    public CameraMode mode = CameraMode.FirstPerson;

    public Transform playerBody;
    public Transform target;
    public float distance = 4f;
    public float height = 2f;
    public float sensitivity = 100f;

    private float xRotation = 0f;
    private float yRotation = 90f;

    private FairyAnimationController fairyController;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        Vector3 initialRot = transform.eulerAngles;
        yRotation = initialRot.y;
        xRotation = initialRot.x;

        if (playerBody != null)
            fairyController = playerBody.GetComponent<FairyAnimationController>();

        if (fairyController != null)
            fairyController.isFirstPerson = (mode == CameraMode.FirstPerson);
    }

    void LateUpdate()
    {
        if (!GetComponent<Camera>().enabled) return;

        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        if (mode == CameraMode.FirstPerson)
        {
            yRotation += mouseX;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f); // כדי לא להסתובב לגמרי אחורה

            // סיבוב המצלמה למעלה ולמטה
            transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);

            // סיבוב גוף השחקן רק בציר Y
            if (playerBody != null)
                playerBody.rotation = Quaternion.Euler(0f, yRotation, 0f);
        }
        else if (mode == CameraMode.ThirdPerson)
        {
            yRotation += mouseX;

            Quaternion rotation = Quaternion.Euler(0f, yRotation, 0f);
            Vector3 direction = rotation * Vector3.back;
            Vector3 desiredPosition = target.position + direction * distance + Vector3.up * height;

            transform.position = desiredPosition;
            transform.LookAt(target);

            if (fairyController != null)
                fairyController.isFirstPerson = false;
        }
    }
}
