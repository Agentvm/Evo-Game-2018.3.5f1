using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Character))]
[RequireComponent (typeof (GrowLeaves))]
public class CollectSunlight : AbilityBaseClass
{
    // References
    GrowLeaves _growLeavesAbility;

    // 
    private float _currentSaturation = 0f;
    float _maxSaturation = 1f;
    float _saturationPerTick; // means game tick

    public float CurrentSaturation { get => _currentSaturation; }

    public bool SubtractSaturation (float amount)
    {
        if ( _currentSaturation >= amount)
        {
            _currentSaturation -= amount;
            return true;
        }

        return false;
    }

    override public void initializeAbility ()
    {
        //// Get References
        _growLeavesAbility = this.GetComponent<GrowLeaves> ();

        // Initialize
        _saturationPerTick = _growLeavesAbility.CurrentLeavesArea;
    }

    override public void Tick ()
    {
        _character.AddEnergy (_saturationPerTick);

        // Upkeep
        _character.SubtractPreservationEnergy (_preservationEnergyPerTick);
    }
}
