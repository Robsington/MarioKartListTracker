using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    private const int screenWidth = 1080;

    [SerializeField] private MarioKartListDisplay marioKartListDisplay = null;

    [SerializeField] private AutoScrollerStateManager autoScroller = null;
    [SerializeField] private SettingsController settingsController = null;
    [SerializeField] private MarioKartListScriptable listScriptable = null;
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private SoundManager soundManager = null;
    [SerializeField] private IdleTimer idleTimer = null;

    private MarioKartList marioKartListData = new MarioKartList();
    //[SerializeField] private MarioKartListDataController listDataController = null;

    private void Start()
    {
        DOTween.Init();
        LoadSettings();
        UpdateScreenSettings();
        autoScroller.InitializeAutoScroller(settingsController.GetCurrentSettings());
        idleTimer.SetIdleTime(settingsController.GetCurrentSettings());
        marioKartListData = MarioKartDataLoader.TryLoadMKData();
        soundManager.SetVolume(settingsController.GetCurrentSettings());
        if (marioKartListData == null)
        {
            marioKartListData = new MarioKartList();
            InitializeListData(marioKartListData, listScriptable.so_MarioKartList);
            MarioKartDataLoader.SaveMKData(marioKartListData);
        }

        marioKartListDisplay.InitializeList(marioKartListData, listScriptable.so_MarioKartList);

        settingsController.updateGameSettings.AddListener(UpdateGameSettings);
        settingsController.updateScreenResolution.AddListener(UpdateScreenSettings);
        settingsController.saveKartData.AddListener(OnSaveKartDataButtonPressed);
        settingsController.clearListData.AddListener(OnClearListDataPressed);
    }

    private void OnClearListDataPressed()
    {
        marioKartListData.ClearData();
        marioKartListDisplay.UpdateListContent(marioKartListData);
        MarioKartDataLoader.SaveMKData(marioKartListData);
    }

    private void OnSaveKartDataButtonPressed()
    {
        Debug.Log("<color=cyan>[GameManager.OnSaveKartDataButtonPressed]</color>");
        MarioKartDataLoader.SaveMKData(marioKartListData);
    }

    private void InitializeListData(MarioKartList listData, SO_MarioKartList so_MarioKartList)
    {
        for(int cupIndex = 0; cupIndex < so_MarioKartList.marioKartCups.Count; cupIndex++)
        {
            MarioKartCup marioKartCup = new MarioKartCup();
            marioKartCup.cupName = so_MarioKartList.marioKartCups[cupIndex].cupName;
            listData.cupList.Add(marioKartCup);
            for(int trackIndex = 0; trackIndex < so_MarioKartList.marioKartCups[cupIndex].trackList.Count; trackIndex++)
            {
                MarioKartTrack marioKartTrack = new MarioKartTrack();
                marioKartTrack.trackName = so_MarioKartList.marioKartCups[cupIndex].trackList[trackIndex];
                listData.cupList[cupIndex].cupTracks.Add(marioKartTrack);
            }
        }
    }

    private void Update()
    {
        if(eventSystem.currentSelectedGameObject != null)
        {
            MarioKartTrackDisplay trackDisplay = eventSystem.currentSelectedGameObject.GetComponent<MarioKartTrackDisplay>();
            marioKartListDisplay.ListenForInput(marioKartListData, trackDisplay);
        }
    }

    private void UpdateScreenSettings()
    {
        int screenHeight = settingsController.GetCurrentSettings().screenHeight;
        Screen.SetResolution(screenWidth, screenHeight, FullScreenMode.Windowed);
    }
    private void UpdateGameSettings()
    {
        autoScroller.InitializeAutoScroller(settingsController.GetCurrentSettings());
        SoundManager.instance.SetVolume(settingsController.GetCurrentSettings());
        idleTimer.SetIdleTime(settingsController.GetCurrentSettings());
    }

    private void LoadSettings()
    {
        settingsController.LoadSettingsData();
    }
}
