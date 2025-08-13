using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Hammer : MonoBehaviour
{
    public Action action;
    public void Animate()
    {
        gameObject.SetActive(true);
        StartCoroutine(cor());
        IEnumerator cor()
        {
            yield return new WaitForSeconds(0.2f);
            action?.Invoke();
            gameObject.SetActive(false);
            (UIManager.GetPanel(PanelType.BoostersPanel) as SkillPanel).SetClearSkillButton(true);

        }
    }
    
}
