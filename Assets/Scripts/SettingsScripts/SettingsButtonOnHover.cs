using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SettingsButtonOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image settingsImage;
    [SerializeField] private TMP_Text settingsText;

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        settingsImage.color = Color.white;
        settingsText.color = Color.black;
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        settingsImage.color = Color.clear;
        settingsText.color = Color.clear;
    }
}
