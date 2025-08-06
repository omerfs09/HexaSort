using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyMoneyPanel : UIPanelTemplate
{
    [SerializeField] private Button buyGoldButton;
    [SerializeField] private Button cancelButton;
    
    void Start()
    {
        buyGoldButton.onClick.AddListener(() => OnBuyButtonClik());
        cancelButton.onClick.AddListener(() => OnCancelButtonClik());
    }
    public void OnBuyButtonClik()
    {
        LevelManager.Instance.GoldCount += 500;
        UIManager.UpdateSkills();
        UIManager.UpdateGold();
        
    }
    public void OnCancelButtonClik()
    {
        UIManager.HideBuyGoldPanel();
    }
}
