using UnityEngine;

public class MinimapFollow : MonoBehaviour
{
    public Transform player;
    public float height = 50f;

    void LateUpdate()
    {
        if (player == null) return;
        Vector3 newPos = player.position;
        newPos.y = height;
        transform.position = newPos;
    }
}
