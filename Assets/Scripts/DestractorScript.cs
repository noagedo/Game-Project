using UnityEngine;

public class DestractorScript : MonoBehaviour
{
    public GameObject remain;

    
    void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            Instantiate(remain,transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
            
                
    }
}
