using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class FurnitureButton
{
    [SerializeField] public Furniture furniture;
    [HideInInspector] public Button button;
    [HideInInspector] public RawImage thumbnail;
    [SerializeField] private Texture2D thumbnailTexture;
    public void SetThumbnail()
    {
        thumbnail.texture = thumbnailTexture;
    }
    public void InitializeButton()
    {
        button.onClick.AddListener(SelectFurniture);
    }
    public void SelectFurniture()
    {
        ARTapToPlace.INSTANCE.selectedFurniture = furniture;
        Debug.Log("Furniture Selected: " + furniture.name);
    }
}
