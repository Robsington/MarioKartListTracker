using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MarioKartTotalDisplay : MonoBehaviour
{
    [SerializeField] private RectTransform finalDisplayRect = null;
    public float GetHeight() { return finalDisplayRect.rect.height; }

    [SerializeField] private TMP_Text totalText = null;
    [SerializeField] private Toggle[] finalToggle = null;

    public void InitializeMarioKartTotal(MarioKartList marioKartList)
    {
        int currentTotal = 0;
        bool allCupsCompleted = true;
        for(int cupIndex = 0; cupIndex < marioKartList.cupList.Count; cupIndex++)
        {
            if (marioKartList.cupList[cupIndex].cupCompleted)
            {
                currentTotal += marioKartList.cupList[cupIndex].totalTracksPlayedCounter;
            }
            else
                allCupsCompleted = false;
        }
        
        totalText.text = currentTotal.ToString();
        for(int i = 0; i < finalToggle.Length; i++)
        {
            finalToggle[i].isOn = allCupsCompleted;
        }
    }

    public void SetMarioKartTotal(int total, bool allCompleted)
    {
        totalText.text = total.ToString();
        for(int i = 0; i < finalToggle.Length; i++)
            finalToggle[i].isOn = allCompleted;
    }
}