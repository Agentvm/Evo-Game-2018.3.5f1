using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Character))]
[RequireComponent (typeof (GrowLeaves))]
public class CollectSunlight : AbilityBaseClass
{
    // References
    GrowLeaves _growLeavesAbility;

    // Traits
    TraitField _lightRequirementTrait;

    // Values
    float _saturationPerTick; // means game tick
    float _preservationEnergyPerTick;

    override public void InitializeAbility ()
    {
        // Get References
        _growLeavesAbility = this.GetComponent<GrowLeaves> ();

        // Get Traits
        _lightRequirementTrait = new TraitField (TraitTypes.LightRequirement, _character);

        // Initialize
        _saturationPerTick = _growLeavesAbility.CurrentLeavesArea;
        _preservationEnergyPerTick = _lightRequirementTrait.Intensity / _lightRequirementTrait.Length / 10;        
    }

    override public void Tick ()
    {
        _character.AddEnergy (_saturationPerTick);

        // Upkeep
        _character.SubtractPreservationEnergy (_preservationEnergyPerTick);
    }
}
