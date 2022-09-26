using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SailingManager")]
public class SailingManager : ScriptableObject
{
    public float weightSpeedMod;
    public float sailingMod;
    public bool onNav = false;
    public float bilgingMod;
    public float carpentingMod;
    public float gunningMod;
    public float speed = 1f;
    public float sailingModBalancer;

    private void OnEnable()
    {
        ResetMods();
    }

    public float SpeedFactor => ((sailingMod * sailingModBalancer) + speed) * weightSpeedMod;

    public void ResetMods()
    {
        weightSpeedMod = 0f;
        sailingMod = 0f;
        onNav = false;
        bilgingMod = 0f;
        carpentingMod = 0f;
        gunningMod = 0f;
    }
}
