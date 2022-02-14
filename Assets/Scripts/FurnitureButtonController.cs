using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FurnitureButtonController : MonoBehaviour
{
    [SerializeField] private List<FurnitureButton> furnitureButtons = null;
    [SerializeField] private Button buttonTemplate = null;
    [SerializeField] private Transform layout = null;
    public void Start()
    {
        foreach (FurnitureButton furnitureButton in furnitureButtons)
        {
            furnitureButton.button = Instantiate(buttonTemplate, layout);
            furnitureButton.thumbnail = furnitureButton.button.GetComponent<RawImage>();
            furnitureButton.SetThumbnail();
            furnitureButton.InitializeButton();
        }
    }
    public void ClearSelection()
    {
        
    }
}
