using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class SettingsController : MonoBehaviour
{
    [SerializeField] private SettingsData defaultSettings = null;
    private SettingsData currentSaveData = null;
    public UnityEvent updateGameSettings = null;
    public UnityEvent updateScreenResolution = null;
    public UnityEvent clearListData = null;
    public UnityEvent saveKartData = null;
    [Header("scroll up")]
    [SerializeField] private TMP_InputField autoScrollUpSpeedInputField = null;
    [SerializeField] private TMP_Text autoScrollUpSpeedPlaceholderText = null;
    [Header("scroll down")]
    [SerializeField] private TMP_InputField autoScrollDownSpeedInputField = null;
    [SerializeField] private TMP_Text autoScrollDownSpeedPlaceholderText = null;
    [Header("Screen Height")]
    [SerializeField] private TMP_InputField screenHeightInputField = null;
    [SerializeField] private TMP_Text screenHeightPlaceholderText = null;
    [SerializeField] private Slider volumeSlider = null;
    [Header("auto scroll settings")]
    [SerializeField] private TMP_InputField autoScrollIdleTimeInputField = null;
    [SerializeField] private TMP_Text autoScrollIdleTimePlaceholderText = null;
    [Header("Panel Buttons")]
    [SerializeField] private Button openPanelButton = null;
    [SerializeField] private Button saveSettingsButton = null;
    [SerializeField] private Button saveScreenResolutionButton = null;
    [SerializeField] private Button clearListButton = null;
    [SerializeField] private Button saveKartDataButton = null;
    [SerializeField] private static readonly string saveDataFileName = Application.streamingAssetsPath + "/SettingsData.json";

    private void Start()
    {
        saveSettingsButton.onClick.AddListener(OnSaveSettingsButtonPressed);
        saveScreenResolutionButton.onClick.AddListener(OnSaveScreenResolutionButtonPressed);
        clearListButton.onClick.AddListener(OnClearButtonPressed);
        openPanelButton.onClick.AddListener(OnPanelOpened);
        saveKartDataButton.onClick.AddListener(OnSaveDataButtonPressed);
        
        Debug.Log(Application.streamingAssetsPath);
    }

    private void OnSaveDataButtonPressed()
    {
        saveKartData?.Invoke();
    }

    public SettingsData GetCurrentSettings()
    {
        return currentSaveData;
    }

    private void OnClearButtonPressed()
    {
        Debug.Log("Clear Data");
        clearListData?.Invoke();
    }

    private void OnSaveSettingsButtonPressed()
    {
        Debug.Log("Save Settings");

        if (screenHeightInputField.text != string.Empty)
            currentSaveData.screenHeight = int.Parse(screenHeightInputField.text);
        if (autoScrollDownSpeedInputField.text != string.Empty)
            currentSaveData.scrollDownSpeed = float.Parse(autoScrollDownSpeedInputField.text);
        if(autoScrollUpSpeedInputField.text != string.Empty)
            currentSaveData.scrollUpSpeed = float.Parse(autoScrollUpSpeedInputField.text);
        if (autoScrollIdleTimeInputField.text != string.Empty)
            currentSaveData.autoScrollTimeout = float.Parse(autoScrollIdleTimeInputField.text);
        currentSaveData.volume = volumeSlider.value;

        string saveData = JsonUtility.ToJson(currentSaveData);
        File.WriteAllText(saveDataFileName, saveData);
        updateGameSettings?.Invoke();
        //SettingsData loadedData = JsonUtility.FromJson<SettingsData>(saveData);
    }
    private void OnSaveScreenResolutionButtonPressed()
    {
        if (screenHeightInputField.text != string.Empty)
            currentSaveData.screenHeight = int.Parse(screenHeightInputField.text);
        updateScreenResolution?.Invoke();
    }

    private void OnPanelOpened()
    {
        autoScrollDownSpeedInputField.text = string.Empty;
        autoScrollUpSpeedInputField.text = string.Empty;
        autoScrollIdleTimeInputField.text = string.Empty;
        screenHeightInputField.text = string.Empty;

        autoScrollDownSpeedPlaceholderText.text = currentSaveData.scrollDownSpeed.ToString();
        autoScrollUpSpeedPlaceholderText.text = currentSaveData.scrollUpSpeed.ToString();
        autoScrollIdleTimeInputField.text = currentSaveData.autoScrollTimeout.ToString();
        screenHeightPlaceholderText.text = currentSaveData.screenHeight.ToString();
    }

    public void LoadSettingsData()
    {
        if(File.Exists(saveDataFileName))
        {
            string saveString = File.ReadAllText(saveDataFileName);
            currentSaveData = JsonUtility.FromJson<SettingsData>(saveString);
        }
        else
        {
            currentSaveData = defaultSettings;
            string saveData = JsonUtility.ToJson(currentSaveData);
            File.WriteAllText(saveDataFileName, saveData);
        }

        autoScrollDownSpeedPlaceholderText.text = currentSaveData.scrollDownSpeed.ToString();
        autoScrollUpSpeedPlaceholderText.text = currentSaveData.scrollUpSpeed.ToString();
        screenHeightPlaceholderText.text = currentSaveData.screenHeight.ToString();
        autoScrollIdleTimePlaceholderText.text = currentSaveData.autoScrollTimeout.ToString();

        volumeSlider.value = currentSaveData.volume;
    }
}
