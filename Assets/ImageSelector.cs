using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class ImageSelector : MonoBehaviour
{
    public UnityEngine.UI.Image image1;  // Assign in the Inspector
    public UnityEngine.UI.Image image2;  // Assign in the Inspector
    public Outline outline1;  // Assign the Outline of image1 in the Inspector
    public Outline outline2;  // Assign the Outline of image2 in the Inspector

    void Start()
    {
        // Set Image 1 as selected by default
        SelectImage1();  // Call the method to enable the outline for Image 1
    }

    public void SelectImage1()  // Ensure this method is public
    {
        outline1.enabled = true;  // Enable outline for Image 1
        outline2.enabled = false;  // Disable outline for Image 2
    }

    public void SelectImage2()  // Ensure this method is public
    {
        outline1.enabled = false;  // Disable outline for Image 1
        outline2.enabled = true;   // Enable outline for Image 2
    }
}
