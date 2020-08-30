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
    float _currentLeavesDensity;
    float _maxLeavesDensity;
    float _lightRequirement;
    float _growthPerTick;

    public float CurrentLeavesArea { get => _currentLeavesArea; }
    public float CurrentLeavesDensity { get => _currentLeavesDensity;}

    override public void InitializeAbility ()
    {
        base.InitializeAbility ();

        // Get References
        _collectSunlightAbility = this.GetComponent<CollectSunlight> ();
        _growAbility = this.GetComponent<Grow> ();

        // Initialize Traits
        _leavesDensityTrait = new TraitField (TraitTypes.LeavesDensity, this._character);
        _lightRequirementTrait = new TraitField (TraitTypes.LightRequirement, this._character);

        // Set initial value for LeavesDensity
        _currentLeavesDensity = 0.1f;
        _maxLeavesDensity = 0.9f;
        _currentLeavesArea = _leavesDensityTrait.Intensity / 10;
        _maxLeavesArea = _leavesDensityTrait.Intensity;
        _character.SetAlpha (_currentLeavesArea / _maxLeavesArea);

        // Set initial value for LightRequirement and Growth
        _growthPerTick = _growAbility.GrowthPerTick;
        _lightRequirement = _lightRequirementTrait.Intensity / _lightRequirementTrait.MaxIntensity;
        _lightRequirement *= _growthPerTick;
    }

    override public void Tick ()
    {
        // Grow Leaves
        if ( (_currentLeavesArea < _maxLeavesArea || _currentLeavesDensity < _maxLeavesDensity) && _character.SubtractEnergy (_lightRequirement) )
        {
            // Increase Leaves Area and show changes on GameObject
            _currentLeavesArea = Mathf.Min (_growthPerTick + _currentLeavesArea, _maxLeavesArea);
            SetSize ();

            // Increase Leaves Density and show changes on GameObject
            _currentLeavesDensity = Mathf.Min (0.1f + _currentLeavesDensity, _maxLeavesDensity);
            SetAlpha ();
        }
    }

    // Scale the GameObject to the current size, if size is not setaccording to GrowLeaves.CurrentLeavesArea
    void SetSize ()
    {
        _character?.SetSize (_currentLeavesArea);
    }

    void SetAlpha ()
    {
        _character?.SetAlpha (_currentLeavesDensity / _maxLeavesDensity);
    }
}
