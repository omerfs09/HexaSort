using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ProgressBar : UIPanelTemplate
{
    [SerializeField]private Image filler;
    [SerializeField] private TextMeshProUGUI fillTMP;
    [SerializeField]private float fillerLerpSpeed = 1;
    float fillRatio;
    void Update()
    {
        fillRatio = Mathf.Clamp(GameStats.Instance.GetProggress() / GameStats.Instance.GetProggressAim(),0,1);
        SetText((int)GameStats.Instance.GetProggress(), (int)GameStats.Instance.GetProggressAim());
        //filler.transform.localScale = new Vector3(Mathf.Lerp(filler.transform.localScale.x,fillRatio,Time.deltaTime*60*fillerLerpSpeed),1,1);    
        filler.fillAmount = Mathf.Lerp(filler.fillAmount, fillRatio, Time.deltaTime * 60 * fillerLerpSpeed);
    }
    public void SetRatio(float ratio)
    {
        fillRatio = ratio;
    }
    public void SetText(int divident,int divider)
    {
        fillTMP.text = divident.ToString() + "/" + divider.ToString();
    }
}
