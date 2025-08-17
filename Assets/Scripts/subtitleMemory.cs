using UnityEngine;
using TMPro;

public class DynamicSubtitleSimple : MonoBehaviour
{
    public TextMeshPro tutorialText3D;  
    public Transform playerCamera;       
    public float displayTime = 3f;       

    private int currentMessage = 0;

    void Start()
    {
        ShowMessage1();
    }

    void Update()
    {
        
        if (playerCamera != null && tutorialText3D.gameObject.activeSelf)
        {
            Vector3 lookPos = playerCamera.position - tutorialText3D.transform.position;
            lookPos.y = 0; 
            tutorialText3D.transform.rotation = Quaternion.LookRotation(lookPos);

            
            tutorialText3D.transform.Rotate(0f, 180f, 0f);
        }
    }

    void ShowMessage1()
    {
        tutorialText3D.text = "Welcome to the dark forest!";
        tutorialText3D.gameObject.SetActive(true);
        Invoke(nameof(ShowMessage2), displayTime);
    }

    void ShowMessage2()
    {
        tutorialText3D.text = "Solve the memory game to get the crystal!";
        Invoke(nameof(ShowMessage3), displayTime);
    }

    void ShowMessage3()
    {
        tutorialText3D.text = "Watch out for zombies!";
        Invoke(nameof(HideText), displayTime);
    }

    void HideText()
    {
        tutorialText3D.gameObject.SetActive(false);
    }
}
