using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Character))]
[RequireComponent (typeof (GrowLeaves))]
[RequireComponent (typeof (ReceiveShadows))]
public class CollectSunlight : AbilityBaseClass
{
    // References
    GrowLeaves _growLeavesAbility;
    ReceiveShadows _receiveShadowsAbility;

    // Traits
    TraitField _lightRequirementTrait;

    // Values
    float _saturationPerTick; // means game tick
    float _preservationEnergyPerTick;

    public float PreservationEnergyPerTick { get => _preservationEnergyPerTick; }

    override public void InitializeAbility ()
    {
        base.InitializeAbility ();

        // Get References
        _growLeavesAbility = this.GetComponent<GrowLeaves> ();
        _receiveShadowsAbility = this.GetComponent<ReceiveShadows> ();

        // Get Traits
        _lightRequirementTrait = new TraitField (TraitTypes.LightRequirement, _character);

        // Initialize
        _saturationPerTick = _growLeavesAbility.CurrentLeavesArea * _growLeavesAbility.CurrentLeavesDensity * _receiveShadowsAbility.RemainingSunlight;
        _character.PreservationEnergyPerTick = _lightRequirementTrait.Intensity / _lightRequirementTrait.MaxIntensity / 10;
    }

    override public void Tick ()
    {
        _saturationPerTick = _growLeavesAbility.CurrentLeavesArea * _growLeavesAbility.CurrentLeavesDensity * _receiveShadowsAbility.RemainingSunlight;
        _character.AddEnergy (_saturationPerTick);
    }
}
