using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private Camera cam;
    [SerializeField]
    private float parallaxSpeed;
    private float xPos;

    private float boundsX;
    
    void Start()
    {
        cam = Camera.main;
        xPos = transform.position.x;

        boundsX = GetComponent<SpriteRenderer>().bounds.size.x;
    }
    
    void Update()
    {
        // 摄像机与背景移动过程中距离的差值
        var distanceDelta = cam.transform.position.x * (1 - parallaxSpeed);
        var distanceToMove = cam.transform.position.x * parallaxSpeed;
        transform.position = new Vector3(xPos + distanceToMove, transform.position.y);

        if (distanceDelta > xPos + boundsX)
        {
            xPos = xPos + boundsX;
        }
        else if (distanceDelta < xPos - boundsX)
        {
            xPos = xPos - boundsX;
        }
    }
}
