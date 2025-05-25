using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MarioKartListDisplay : MonoBehaviour
{
    //public MarioKartList marioKartList = new MarioKartList();
    [SerializeField] private Transform contentTransform = null;
    [Header("prefabs")]
    [SerializeField] private MarioKartTotalDisplay finalTotalPrefab = null;
    private MarioKartTotalDisplay _finalTotalDisplay = null;
    [SerializeField] private MarioKartCupDisplay cupDisplayPrefab = null;
    private List<MarioKartCupDisplay> marioKartCups = new List<MarioKartCupDisplay>();
    private RectTransform contentRect = null;
    public UnityEvent onSixtyNinePlayed = null;
    #region initialization

    public void InitializeList(MarioKartList marioKartList, SO_MarioKartList so_MarioKartList)
    {
        contentRect = contentTransform.GetComponent<RectTransform>();
        ScaleContent(marioKartList);
        CreateList(marioKartList, so_MarioKartList);
        CreateFinalTalley(marioKartList);
    }

    /// <summary>
    /// scale the content transform to accomodate the size of the list and finalTotal prefab
    /// </summary>
    private void ScaleContent(MarioKartList marioKartList)
    {
        float height = (cupDisplayPrefab.GetRectHeight() * marioKartList.cupList.Count) + finalTotalPrefab.GetHeight();
        contentRect.sizeDelta = new Vector2(contentRect.rect.width, height);
    }

    /// <summary>
    /// Create the Cup List
    /// </summary>
    private void CreateList(MarioKartList marioKartList, SO_MarioKartList so_MarioKartList)
    {
        for(int kartListIndex = 0; kartListIndex < marioKartList.cupList.Count; kartListIndex++)
        {
            GameObject go = Instantiate(cupDisplayPrefab.gameObject, contentTransform);
            MarioKartCupDisplay cup = go.GetComponent<MarioKartCupDisplay>();
            cup.InitializeKartCupDisplay(kartListIndex, marioKartList, so_MarioKartList);
            cup.cupCompletedEvent.AddListener(() => OnCupCompleted(marioKartList));
            cup.cupTrackCompleted.AddListener(() => OnCupTrackCompleted(marioKartList));
            marioKartCups.Add(cup);
        }
    }
    private void OnCupTrackCompleted(MarioKartList marioKartList)
    {
        int total = 0;
        for(int i = 0; i < marioKartList.cupList.Count; i++)
        {
            total += marioKartList.cupList[i].totalTracksPlayedCounter;
        }
        marioKartList.totalTracksPlayed = total;
        _finalTotalDisplay.SetMarioKartTotal(marioKartList.totalTracksPlayed, marioKartList.allCupsCompleted);
        if (total == 69)
            onSixtyNinePlayed?.Invoke();
    }
    public void UpdateListContent(MarioKartList marioKartList)
    {
        _finalTotalDisplay.SetMarioKartTotal(0, false);
        for(int i = 0; i < marioKartCups.Count; i++)
            marioKartCups[i].UpdateCupContent(marioKartList);
    }
    private void OnCupCompleted(MarioKartList marioKartList)
    {
        int total = 0;
        bool allCompleted = true;
        for(int i = 0; i < marioKartList.cupList.Count; i++)
        {
            if (!marioKartList.cupList[i].cupCompleted)
                allCompleted = false;
            total += marioKartList.cupList[i].totalTracksPlayedCounter;
        }
        marioKartList.totalTracksPlayed = total;
        marioKartList.allCupsCompleted = allCompleted;
        _finalTotalDisplay.SetMarioKartTotal(marioKartList.totalTracksPlayed, marioKartList.allCupsCompleted);
        if (allCompleted)
            SoundManager.instance.PlayAllCompletedSoundEffect();
    }

    private void CreateFinalTalley(MarioKartList marioKartList)
    {
        GameObject go = Instantiate(finalTotalPrefab.gameObject, contentTransform);
        _finalTotalDisplay = go.GetComponent<MarioKartTotalDisplay>();
        _finalTotalDisplay.InitializeMarioKartTotal(marioKartList);
    }
    #endregion

    /// <summary>
    /// listening for input from the keyboard
    /// </summary>
    public void ListenForInput(MarioKartList marioKartList, MarioKartTrackDisplay trackDisplay)
    {
        if (trackDisplay == null)
            return;

        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            trackDisplay.DecreaseTrackCounter(marioKartList);
        }
        if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            trackDisplay.IncreaseTrackCounter(marioKartList);
        }
    }
}
