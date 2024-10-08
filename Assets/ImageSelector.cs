using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class ImageSelector : MonoBehaviour
{
    public UnityEngine.UI.Image image1;  // Assign in the Inspector
    public UnityEngine.UI.Image image2;  // Assign in the Inspector
    public Outline outline1;  // Assign the Outline of image1 in the Inspector
    public Outline outline2;  // Assign the Outline of image2 in the Inspector
    public SceneChange sceneChanger;  // Reference to the SceneChange script

    //private bool isImage1Selected = true;
    void Start()
    {
        // Set Image 1 as selected by default
        SelectImage1();  // Call the method to enable the outline for Image 1
    }

    public void SelectImage1()
    {
        outline1.enabled = true;
        outline2.enabled = false;
        sceneChanger.IsImage1Selected = true;  // Image 1 is selected
    }

    public void SelectImage2()
    {
        outline1.enabled = false;
        outline2.enabled = true;
        sceneChanger.IsImage1Selected = false;  // Image 2 is selected
    }

    
}
