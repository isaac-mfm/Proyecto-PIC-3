using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FurnitureMenu : MonoBehaviour
{
    [SerializeField] VisualTreeAsset folderUXML;
    [SerializeField] VisualTreeAsset furnitureUXML;
    [SerializeField] List<FurnitureFolder> furnitureFolders;
    private GroupBox inventory;
    private GroupBox placementMenu;
    private ScrollView folderArea;
    private ScrollView furnitureArea;
    private VisualElement backButton;
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        inventory = root.Q<GroupBox>("inventory");
        folderArea = inventory.Q<ScrollView>("folder-panel");
        furnitureArea = inventory.Q<ScrollView>("furniture-panel");
        placementMenu = inventory.Q<GroupBox>("placement-menu");
        backButton = furnitureArea.Q<VisualElement>("back-button");
        backButton.RegisterCallback<ClickEvent>(ev => CloseFolder());
        foreach (FurnitureFolder furnitureFolder in furnitureFolders)
        {
            Debug.Log("anadiendo carpeta: " + furnitureFolder.name);
            TemplateContainer folderTemplate = folderUXML.Instantiate();
            folderTemplate.Q<VisualElement>("folder").RegisterCallback<ClickEvent>(ev => 
            {
                Debug.Log("abriendo carspeta: " + furnitureFolder.name);
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
            furnitureTemplate.name = "furniture";
            furnitureTemplate.Q<IMGUIContainer>("furniture-icon").style.backgroundImage = furnitureButton.thumbnailTexture;
            furnitureTemplate.Q<Label>("furniture-text").text = furnitureButton.furniture.name;
            furnitureArea.Add(furnitureTemplate);
        }
        furnitureArea.style.display = DisplayStyle.Flex;
    }
    public void CloseFolder()
    {
        furnitureArea.style.display = DisplayStyle.None;
        folderArea.style.display = DisplayStyle.Flex;
        foreach(var child in furnitureArea.Children()) 
        {
            Debug.Log(child.name);
            if (child.name == "furniture") furnitureArea.Remove(child);
        }
        
    }
    public void SelectFurniture(Furniture furniture)
    {
        ARTapToPlace.INSTANCE.Cancel();
        ARTapToPlace.INSTANCE.selectedFurniture = furniture;
        placementMenu.style.display = DisplayStyle.Flex;
        Debug.Log("Furniture Selected: " + furniture.name);
    }
}
