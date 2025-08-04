using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : UIPanelTemplate
{
    [SerializeField]private Button rePlayButton,mainMenuButton;
    private void Start()
    {
        rePlayButton.onClick.AddListener(() => RePlayButtonOnClick());
        mainMenuButton.onClick.AddListener(() => MainMenuButtonOnClick()); 
    }
    private void RePlayButtonOnClick()
    {
        
        LevelManager.Instance.ReloadLevel();
    }
    private void MainMenuButtonOnClick()
    {

    }
    public override void HidePanel()
    {
        base.HidePanel();

    }
}
