using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static Dictionary<PanelType, UIPanelTemplate> panelsDict = new Dictionary<PanelType, UIPanelTemplate>();
    
    public static void ShowPanel(PanelType type)
    {
        panelsDict[type].ShowPanel();
    }
    public static void HidePanel(PanelType type)
    {
        panelsDict[type].HidePanel();
    }
    public static UIPanelTemplate GetPanel(PanelType type)
    {
        return panelsDict[type];
    }
    public static void HideAllPanels()
    {
        foreach(PanelType panelType in panelsDict.Keys)
        {
            InstantHide(panelType);
        }
    }
    public static void RegisterPanel(PanelType panelType,UIPanelTemplate panel)
    {
        if (!panelsDict.ContainsKey(panelType))
        {

            panelsDict.Add(panelType, panel);
            Debug.Log("**"+ panelType.ToString());
        }
        else
        Debug.LogWarning("This type of panel exists => " + panelType.ToString());
    }
    public static void ShowClearSkillPanel()
    {
        HideAllPanels();
        ShowPanel(PanelType.ClearSkillPanel);
        ShowPanel(PanelType.FocusPanel);
    }
    public static void HideClearSkillPanel()
    {
        HidePanel(PanelType.ClearSkillPanel);
        ShowMainPanel();
        InstantShow(PanelType.FocusPanel);
        HidePanel(PanelType.FocusPanel);

    }
    public static void ShowMoveSkillPanel()
    {
        HideAllPanels();
        ShowPanel(PanelType.MoveSkillPanel);
        ShowPanel(PanelType.FocusPanel);
    }
    public static void HideMoveSkillPanel()
    {
        HidePanel(PanelType.MoveSkillPanel);
        ShowMainPanel();
        InstantShow(PanelType.FocusPanel);
        HidePanel(PanelType.FocusPanel);

    }
    public static void InstantShow(PanelType panelType)
    {
        panelsDict[panelType].holder.SetActive(true);
    }
    public static void InstantHide(PanelType panelType)
    {
        panelsDict[panelType].holder.SetActive(false);
    }
    public static void ShowSettingsPanel()
    {
        HideAllPanels();
        ShowPanel(PanelType.SettingsPanel);
    }
    public static void HideSettingsPanel()
    {
        ShowMainPanel();
        HidePanel(PanelType.SettingsPanel);
    }
    public static void ShowMainPanel()
    {
        HideAllPanels();
        ShowPanel(PanelType.BoostersPanel);
        ShowPanel(PanelType.ProgressBar);
        ShowPanel(PanelType.GoldPanel);
    }
    public static void ShowLevelCompletePanel()
    {
        HideAllPanels();
        ShowPanel(PanelType.LevelCompletePanel);
    }
    public static void UpdateSkills()
    {
        SkillPanel panel = (SkillPanel)panelsDict[PanelType.BoostersPanel];
        panel.UpdateTexts();

    }
    public static void UpdateGold()
    {
        GoldPanel panel = (GoldPanel)panelsDict[PanelType.GoldPanel];
        panel.UpdateTexts();

    }
    public static void HideBuyGoldPanel()
    {
        HidePanel(PanelType.BuyGoldPanel);
        ShowMainPanel();
    }
    public static void ShowBuyGoldPanel()
    {
        HideAllPanels();
        ShowPanel(PanelType.BuyGoldPanel);
    }
    public static void OnLevelLoad()
    {
        ShowPanel(PanelType.GoalPanel);
    }
    public static void ShowGoalPanel(string message)
    {
        GoalAnimationPanel panel =  (GoalAnimationPanel)panelsDict[PanelType.GoalPanel];
        panel.SetMessage(message);
        panel.ShowPanel();
    }
}
