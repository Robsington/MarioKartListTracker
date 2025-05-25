using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class MarioKartCupDisplay : MonoBehaviour
{
    private int cupIndex = 0;
    [SerializeField] private TMP_Text cupName = null;
    [SerializeField] private TMP_Text cupTotalCounter = null;
    [SerializeField] private Toggle cupCompletedToggle = null;

    [SerializeField] private MarioKartTrackDisplay marioKartTrackDisplayPrefab = null;
    [SerializeField] private Transform cupContent = null;
    [SerializeField] private RectTransform cupDisplayRectTransform = null;
    [SerializeField] private Image leftCupImage = null;
    [SerializeField] private Image rightCupImage = null;
    public UnityEvent cupCompletedEvent = null;
    public UnityEvent cupTrackCompleted = null;

    public float GetRectHeight() { return cupDisplayRectTransform.rect.height; }

    private List<MarioKartTrackDisplay> cupTracks = new List<MarioKartTrackDisplay>();
    private bool alreadyPlayedSoundEffect = false;
    public void InitializeKartCupDisplay(int cupIndex, MarioKartList marioKartList, SO_MarioKartList so_MarioKartList)
    {
        this.cupIndex = cupIndex;
        this.cupName.text = marioKartList.cupList[cupIndex].cupName;
        cupCompletedToggle.isOn = marioKartList.cupList[cupIndex].cupCompleted;
        cupTotalCounter.text = marioKartList.cupList[cupIndex].totalTracksPlayedCounter.ToString();
        leftCupImage.sprite = so_MarioKartList.marioKartCups[cupIndex].cupEmblem;
        rightCupImage.sprite = so_MarioKartList.marioKartCups[cupIndex].cupEmblem;
        CreateTrackList(marioKartList);
    }
    /// <summary>
    /// create the list of tracks within the cup
    /// </summary>
    /// <param name="marioKartList"></param>
    private void CreateTrackList(MarioKartList marioKartList)
    {
        int trackCount = marioKartList.cupList[cupIndex].cupTracks.Count;

        for(int trackIndex = 0; trackIndex < trackCount; trackIndex++)
        {
            GameObject go = Instantiate(marioKartTrackDisplayPrefab.gameObject, cupContent);
            MarioKartTrackDisplay track = go.GetComponent<MarioKartTrackDisplay>();
            track.InitializeMarioKartTrackDisplay(cupIndex, trackIndex, marioKartList);
            track.counterChangedEvent.AddListener(() => TrackCounterChanged(marioKartList));
            cupTracks.Add(track);
        }
    }
    public void UpdateCupContent(MarioKartList mariokartList)
    {
        cupTotalCounter.text = mariokartList.cupList[cupIndex].totalTracksPlayedCounter.ToString();
        cupCompletedToggle.isOn = mariokartList.cupList[cupIndex].cupCompleted;
        for (int trackIndex = 0; trackIndex < cupTracks.Count; trackIndex++)
            cupTracks[trackIndex].UpdateTrackContent(mariokartList);
    }
    public void TrackCounterChanged(MarioKartList marioKartList)
    {
        List<MarioKartTrack> cupTracks = marioKartList.cupList[cupIndex].cupTracks;
        int currentTotal = 0;
        bool allTracksCompleted = true;
        for (int trackIndex = 0; trackIndex < cupTracks.Count; trackIndex++)
        {
            currentTotal += cupTracks[trackIndex].trackCounter;
            if (cupTracks[trackIndex].trackCompleted == false)
            {
                alreadyPlayedSoundEffect = false;
                allTracksCompleted = false;
            }
        }
        cupTotalCounter.text = currentTotal.ToString();
        marioKartList.cupList[cupIndex].totalTracksPlayedCounter = currentTotal;
        marioKartList.cupList[cupIndex].cupCompleted = allTracksCompleted;
        cupCompletedToggle.isOn = marioKartList.cupList[cupIndex].cupCompleted;
        cupTrackCompleted?.Invoke();
        if (cupCompletedToggle.isOn)
        {
            if (!alreadyPlayedSoundEffect)
            {
                SoundManager.instance.PlayCupCompleteSoundEffect(marioKartList.cupList[cupIndex].cupName);
                alreadyPlayedSoundEffect = true;
            }
            cupCompletedEvent?.Invoke();
        }
    }
}
