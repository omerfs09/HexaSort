using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class GoldPanel : UIPanelTemplate
{
    [SerializeField]private TextMeshProUGUI goldTMP;
    [SerializeField]private GameObject goldSprite;
    void Start()
    {
        UpdateTexts();    
    }
    public void ChangeGold(int newValue)
    {
        goldTMP.text = newValue.ToString();
        ChangeGoldAnimation();
    }
    public void UpdateTexts()
    {
        goldTMP.text =  LevelManager.Instance.GoldCount.ToString();
    }
    public void ChangeGoldAnimation()
    {
        goldSprite.transform.DOKill();
        goldSprite.transform.DOScale(Vector3.one*1.2f,0.2f);
        goldSprite.transform.DOScale(Vector3.one,0.2f).SetDelay(0.2f);
    }
}
