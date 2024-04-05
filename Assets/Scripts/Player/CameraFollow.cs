using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private float minXClamp = -42f;
    private float maxXClamp = 50f;
    private float minYClamp = -42f;
    private float maxYClamp = 41f;
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