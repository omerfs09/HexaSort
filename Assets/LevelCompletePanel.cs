using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompletePanel : UIPanelTemplate
{
    public Button nextLevelButton;
    void Start()
    {
        nextLevelButton.onClick.AddListener(() => NextLevelButtonOnClick());    
    }
    private void NextLevelButtonOnClick()
    {
        LevelManager.Instance.LoadNextLevel();
    }
}
