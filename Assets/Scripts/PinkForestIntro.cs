using UnityEngine;
using TMPro;

public class PinkForestIntro : MonoBehaviour
{
    public TextMeshPro tutorialText3D;  

    void Start()
    {
        tutorialText3D.text = "\nYour goal is to steal the crystal\nfrom the house.";
        tutorialText3D.gameObject.SetActive(true);

        Invoke(nameof(HideIntro), 3f); 
    }

    void HideIntro()
    {
        tutorialText3D.gameObject.SetActive(false);
    }
}
