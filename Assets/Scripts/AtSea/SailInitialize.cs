using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SailInitialize : MonoBehaviour
{
    [SerializeField] PlayerRoster playerRoster;
    [SerializeField] SailingManager sailingManager;
    [SerializeField] MapManager mapManager;
    [SerializeField] private float timer;
    [SerializeField] private Slider journeySlider, durabilitySlider, damagedDurabilitySlider, bilgeSlider;

    private void OnEnable()
    {
        mapManager.DestinationSetExtras.AddListener(ResetTimers);
    }

    private void OnDisable()
    {
        mapManager.DestinationSetExtras.RemoveListener(ResetTimers);
    }

    void Start()
    {
        CalcSpeedMod(); //make this happen when dropping off cargo mid trip

        ResetTimers();
        playerRoster.distanceTraveled = 0f;
        durabilitySlider.maxValue = playerRoster.ship.maxDurability;
        durabilitySlider.value = playerRoster.ship.currentDurability;
        damagedDurabilitySlider.maxValue = playerRoster.ship.maxDurability;
        damagedDurabilitySlider.value = playerRoster.ship.maxDamagedDurability;
        bilgeSlider.maxValue = playerRoster.ship.maxBilge;
        bilgeSlider.value = playerRoster.ship.currentBilge;
        playerRoster.ship.currentBilge = 0f;

        sailingManager.sailingMod = 0f;
        sailingManager.carpentingMod = 0f;
        sailingManager.bilgingMod = 0f;
        sailingManager.onNav = false;
    }

    private void ResetTimers()
    {
        timer = playerRoster.currentDistanceToTravel;
        journeySlider.maxValue = timer;
        journeySlider.value = timer;
    }

    private void Update()
    {
        //Debug.Log($"timer: {timer}, slider value: {journeySlider.value}");
        if (sailingManager.onNav) // if navigation maned, ship moves
        {
            timer -= sailingManager.SpeedFactor * Time.deltaTime;
            journeySlider.value = timer;
            playerRoster.distanceTraveled += sailingManager.SpeedFactor * Time.deltaTime;
            if (timer <= 0f) Docking();
        }

        playerRoster.ship.currentDurability -= 1f * Time.deltaTime; // constant durability reduction
        playerRoster.ship.maxDamagedDurability -= 0.4f * Time.deltaTime;

        if (playerRoster.ship.currentDurability < playerRoster.ship.maxDurability) // if "damage" taken
        {

            if (playerRoster.ship.currentDurability > playerRoster.ship.maxDurability * 0.75f) playerRoster.ship.currentBilge += 0.3f * Time.deltaTime; // constant bilge increase
            else if (playerRoster.ship.currentDurability < playerRoster.ship.maxDurability * 0.40f) playerRoster.ship.currentBilge += 1.5f * Time.deltaTime; // add visual representation for this ??
            else playerRoster.ship.currentBilge += 0.8f * Time.deltaTime;

            if (playerRoster.ship.currentBilge < playerRoster.ship.maxBilge)
            {
                if (playerRoster.ship.currentBilge > 0f) playerRoster.ship.currentBilge -= sailingManager.bilgingMod / 2f * Time.deltaTime; //reduce bilge, if below 0 make 0
                else playerRoster.ship.currentBilge = 0f;
            }
            // else sink

            if (playerRoster.ship.currentDurability < playerRoster.ship.maxDamagedDurability)
            {
                playerRoster.ship.currentDurability += (sailingManager.carpentingMod) / 2f * Time.deltaTime; //ship repair
            }
            else playerRoster.ship.currentDurability = playerRoster.ship.maxDamagedDurability;
        }

        bilgeSlider.value = playerRoster.ship.currentBilge;
        durabilitySlider.value = playerRoster.ship.currentDurability;
        damagedDurabilitySlider.value = playerRoster.ship.maxDamagedDurability;
        //Debug.Log($"Speed: {speed + sailingManager.sailingMod / 2f}, Durability: {playerRoster.ship.currentDurability}, Bilge: {playerRoster.ship.currentBilge}");
    }

    private void Docking()
    {
        playerRoster.currentDock = playerRoster.destinationDock;
        playerRoster.destinationDock = null;
        SceneManager.LoadScene("Dock");
    }

    private void CalcSpeedMod()
    {
        if (playerRoster.ship.currentStorage >= playerRoster.ship.maxStorage * 0.71f)
        {
            sailingManager.weightSpeedMod = 0.8f; //speed -20%
        }
        else if (playerRoster.ship.currentStorage >= playerRoster.ship.maxStorage * 0.91f)
        {
            //speed -1% per cargo%
        }
        else sailingManager.weightSpeedMod = 1f;

        foreach (Sailor sailor in playerRoster.sailors)
        {
            sailor.fatigue = sailor.maxFatigue;
        }
    }
}
