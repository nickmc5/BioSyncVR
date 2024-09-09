using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LeftHandMenu : MonoBehaviour
{
  public GameObject menuPanel;
  public InputActionReference openMenuAction;
  private void Awake()
  {
    openMenuAction.action.Enable();
    openMenuAction.action.performed += ToggleMenu;
    InputSystem.onDeviceChange += OnDeviceChange; 
    menuPanel.SetActive(false);
  }

  private void OnDestroy()
  {
    openMenuAction.action.Disable();
    openMenuAction.action.performed -= ToggleMenu;
    InputSystem.onDeviceChange -= OnDeviceChange;
  }

  private void ToggleMenu(InputAction.CallbackContext context)
  {
    menuPanel.SetActive(!menuPanel.activeSelf);
  }

  private void OnDeviceChange(InputDevice device, InputDeviceChange change)
  {
    switch (change)
    {
        case InputDeviceChange.Disconnected:
            openMenuAction.action.Disable();
            openMenuAction.action.performed -= ToggleMenu;
            break;
        case InputDeviceChange.Reconnected:
            openMenuAction.action.Enable();
            openMenuAction.action.performed += ToggleMenu;
            break;
    }
  }


}
