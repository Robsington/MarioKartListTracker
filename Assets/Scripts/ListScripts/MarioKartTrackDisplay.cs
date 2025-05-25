using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class MarioKartTrackDisplay : MonoBehaviour
{
    private int trackIndex = 0;
    private int cupIndex = 0;
    [SerializeField] private TMP_Text trackName = null;
    [SerializeField] private TMP_Text trackCounter = null;
    [SerializeField] private Toggle trackCompletedToggle = null;
    [SerializeField] private Button trackButton = null;
    public UnityEvent counterChangedEvent = null;

    public void UpdateTrackContent(MarioKartList marioKartList)
    {
        trackCounter.text = marioKartList.cupList[cupIndex].cupTracks[trackIndex].trackCounter.ToString();
        trackCompletedToggle.isOn = marioKartList.cupList[cupIndex].cupTracks[trackIndex].trackCompleted;
    }
    public void InitializeMarioKartTrackDisplay(int cupIndex, int trackIndex, MarioKartList marioKartList)
    {
        this.trackIndex = trackIndex;
        this.cupIndex = cupIndex;
        trackName.text = marioKartList.cupList[cupIndex].cupTracks[trackIndex].trackName;
        trackCounter.text = marioKartList.cupList[cupIndex].cupTracks[trackIndex].trackCounter.ToString();
        trackCompletedToggle.isOn = marioKartList.cupList[cupIndex].cupTracks[trackIndex].trackCompleted;
    }

    public void IncreaseTrackCounter(MarioKartList marioKartList)
    {
        marioKartList.cupList[cupIndex].cupTracks[trackIndex].trackCounter++;
        trackCounter.text = marioKartList.cupList[cupIndex].cupTracks[trackIndex].trackCounter.ToString();

        if(marioKartList.cupList[cupIndex].cupTracks[trackIndex].trackCounter > 0)
        {
            marioKartList.cupList[cupIndex].cupTracks[trackIndex].trackCompleted = true;
            trackCompletedToggle.isOn = marioKartList.cupList[cupIndex].cupTracks[trackIndex].trackCompleted;
            trackCounter.text = marioKartList.cupList[cupIndex].cupTracks[trackIndex].trackCounter.ToString();
            if(marioKartList.cupList[cupIndex].cupTracks[trackIndex].trackCounter == 1)
                SoundManager.instance.PlayTrackCompleteSoundEffect(marioKartList.cupList[cupIndex].cupName);
        }
        counterChangedEvent?.Invoke();
    }

    public void DecreaseTrackCounter(MarioKartList marioKartList)
    {
        marioKartList.cupList[cupIndex].cupTracks[trackIndex].trackCounter--;
        if (marioKartList.cupList[cupIndex].cupTracks[trackIndex].trackCounter <= 0)
        {
            marioKartList.cupList[cupIndex].cupTracks[trackIndex].trackCounter = 0;
            marioKartList.cupList[cupIndex].cupTracks[trackIndex].trackCompleted = false;
            trackCompletedToggle.isOn = marioKartList.cupList[cupIndex].cupTracks[trackIndex].trackCompleted;
        }
        trackCounter.text = marioKartList.cupList[cupIndex].cupTracks[trackIndex].trackCounter.ToString();
        counterChangedEvent?.Invoke();
    }
}
