using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://docs.unity3d.com/ScriptReference/RequireComponent.html
[RequireComponent(typeof(Character))]
public class Grow : AbilityBaseClass
{
    private Character character;
    private int max_size_intensity;
    private int grow_rate_intensity;

    // quasi - enum to enumerate traits for quick look up
    //private Trait MaxSize { get => Traits[0]; }
    //private Trait GrowRate { get => Traits[1]; }



    private int max_age = 10;

    // Start is called before the first frame update
    void Start()
    {
        //traits = new List<Trait> ();
        Traits.AddRange (GameCore.Instance.GameVariables.getTraits (new List<string> () { "MaxSize", "GrowRate" } ));
        //max_size_intensity = Traits[0].IntensityStatus (character.Genome.GenomeString); // initialize character zero via GameVariables (FindGameObjectsWithTag)
        character = GetComponent<Character> ();
    }

    public void Tick ()
    {
        /*if ( max_size_intensity > 8 )
        {
            float scalar = max_size_intensity / (max_age / 4f);
            transform.localScale.Scale (new Vector3 (1f, 1f, 1f));
        }*/
    }
}
