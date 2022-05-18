using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class FurniturePlacement : MonoBehaviour
{
    public static FurniturePlacement INSTANCE;
    [HideInInspector] public GroupBox placementMenu;
    private Button placeButton;
    private Button cancelButton;
    public void Awake()
    {
        INSTANCE = this;
    }
    public void Start()
    {
        GetElements();
        InitializeButtons();
    }
    private void GetElements()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        placementMenu = root.Q<GroupBox>("placement-menu");
        placeButton = placementMenu.Q<Button>("place-button");
        cancelButton = placementMenu.Q<Button>("cancel-button");
    }
    private void InitializeButtons()
    {
        placeButton.RegisterCallback<ClickEvent>(ev => ARTapToPlace.INSTANCE.Place());
        cancelButton.RegisterCallback<ClickEvent>(ev => ARTapToPlace.INSTANCE.Cancel());
    }
}
