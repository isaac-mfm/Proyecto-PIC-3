using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FurnitureFolderController : MonoBehaviour
{
    [SerializeField] VisualTreeAsset folderUXML;
    [SerializeField] VisualTreeAsset furnitureUXML;
    [SerializeField] List<FurnitureFolder> furnitureFolders;
    private GroupBox inventory;
    private ScrollView folderArea;
    private ScrollView furnitureArea;
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        inventory = root.Q<GroupBox>("inventory");
        folderArea = inventory.Q<ScrollView>("folder-panel");
        furnitureArea = inventory.Q<ScrollView>("furniture-panel");
        foreach (FurnitureFolder furnitureFolder in furnitureFolders)
        {
            Debug.Log("anadiendo carpeta: " + furnitureFolder.name);
            TemplateContainer folderTemplate = folderUXML.Instantiate();
            folderTemplate.Q<VisualElement>("folder").RegisterCallback<ClickEvent>(ev => 
            {
                Debug.Log("abriendo carpeta: " + furnitureFolder.name);
                OpenFolder(furnitureFolder);
            });
            folderTemplate.Q<Label>("folder-text").text = furnitureFolder.name;
            folderArea.Add(folderTemplate);
        }
    }
    public void OpenFolder(FurnitureFolder furnitureFolder)
    {
        folderArea.style.display = DisplayStyle.None;
        foreach (FurnitureButton furnitureButton in furnitureFolder.furnitureButtons)
        {
            TemplateContainer furnitureTemplate = furnitureUXML.Instantiate();
            furnitureTemplate.Q<VisualElement>("furniture").RegisterCallback<ClickEvent>(ev => 
            {
                SelectFurniture(furnitureButton.furniture);
            });
            furnitureTemplate.Q<IMGUIContainer>("furniture-icon").style.backgroundImage = furnitureButton.thumbnailTexture;
            furnitureTemplate.Q<Label>("furniture-text").text = furnitureButton.furniture.name;
            furnitureArea.Add(furnitureTemplate);
        }
        furnitureArea.style.display = DisplayStyle.Flex;
    }
    public void SelectFurniture(Furniture furniture)
    {
        ARTapToPlace.INSTANCE.selectedFurniture = furniture;
        Debug.Log("Furniture Selected: " + furniture.name);
    }
}
