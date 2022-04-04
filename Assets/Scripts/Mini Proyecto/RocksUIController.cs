using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class RocksUIController : MonoBehaviour
{
    public static RocksUIController INSTANCE = null;
    [HideInInspector] public int value = 0;
    [HideInInspector] public bool playing = false;
    [HideInInspector] public VisualElement indicator = null;
    [SerializeField] private float pointerSpeedMultiplier = 1.0f;
    private GroupBox placeGroup = null;
    private GroupBox gameplayGroup = null;
    private GroupBox finishedGroup = null;
    private Button placeButton = null;
    private Button startButton = null;
    private VisualElement pointer = null;
    private Label indicatorValue = null;
    private Label finishedMessage = null;
    private float direction = 1;
    private void Awake()
    {
        INSTANCE = this;
    }
    private void Start()
    {
        GetUIElements(GetComponent<UIDocument>().rootVisualElement);
        SetUIEvents();
    }
    private void Update()
    {
        pointer.style.rotate = new StyleRotate(new Rotate(new Angle(pointer.style.rotate.value.angle.value + 0.1f * pointerSpeedMultiplier * direction)));
        value = (int)((1f - Mathf.Abs(pointer.style.rotate.value.angle.value / 60f)) * 100f);
        indicatorValue.text = value.ToString();
        if (pointer.style.rotate.value.angle.value >= 60) direction = -1f;
        else if (pointer.style.rotate.value.angle.value <= -60) direction = 1f;
    }
    private void GetUIElements(VisualElement root)
    {
        placeGroup = root.Q<GroupBox>("place-group");
        gameplayGroup = root.Q<GroupBox>("gameplay-group");
        finishedGroup = root.Q<GroupBox>("finish-group");
        placeButton = root.Q<Button>("place-button");
        startButton = root.Q<Button>("start-button");
        pointer = root.Q<VisualElement>("indicator-pointer");
        indicator = root.Q<VisualElement>("indicator-main");
        indicatorValue = root.Q<Label>("indicator-value");
        finishedMessage = root.Q<Label>("finish-advice");
    }
    private void SetUIEvents()
    {
        placeButton.clicked += SetPlacementLock;
        startButton.clicked += StartGame;
    }
    private void SetPlacementLock()
    {
        if (ARInstancer.INSTANCE.enabled) placeButton.text = "Unlock";
        else placeButton.text = "Lock";
        ARInstancer.INSTANCE.enabled = !ARInstancer.INSTANCE.enabled;
    }
    private void StartGame()
    {
        ARInstancer.INSTANCE.enabled = false;
        placeGroup.style.display = DisplayStyle.None;
        gameplayGroup.style.display = DisplayStyle.Flex;
        playing = true;
    }
    public void ShowFinishedGameMessage(string text, Color color)
    {
        playing = false;
        gameplayGroup.style.display = DisplayStyle.None;
        finishedMessage.text = text;
        finishedMessage.style.color = color;
        finishedGroup.style.display = DisplayStyle.Flex;
    }
}
