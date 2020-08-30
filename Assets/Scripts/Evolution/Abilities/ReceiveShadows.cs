using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Grow))]
[RequireComponent (typeof (GrowLeaves))]
public class ReceiveShadows : AbilityBaseClass
{
    Ray _skyRay = default;
    float _maxRaycastLenght = 1.0f;
    float _remainingSunlight = 1f;
    private float _shadowDimishingFactor = 0.8f; // We currently do not detect if the whole plant is shadowed

    // Properties
    private Ray SkyRay
    {
        get
        {
            // Fill the Ray at the first time of use
            if ( _skyRay.Equals (default) )
                _skyRay = new Ray (this.transform.position, -Vector3.forward);

            return _skyRay;
        }
    }

    public float RemainingSunlight { get => _remainingSunlight; }


    private List<float> GetShadowDensities ()
    {
        List<float> shadowDensityList = new List<float>();

        // Send a Raycast upwards to find all Plants that are shadowing this one
        foreach ( RaycastHit raycastHitInfo in Physics.RaycastAll (SkyRay, _maxRaycastLenght) )
        {
            GrowLeaves tallerPlantCanopy = raycastHitInfo.transform.GetComponent<GrowLeaves> ();
            if ( tallerPlantCanopy != null )
                shadowDensityList.Add (tallerPlantCanopy.CurrentLeavesArea);
        }

        return shadowDensityList;
    }

    private void UpdateShadowStrength ()
    {
        // Calculate the combined strength of shadows that have an effect on this plant
        _remainingSunlight = 1f;
        foreach ( float shadow in GetShadowDensities () )
            _remainingSunlight *= (1 - shadow * _shadowDimishingFactor);
    }

    public override void InitializeAbility ()
    {
        base.InitializeAbility ();
        UpdateShadowStrength ();
    }

    override public void Tick ()
    {
        UpdateShadowStrength ();
        if ( _remainingSunlight != 1.0f)
            Debug.Log ("_remainingSunlight: " + _remainingSunlight);
    }
}
