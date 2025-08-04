using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillPanel : UIPanelTemplate
{
    [SerializeField] private Button clearSkillButton,moveSkillButton,refreshDeskButton;
    [SerializeField] private TextMeshProUGUI clearSkillTMP, moveSkillTMP, refreshDeskTMP;
    
    void Start()
    {
        clearSkillButton.onClick.AddListener(() => OnClearButtonClicked());
        moveSkillButton.onClick.AddListener(() => OnMoveButtonClicked());
        refreshDeskButton.onClick.AddListener(() => OnRefreshButtonClicked());
        UpdateTexts();

    }
    public void OnClearButtonClicked()
    {
        if(LevelManager.Instance.ClearSkillCount > 0)
        {
            GameController.Instance.ChangeControlState(ControlState.ClearSkill);
            UIManager.ShowClearSkillPanel();
            LevelManager.Instance.ClearSkillCount--;
            UpdateTexts();
        }
        else{
            //Buy Menu
        }
      
    }
    public void OnMoveButtonClicked()
    {
        if (LevelManager.Instance.MoveSkillCount > 0)
        {
            GameController.Instance.ChangeControlState(ControlState.MoveSkill);
            UIManager.ShowMoveSkillPanel();
            LevelManager.Instance.MoveSkillCount--;
            UpdateTexts();

        }
        else
        {
            //Buy Menu
        }
    }
    public void OnRefreshButtonClicked()
    {
        if (LevelManager.Instance.RefreshDeskCount > 0)
        {
            Desk.Instance.RefreshDesk();
            UIManager.ShowPanel(PanelType.RefreshDeskPanel);
            LevelManager.Instance.RefreshDeskCount--;
            UpdateTexts();

        }
        else
        {
            //Buy Menu
        }
    
        
    }

    public override void ShowPanel()
    {
        holder.SetActive(true);

    }

    public override void HidePanel()
    {
        holder.SetActive(false);
    }
    public void UpdateTexts()
    {
        clearSkillTMP.text = LevelManager.Instance.ClearSkillCount.ToString();
        moveSkillTMP.text = LevelManager.Instance.MoveSkillCount.ToString();
        refreshDeskTMP.text = LevelManager.Instance.RefreshDeskCount.ToString();
    }
}
