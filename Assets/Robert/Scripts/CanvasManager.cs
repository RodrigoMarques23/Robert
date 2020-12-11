using UnityEngine;
using UnityEngine.UI;
public class CanvasManager : MonoBehaviour
{
    public Text         interactionMessage;
    public GameObject   interactionBox;
    public RawImage[]   inventoryImage;

    public void ShowInteractionBox(string information)
    {
        interactionBox.SetActive(true);
        interactionMessage.text = information;
    }

    void Start()
    {
        HideInteractionBox();
    }
    public void HideInteractionBox()
    {
        interactionBox.SetActive(false);
    }

    public void Inventory(int x, Texture icon)
    {
        inventoryImage[x].texture = icon;
        inventoryImage[x].color = Color.white;
    }

    public void CleanInventory()
    {
        for (int x = 0; x < inventoryImage.Length; ++x)
        {
            inventoryImage[x].color = Color.clear;
            inventoryImage[x].texture = null;
        }
    }
}