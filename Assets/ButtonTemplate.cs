using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ButtonTemplate : MonoBehaviour
{
    public ButtonType type;
    private Button button;
    public void SetButton(bool active)
    {
        button.interactable = active;
    }
    public void Awake()
    {
        button = gameObject.GetComponent<Button>();
        UIManager.RegisterButton(type,this);
    }
}
public enum ButtonType
{
    SettingsButton,

}
