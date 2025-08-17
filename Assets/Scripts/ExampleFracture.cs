using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleFracture : MonoBehaviour
{
    public GameObject[] asteroids;
    public GameObject chonker;
    private int counter = 0;

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            asteroids[counter].GetComponent<Fracture>().FractureObject();
            counter++;
        }
        if (Input.GetKey(KeyCode.I))
        {
            chonker.gameObject.SetActive(true);
        }

    }
}
