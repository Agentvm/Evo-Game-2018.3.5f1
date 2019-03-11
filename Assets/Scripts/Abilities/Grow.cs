using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class Grow : AbilityBaseClass
{

    // quasi - enum to enumerate traits for quick look up
    //private Trait MaxSize { get => Traits[0]; }
    //private Trait GrowRate { get => Traits[1]; }



    private int max_age = 10;

    // Start is called before the first frame update
    override public void Start()
    {
        base.Start (); // character = GetComponent<Character> ();

        //

        
        

        

        //Intensity = character.Genome.getTraitIntensities (new List<string> () { "MaxSize", "GrowRate" });

        //max_size_intensity = Traits[0].IntensityStatus (character.Genome.GenomeString); // initialize character zero via GameVariables (FindGameObjectsWithTag)

    }

    public void Update ()
    {
        StartCoroutine (waitForInitialization ());
    }

    private IEnumerator waitForInitialization ()
    {
        //https://answers.unity.com/questions/304394/have-a-function-to-wait-until-true.html
        //yield return new WaitUntil (() => character.Initialized == true); // Why would you make it understandable while you can use some obscure lambda expression?
        yield return new WaitUntil (() => character.Initialized == true);
        //while ( !character.Initialized )

        We never arrive here!

        Debug.Log ("Wait is Over.");

        TraitManifestation = character.Genome.getTraitManifestations (new List<string> () { "MaxSize", "GrowRate" });

        foreach ( TraitManifestation t in TraitManifestation.Values )
        {
            Debug.Log ("Trait: " + t.Name);
        }
    }

    public void Tick ()
    {
        /*if ( Intensity["MaxSize"] > 8 )
        {
            // Mathf.Pow(2,
            float scalar = Intensity["MaxSize"] / MaxSize.Length // / (max_age / 3f);
            transform.localScale.Scale (new Vector3 (1f, 1f, 1f));
        }*/
    }
}
