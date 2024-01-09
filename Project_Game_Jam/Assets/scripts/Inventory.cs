using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    [Header("UI")]
    public GameObject inventory;
    public List<Slot> InventorySlots = new List<Slot>();
    public Image crosshair;
    public TMP_Text itemHoverText;

    [Header("Raycast")]
    public float raycastDistance = 5f;
    public LayerMask itemLayer;

    public void Start()
    {
        toggleInventory(false);

        foreach (Slot uiSlot in InventorySlots)
        {
            uiSlot.initialiseSlot();
        }
    }

    public void Update()
    {
        itemRaycast(Input.GetMouseButtonDown(0));

        if (Input.GetKeyDown(KeyCode.E))
            toggleInventory(!inventory.activeInHierarchy);
    }

 private void ItemRaycast(bool hasClicked = false)
{
    itemHoverText.text = "";

    if (mainCamera == null || playerTransform == null)
    {
        Debug.LogError("Main camera or player transform not assigned in the Inspector!");
        return;
    }

    Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
    RaycastHit hit;

    Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.red, 0.1f); // Draw the ray for debugging

    if (Physics.Raycast(ray, out hit, raycastDistance, itemLayer))
    {
        if (hit.collider != null)
        {
            if (hasClicked) // Pick up
            {
                Item newItem = hit.collider.GetComponent<Item>();
                if (newItem)
                {
                    AddItemToInventory(newItem);
                }
            }
            else // Get the name
            {
                Item newItem = hit.collider.GetComponent<Item>();
                if (newItem)
                {
                    itemHoverText.text = newItem.name;
                }
            }
        }
    }
    else
    {
        Debug.Log("Raycast did not hit anything.");
    }
}

    private void addItemToInventory(Item itemToAdd)
    {
        int leftoverQuantity = itemToAdd.currentQuantity;
        Slot openSlot = null;
        for (int i = 0; i < InventorySlots.Count; i++)
        {
            Item heldItem = InventorySlots[i].getItem();

            if (heldItem != null && itemToAdd.name == heldItem.name)
            {
                int freeSpaceInSlot = heldItem.maxQuantity - heldItem.currentQuantity;

                if (freeSpaceInSlot >= leftoverQuantity)
                {
                    heldItem.currentQuantity += leftoverQuantity;
                    Destroy(itemToAdd.gameObject);
                    InventorySlots[i].updateData();
                    return;
                }
                else // Add as much as we can to the current slot
                {
                    heldItem.currentQuantity = heldItem.maxQuantity;
                    leftoverQuantity -= freeSpaceInSlot;
                }
            }
            else if (heldItem == null)
            {
                if (!openSlot)
                    openSlot = InventorySlots[i];
            }

            InventorySlots[i].updateData();
        }

        if (leftoverQuantity > 0 && openSlot)
        {
            openSlot.setItem(itemToAdd);
            itemToAdd.currentQuantity = leftoverQuantity;
            itemToAdd.gameObject.SetActive(false);
        }
        else
        {
            itemToAdd.currentQuantity = leftoverQuantity;
        }
    }

    private void toggleInventory(bool enable)
    {
        inventory.SetActive(enable);

        Cursor.lockState = enable ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = enable;

        // Disable the rotation of the camera.
      
    }
}