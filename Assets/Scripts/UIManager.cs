using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static Dictionary<PanelType, UIPanelTemplate> panelsDict = new Dictionary<PanelType, UIPanelTemplate>();
    
    public static void ShowPanel(PanelType type)
    {
        panelsDict[type].ShowPanel();
    }
    public static void HidePanel(PanelType type)
    {
        panelsDict[type].HidePanel();
    }
    public static void HideAllPanels()
    {
        foreach(PanelType panelType in panelsDict.Keys)
        {
            panelsDict[panelType].holder.SetActive(false);
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
    }
    public static void HideClearSkillPanel()
    {
        HidePanel(PanelType.ClearSkillPanel);
        ShowMainPanel();

    }
    public static void ShowMoveSkillPanel()
    {
        HideAllPanels();
        ShowPanel(PanelType.MoveSkillPanel);
    }
    public static void HideMoveSkillPanel()
    {
        HidePanel(PanelType.MoveSkillPanel);
        ShowMainPanel();

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
    }
}
