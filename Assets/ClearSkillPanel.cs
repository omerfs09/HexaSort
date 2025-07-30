using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearSkillPanel : UIPanelTemplate
{
    public Button cancelButton;
    void Start()
    {
        cancelButton.onClick.AddListener(() => OnCancelButtonClick());  
    }
    public void OnCancelButtonClick()
    {
        UIManager.HidePanel(panelType);
        GameController.Instance.ChangeControlState(ControlState.DragAndDrop);
    }
}
