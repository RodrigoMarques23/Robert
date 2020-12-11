using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private const float MaxInteractionDistance = 2f;

    public CanvasManager canvasManager;

    private Transform _cameraTranform;
    private Interaction _currentInteraction;
    private bool _requirementsInInventory;
    private List<Interaction> _inventory;



    void Start()
    {
        _cameraTranform = GetComponentInChildren<Camera>().transform;
        _requirementsInInventory = false;
        _inventory = new List<Interaction>();
    }

    void Update()
    {
        CheckForInteraction();
        CheckForCurrentInteraction();
    }

    private void CheckForInteraction()
    {
        if (Physics.Raycast(_cameraTranform.position, _cameraTranform.forward, out RaycastHit hitInfo, MaxInteractionDistance))
        {
            Interaction interaction = hitInfo.transform.GetComponent<Interaction>();

            if (interaction == null)
                ClearCurrentInteraction();
            else if (interaction != _currentInteraction)
                SetCurrentInteraction(interaction);
        }
        else
            ClearCurrentInteraction();
    }

    private void SetCurrentInteraction(Interaction interaction)
    {
        _currentInteraction = interaction;

        if (PlayerHasInteractionRequirements())
        {
            _requirementsInInventory = true;
            canvasManager.ShowInteractionBox(interaction.interactionText);
        }
        else
        {
            _requirementsInInventory = false;
            canvasManager.ShowInteractionBox(interaction.requirementText);
        }
    }

    private void CheckForCurrentInteraction()
    {
        if (Input.GetMouseButtonDown(0) && _currentInteraction != null)
        {
            if (_currentInteraction.type == Interaction.InteractiveType.PICKABLE)
                PickCurrentInteraction();
            else if (_requirementsInInventory)
                InteractWithCurrentInteraction();
        }
    }

    private void InteractWithCurrentInteraction()
    {
        for (int i = 0; i < _currentInteraction.requirements.Length; ++i)
            RemoveFromInventory(_currentInteraction.requirements[i]);

        _currentInteraction.Interact();
    }


    private void ClearCurrentInteraction()
    {
        _currentInteraction = null;
        canvasManager.HideInteractionBox();
    }

    private void PickCurrentInteraction()
    {
        _currentInteraction.gameObject.SetActive(false);
        AddToInventory(_currentInteraction);
    }

    private bool PlayerHasInteractionRequirements()
    {
        if (_currentInteraction.requirements == null)
            return true;

        for (int i = 0; i < _currentInteraction.requirements.Length; ++i)
            if (!IsInInventory(_currentInteraction.requirements[i]))
                return false;

        return true;
    }

    private void AddToInventory(Interaction item)
    {
        _inventory.Add(item);
        canvasManager.Inventory(_inventory.Count - 1, item.icon); 
    }

    private void RemoveFromInventory(Interaction item)
    {
        _inventory.Remove(item);

        canvasManager.CleanInventory();

        for (int i = 0; i < _inventory.Count; ++i)
            canvasManager.Inventory(i, _inventory[i].icon);
    }

    private bool IsInInventory (Interaction item)
    {
        return _inventory.Contains(item);
    }
}
