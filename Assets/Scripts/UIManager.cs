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
    
}
