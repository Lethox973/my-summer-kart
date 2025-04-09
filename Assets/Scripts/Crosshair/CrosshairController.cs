using UnityEngine;
using UnityEngine.UI;

public class CrosshairController : MonoBehaviour
{
    public Image crosshairImage;

    void Update()
    {
        // Example: Change crosshair color when aiming
        if (Input.GetMouseButton(1)) // Right mouse button is held down
        {
            crosshairImage.color = Color.red;
        }
        else
        {
            crosshairImage.color = Color.white;
        }
    }
}
