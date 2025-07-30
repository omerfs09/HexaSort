using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanel : UIPanelTemplate
{
    public Button clearSkillButton,moveSkillButton,refreshDeskButton;

    
    void Start()
    {
        clearSkillButton.onClick.AddListener(() => OnClearButtonClicked());
        moveSkillButton.onClick.AddListener(() => OnMoveButtonClicked());
        refreshDeskButton.onClick.AddListener(() => OnRefreshButtonClicked());
    }
    public void OnClearButtonClicked()
    {
        GameController.Instance.ChangeControlState(ControlState.ClearSkill);
        UIManager.ShowPanel(PanelType.ClearSkillPanel);
    }
    public void OnMoveButtonClicked()
    {
        GameController.Instance.ChangeControlState(ControlState.MoveSkill);
        UIManager.ShowPanel(PanelType.MoveSkillPanel);
    }
    public void OnRefreshButtonClicked()
    {
        Desk.Instance.RefreshDesk();
        UIManager.ShowPanel(PanelType.RefreshDeskPanel);
    }
}
