using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public delegate void RightClickPanel(Sailor sailor);

public class SailorSlot : MonoBehaviour, IPointerDownHandler
{
    public RightClickPanel OnRightClick;

    [SerializeField] private Sailor sailor;
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image stationIcon;
    [SerializeField] private TextMeshProUGUI stationText;

    public Sailor GetSailor() { return sailor; }
    public void SetSailor(Sailor sailorer) { sailor = sailorer; }

    private void Start()
    {
        sailor.onSailorUIUpdate += UpdateUI;
        sailor.onStation = false; //temporary until later?  
        sailor.currentStation = Station.Stations.None;
        sailor.stationIcon = null;
        UpdateUI();
    }

    private void OnDestroy()
    {
        sailor.onSailorUIUpdate -= UpdateUI;
    }

    private void Update()
    {
        slider.value = sailor.fatigue;
        if (sailor.onStation)
        {
            if (sailor.currentStation == Station.Stations.Resting)
            {
                if (sailor.fatigue < sailor.maxFatigue) sailor.fatigue += 4f * Time.deltaTime;
                else sailor.fatigue = sailor.maxFatigue;
            }
            else
            {
                if (sailor.fatigue > 0f) sailor.fatigue -= 2f * Time.deltaTime;
            }
        }
    }

    public void UpdateUI()
    {
        nameText.text = sailor.sailorName;
        slider.maxValue = sailor.maxFatigue;
        stationText.text = sailor.stationText;
        //if (sailor.stationIcon != null)
        stationIcon.sprite = sailor.stationIcon;
        //slider.value = sailor.fatigue;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick?.Invoke(sailor);
        }
    }
}
