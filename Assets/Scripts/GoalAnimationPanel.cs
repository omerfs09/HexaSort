using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class GoalAnimationPanel : UIPanelTemplate
{
    [SerializeField]private TextMeshProUGUI messageTMP;
    [SerializeField]private TextMeshProUGUI titleTMP;
    [SerializeField]private GameObject titleBG;
    [SerializeField]private GameObject messageBG;
    [SerializeField]private float duration1;
    [SerializeField]private Ease ease1;
    [SerializeField]private GameObject target;

    
    
    private string message;
    public void ResetAnimation()
    {
        titleBG.transform.localPosition = titleBGStartPos;
        messageBG.transform.localPosition = messageBGStartPos;
        titleBG.transform.localScale = Vector3.one;
        messageBG.transform.localScale = Vector3.one;
    }
    
    public override void Awake()
    {
        base.Awake();
        titleBGStartPos = titleBG.transform.localPosition;
        messageBGStartPos = messageBG.transform.localPosition;
    }
    private Vector3 titleBGStartPos;
    private Vector3 messageBGStartPos;
    public void Start()
    {
        
    }
    public void SetMessage(string message)
    {
        this.message = message;
        ShowPanel();
    }
    public override void ShowPanel()
    {
        holder.SetActive(true);
        messageTMP.text = message;
        Sequence seq = DOTween.Sequence();
        ResetAnimation();
        messageTMP.transform.localScale = Vector3.zero;
        titleTMP.transform.localScale = Vector3.zero;
        seq.Append(messageTMP.transform.DOScale(Vector3.one, duration1).SetEase(ease1).SetDelay(0.2f));
        seq.Join(titleTMP.transform.DOScale(Vector3.one, duration1).SetEase(ease1).SetDelay(0));
        seq.Append(messageTMP.transform.DOScale(Vector3.one*1.25f, duration1).SetEase(ease1));
        seq.AppendInterval(1.5f);
        seq.Append(messageTMP.transform.DOScale(Vector3.zero, duration1).SetEase(ease1));
        seq.Append(titleTMP.transform.DOScale(Vector3.zero, duration1).SetEase(ease1));
        seq.Join(titleBG.transform.DOScale(new Vector3(0.1f, 1, 1), duration1).SetEase(ease1));
        seq.Join(messageBG.transform.DOScale(new Vector3(0.1f,1,1), duration1).SetEase(ease1));
        GameObject target = UIManager.GetPanel(PanelType.ProgressBar).holder.transform.GetChild(0).gameObject;
        seq.Join(titleBG.transform.DOLocalMove(target.transform.localPosition, duration1).SetEase(ease1));
        seq.Join(titleBG.transform.DOScale(Vector3.zero, duration1).SetEase(ease1));
        seq.Join(messageBG.transform.DOLocalMove(target.transform.localPosition, duration1).SetEase(ease1).SetDelay(0.1f));
        seq.Join(messageBG.transform.DOScale(Vector3.zero, duration1).SetEase(ease1));

        seq.Play();

    }
}
