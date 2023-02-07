using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoreUIElements : MonoBehaviour
{
    private static CoreUIElements _i;

    public static CoreUIElements i
    {
        get
        {
            return _i;
        }
    }

    private void Awake()
    {
        if (_i != null && _i != this)
            Destroy(gameObject);
        else
            _i = this;
    }

    [SerializeField]
    private UIText[] textComponents;

    [Serializable]
    private class UIText
    {
        public TextMeshProUGUI textHolder;

        public UIController.UITextComponent textComponent;
    }

    public TextMeshProUGUI
    GetTextComponent(UIController.UITextComponent textComponent)
    {
        foreach (UIText text in textComponents)
        {
            if (text.textComponent == textComponent) return text.textHolder;
        }
        Debug.LogError("Text Component" + textComponent + "missing!");
        return null;
    }
}
