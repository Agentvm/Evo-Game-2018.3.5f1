using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Grow))]
[RequireComponent (typeof (GrowLeaves))]
public class ReceiveShadows : AbilityBaseClass
{
    // Components
    Collider2D _collider;

    // Cogs
    Collider2D[] collidingShadows = new Collider2D[20];

    // Values
    float _remainingSunlight = 1f;
    private float _shadowDimishingFactor = 0.8f; // We currently do not detect if the whole plant is shadowed

    // Property
    public float RemainingSunlight { get => _remainingSunlight; }


    private List<float> GetShadowDensities ()
    {
        if ( _collider == null ) return new List<float>();

        // Get all colliding Colliders
        int numberOfCollisions = _collider.OverlapCollider (new ContactFilter2D (), collidingShadows);

        // And step through them
        List<float> shadowDensityList = new List<float>();
        for ( int i = 0; i <= numberOfCollisions; i++)
        {
            // Get the GrowLeaves Script to find the Current LeavesDensity
            GrowLeaves tallerPlantCanopy = collidingShadows[i]?.transform.GetComponent<GrowLeaves> ();
            if ( tallerPlantCanopy != null )
                shadowDensityList.Add (tallerPlantCanopy.CurrentLeavesDensity);
        }

        // Clear Array and resize if needed
        System.Array.Clear (collidingShadows, 0, numberOfCollisions);
        if ( collidingShadows.Length - numberOfCollisions < 10 )
            System.Array.Resize<Collider2D> (ref collidingShadows, numberOfCollisions * 2);

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

        _collider = this.GetComponent<Collider2D> ();
        UpdateShadowStrength ();
    }

    override public void Tick ()
    {
        UpdateShadowStrength ();
        if ( _remainingSunlight != 1.0f)
            Debug.Log ("_remainingSunlight: " + _remainingSunlight);
    }
}
