using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private Camera cam;
    [SerializeField][Range(0,1)]
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
        var distanceToMove = cam.transform.position.x * parallaxSpeed;
        // 将目标位置设置为摄像机位置加上距离
        transform.position = new Vector3(xPos + distanceToMove, transform.position.y);
        
        // 计算相机位置与视差速度之间的距离差
        var distanceDelta = cam.transform.position.x * (1 - parallaxSpeed);
        // 如果距离差大于xPos加上边界值，则将xPos设置为xPos加上边界值
        if (distanceDelta > xPos + boundsX)
            xPos = xPos + boundsX;
        // 如果距离差小于xPos减去边界值，则将xPos设置为xPos减去边界值
        else if (distanceDelta < xPos - boundsX)
            xPos = xPos - boundsX;
    }
}
