using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grow : Ability
{

    void Start ()
    {
        ability_name = "";
        script = null;
        traits = new List<Trait> ();
        traits.AddRange (GameCore.instance.GameVariables.SomeAvailableTraits); // do something similar, pick the traits you need
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
