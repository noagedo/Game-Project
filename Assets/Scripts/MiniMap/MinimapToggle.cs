using UnityEngine;
using UnityEngine.UI;

public class MinimapToggle : MonoBehaviour
{
    public Camera minimapCamera;       
    public RectTransform minimapUI;   

    public float smallSize = 30f;      
    public float largeSize = 80f;     

    public Vector2 smallUI = new Vector2(150, 150);  
    public Vector2 largeUI = new Vector2(400, 400);  

    private bool isExpanded = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMinimap();
        }
    }

    public void ToggleMinimap()
    {
        isExpanded = !isExpanded;

        if (isExpanded)
        {
            minimapCamera.orthographicSize = largeSize;
            minimapUI.sizeDelta = largeUI;
        }
        else
        {
            minimapCamera.orthographicSize = smallSize;
            minimapUI.sizeDelta = smallUI;
        }
    }
}
