using UnityEngine;

public class BoundsCheck : MonoBehaviour
{
    public enum EType
    {
        Center,
        Inset,
        Outset
    }

    [Header("Inscribed")] public EType boundsType = EType.Center;
    public float radius = 1f;

    [Header("Dynamic")] 
    public static float camWidth;
    public static float camHeight;

    private void Awake()
    {
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
    }

    private void LateUpdate()
    {
        var checkRadius = 0f;
        if (boundsType == EType.Inset) checkRadius = -radius;
        if (boundsType == EType.Outset) checkRadius = radius;

        var pos = transform.position;

        if (pos.x > camWidth + checkRadius)
        {
            pos.x = camWidth + checkRadius;
        }

        if (pos.x < -camWidth - checkRadius)
        {
            pos.x = -camWidth - checkRadius;
        }

        if (pos.y > camHeight + checkRadius)
        {
            pos.y = camHeight + checkRadius;
        }

        if (pos.y < -camHeight - checkRadius)
        {
            pos.y = -camHeight - checkRadius;
        }

        transform.position = pos;
        
    }
}
