using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Character))]
[RequireComponent (typeof (CollectSunlight))]
public class GrowLeaves : AbilityBaseClass
{
    // References
    CollectSunlight _collectSunlightAbility;

    // Traits
    TraitField _growRate;
    TraitField _leavesDensityTrait;
    TraitField _lightRequirementTrait;

    // Features derived from Traits
    float _currentLeavesArea = 0f;
    float _lightRequirement;

    public float CurrentLeavesArea { get => _currentLeavesArea; }

    override public void initializeAbility ()
    {
        // Get References
        _collectSunlightAbility = this.GetComponent<CollectSunlight> ();

        // Initialize Traits
        _growRate = new TraitField (TraitTypes.GrowRate, this._character);
        _leavesDensityTrait = new TraitField (TraitTypes.LeavesDensity, this._character);
        _lightRequirementTrait = new TraitField (TraitTypes.LightRequirement, this._character);

        // Set initial value for LeavesDensity
        _currentLeavesArea = _leavesDensityTrait.Intensity / 10;

        // Set initial value for LightRequirement
        _lightRequirement = _lightRequirementTrait.Intensity / _lightRequirementTrait.Length;
    }

    override public void Tick ()
    {
        if ( _collectSunlightAbility.SubtractSaturation (_lightRequirement) )
        {
            // 
            //_growRate
            /*
             Rather than computing
             
                min rate = 400^1 / 400^2 = 0,0025 --> 100 years to full growth | max rate = 400^2 / 400^2 = 1 --> 1 Season to full growth | mean rate = 400^1.5 / 400^2 = 0.05 --> 5 Years to full growth
            
             again (just like in the Grow Ability), shouldnt this be handled by the trait itself?
             */
        }
    }
}
