using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Character))]
[RequireComponent (typeof (CollectSunlight))]
[RequireComponent (typeof (Grow))]
public class GrowLeaves : AbilityBaseClass
{
    // References
    CollectSunlight _collectSunlightAbility;
    Grow _growAbility;

    // Traits
    TraitField _leavesDensityTrait;
    TraitField _lightRequirementTrait;

    // Features derived from Traits
    float _currentLeavesArea = 0f;
    float _maxLeavesArea;
    float _lightRequirement;
    float _growthPerTick;

    public float CurrentLeavesArea { get => _currentLeavesArea; }

    override public void initializeAbility ()
    {
        // Get References
        _collectSunlightAbility = this.GetComponent<CollectSunlight> ();
        _growAbility = this.GetComponent<Grow> ();

        // Initialize Traits
        _leavesDensityTrait = new TraitField (TraitTypes.LeavesDensity, this._character);
        _lightRequirementTrait = new TraitField (TraitTypes.LightRequirement, this._character);

        // Set initial value for LeavesDensity
        _currentLeavesArea = _leavesDensityTrait.Intensity / 10;
        _maxLeavesArea = _leavesDensityTrait.Intensity;
        _character.SetAlpha (_currentLeavesArea / _maxLeavesArea);

        // Set initial value for LightRequirement
        _lightRequirement = _lightRequirementTrait.Intensity / _lightRequirementTrait.Length;

        // Set inital value for Growth
        _growthPerTick = _growAbility.GrowthPerTick;
    }

    override public void Tick ()
    {
        if ( _collectSunlightAbility.SubtractSaturation (_lightRequirement) )
        {
            _currentLeavesArea = Mathf.Min (_growthPerTick+ _currentLeavesArea, _maxLeavesArea);
            _character.SetAlpha (_currentLeavesArea / _maxLeavesArea);
            Debug.Log ("Current Leaves Area: " + _currentLeavesArea);
        }
    }
}
