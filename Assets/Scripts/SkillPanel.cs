using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillPanel : UIPanelTemplate
{
    [SerializeField] private Button clearSkillButton,moveSkillButton,refreshDeskButton;
    [SerializeField] private TextMeshProUGUI clearSkillTMP, moveSkillTMP, refreshDeskTMP;
    [SerializeField] private TextMeshProUGUI clearSkillPriceTMP, moveSkillPriceTMP, refreshDeskPriceTMP;
    
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
            UseSkill();
        }
        else{
            if(LevelManager.Instance.GoldCount >= GameConstants.CLEARSKILL_PRICE)
            {
                LevelManager.Instance.GoldCount -= GameConstants.CLEARSKILL_PRICE;   
                GoldPanel panel = (GoldPanel)UIManager.GetPanel(PanelType.GoldPanel);
                panel.UpdateTexts();
                LevelManager.Instance.ClearSkillCount++;
                UseSkill();
            }
            else
            {

            }
        }
        void UseSkill()
        {
            GameController.Instance.ChangeControlState(ControlState.ClearSkill);
            UIManager.ShowClearSkillPanel();
            LevelManager.Instance.ClearSkillCount--;
            UpdateTexts();
        }
    }
    
    public void OnMoveButtonClicked()
    {
        if (LevelManager.Instance.MoveSkillCount > 0)
        {

            UseSkill();
        }
        else
        {
            if (LevelManager.Instance.GoldCount >= GameConstants.MOVESKILL_PRICE)
            {
                LevelManager.Instance.GoldCount -= GameConstants.MOVESKILL_PRICE;
                GoldPanel panel = (GoldPanel)UIManager.GetPanel(PanelType.GoldPanel);
                panel.UpdateTexts();
                LevelManager.Instance.MoveSkillCount++;

                UseSkill();

            }
            else
            {

            }
        }
        void UseSkill()
        {
            GameController.Instance.ChangeControlState(ControlState.MoveSkill);
            UIManager.ShowMoveSkillPanel();
            LevelManager.Instance.MoveSkillCount--;
            UpdateTexts();
        }
    }
    public void OnRefreshButtonClicked()
    {
        if (LevelManager.Instance.RefreshDeskCount > 0)
        {

            UseSkill();
        }
        else
        {
            if (LevelManager.Instance.GoldCount >= GameConstants.REFRESHDESK_PRICE)
            {
                LevelManager.Instance.GoldCount -= GameConstants.REFRESHDESK_PRICE;
                GoldPanel panel = (GoldPanel)UIManager.GetPanel(PanelType.GoldPanel);
                panel.UpdateTexts();
                LevelManager.Instance.RefreshDeskCount++;

                UseSkill();
            }
            else
            {

            }
        }
        void UseSkill()
        {
            Desk.Instance.RefreshDesk();
            UIManager.ShowPanel(PanelType.RefreshDeskPanel);
            LevelManager.Instance.RefreshDeskCount--;
            UpdateTexts();
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
        int clear = LevelManager.Instance.ClearSkillCount;
        int move = LevelManager.Instance.MoveSkillCount;
        int refresh = LevelManager.Instance.RefreshDeskCount;
        clearSkillTMP.text = clear.ToString();
        moveSkillTMP.text = move.ToString();
        refreshDeskTMP.text = refresh.ToString();
        
        clearSkillPriceTMP.text = GameConstants.CLEARSKILL_PRICE.ToString();
        moveSkillPriceTMP.text = GameConstants.MOVESKILL_PRICE.ToString();
        refreshDeskPriceTMP.text = GameConstants.REFRESHDESK_PRICE.ToString();
        if (clear > 0) clearSkillPriceTMP.gameObject.SetActive(false);
        else clearSkillPriceTMP.gameObject.SetActive(true);
        if (move > 0) moveSkillPriceTMP.gameObject.SetActive(false);
        else moveSkillPriceTMP.gameObject.SetActive(true);
        if (refresh > 0) refreshDeskPriceTMP.gameObject.SetActive(false);
        else refreshDeskPriceTMP.gameObject.SetActive(true);
    }
}
