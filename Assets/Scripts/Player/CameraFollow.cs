using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private float minXClamp = -52f;
    private float maxXClamp = 60f;
    private float minYClamp = -52f;
    private float maxYClamp = 51f;
    [SerializeField] private PlayerController player;

    private void LateUpdate()
    {
        if (player != null)
        {
            Vector3 cameraPos;

            cameraPos = transform.position;
            cameraPos.x = Mathf.Clamp(player.transform.position.x, minXClamp, maxXClamp);
            cameraPos.y = Mathf.Clamp(player.transform.position.y, minYClamp, maxYClamp);

            transform.position = cameraPos;
        }
    }
}