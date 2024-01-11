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
    public Image Crosshair;
    public TMP_Text itemHoverText;

    [Header("Raycast")]
    public float raycastDistance = 5f;
    public LayerMask itemLayer;

    [Header("Drag and Drop")]
    public Image dragIconImage;
    private Item currentDraggedItem;
    private int currentDragslotIndex = -1;

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

        if(inventory.activeInHierarchy && Input.GetMouseButtonDown(0))
        {
            dragInventoryIcon();
        }
        else if(currentDragslotIndex != -1 && Input.GetMouseButtonUp(0) || currentDragslotIndex != -1 && !inventory.activeInHierarchy) 
        {
            dropInventoryIcon();

        }

        dragIconImage.transform.position = Input.mousePosition;
    }

    private void itemRaycast(bool hasClicked = false)
    {
        itemHoverText.text = "";
        Ray ray = Camera.main.ScreenPointToRay(Crosshair.transform.position);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, itemLayer))
        {
            if (hit.collider != null)
            {
                if (hasClicked) // Pick up
                {
                    Item newItem = hit.collider.GetComponent<Item>();
                    if (newItem)
                    {
                        addItemToInventory(newItem);
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
            Debug.DrawRay(transform.position, Vector3.forward, Color.green);
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

    private void dragInventoryIcon()
    {
        for (int i = 0; i < InventorySlots.Count; i++)
        {
          Slot curSlot = InventorySlots[i];
            if(curSlot.hovered && curSlot.hasItem())
            {
                currentDragslotIndex = i;

                currentDraggedItem = curSlot.getItem();
                dragIconImage.sprite = currentDraggedItem.icon;
                dragIconImage.color = new Color(1, 1 , 1, 1);

                curSlot.setItem(null);
            }
        }

    }

    private void dropInventoryIcon()
    {

        dragIconImage.sprite = null;
        dragIconImage.color = new Color(1 , 1, 1, 0);
        for (int i = 0; i < InventorySlots.Count; i++)
        {

            Slot curSlot = InventorySlots[i];
            if(curSlot.hovered) 
            {
                if(curSlot.hasItem()) 
                {

                    Item itemToSwap = curSlot.getItem();

                    curSlot.setItem(currentDraggedItem);
                    InventorySlots[currentDragslotIndex].setItem(itemToSwap);

                    resetDragVariables();
                    return;
                
                }
                else
                {
                    curSlot.setItem(currentDraggedItem);
                    resetDragVariables();
                    return;
                }
            
            
            
            }
        }

        InventorySlots[currentDragslotIndex].setItem(currentDraggedItem);
        resetDragVariables();
        
    }



    private void resetDragVariables()
    {
        currentDraggedItem = null;
        currentDragslotIndex = -1;

    }
}