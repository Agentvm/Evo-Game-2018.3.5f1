using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for animals, plants, etc that are subject to evolution
[RequireComponent (typeof (SpriteRenderer))]
public class Character : MonoBehaviour {

    private Genome genome;
    private SpriteRenderer sprite;
    private bool traits_initialized = false;
    private List<AbilityBaseClass> abilities = new List<AbilityBaseClass> ();
    //private float size;

    // Properties
    public Genome Genome { get => genome; }
    //public List<Trait> Traits { get => genome.Traits; } // currently not necessary
    public bool TraitsInitialized { get => traits_initialized; } // Are a genom and all necessary traits attatched?

    //public float Size { get => size;}
    //public Color Color { get => color; }

    private void Start () // called after Constructor
    {
        sprite = GetComponent<SpriteRenderer> (); // get reference to sprite so you can change it's color
        setColorbyGenome ();
    }


    /// <summary>
    /// Constructor. Traits are only added here.
    /// </summary>
    /// <param name="mutated_parent_genome"></param>
    /// <param name="new_traits"></param>
    public void Birth ( Genome mutated_parent_genome, List<TraitTypes> new_traits, List<AbilityTypes> new_abilities )
    {
        genome = mutated_parent_genome;

        // add Traits to genome
        if (new_traits != null)
        {
            foreach (TraitTypes type in new_traits)
                // This will cause trouble: The genome is slowly mutated, but trait positions are random? This will cause random trait manifestiations
                genome.manifestTrait (TraitData.getTrait (type )); // random Trait positions in genome
        }
        traits_initialized = true; // set this bool, so Abilities can initialize with traits


        // add abilities (request list of abilities for this Constructor?)
        foreach (AbilityTypes type in new_abilities )
        {
            gameObject.AddComponent (AbilityData.getAbilityType (type ));
        }
        //foreach ( Trait trait in genome.Traits )
        //{
        //    // Add Behaviour Script to gameobject
        //    //System.Type ability_type = System.Type.GetType(trait.Name );
        //    //gameObject.AddComponent (ability_type);
        //    // good to know
        //    // ((MySpellScript)GetComponent(mType)).Fire();
        //}


    }

    // maybe make this function scale to represent the whole string? (scaling is bad, because in binary code, the first digit will get extreme impact)
    /// <summary>
    /// Changes the color of the sprite in respect to the first 32 digits of the GenomeString.
    /// Binary to Decimal conversion of respectively 8 digits (boolean values in genome_string) is used.
    /// </summary>
    void setColorbyGenome ()
    {
        if (genome.GenomeString.Count < 24) // vermin
        {
            sprite.color = new Color (0.1f, 0.1f, 0.1f, 0.6f);

        }
        else if (genome.GenomeString.Count < 32)
        {
            float r = binaryToDecimal (genome.GenomeString.GetRange (0, 8) ) / 256; // convert the first 8 digits to a decimal number, which is then normalized to match the 0 - 1f range
            float g = binaryToDecimal (genome.GenomeString.GetRange (8, 8) ) / 256;
            float b = binaryToDecimal (genome.GenomeString.GetRange (16, 8) ) / 256;
            float a = 0.8f;

            sprite.color = new Color (r, g, b, a);
        }
        else
        {
            float r = binaryToDecimal (genome.GenomeString.GetRange (0, 8) ) / 256;
            float g = binaryToDecimal (genome.GenomeString.GetRange (8, 8) ) / 256;
            float b = binaryToDecimal (genome.GenomeString.GetRange (16, 8) ) / 256;
            float a = Mathf.Min (binaryToDecimal (genome.GenomeString.GetRange (24, 8) ) / 256, 0.5f);

            sprite.color = new Color (r, g, b, a );
        }
        
        //sprite.color = color;
    }


    /// <summary>
    /// Does what is says, but uses a List<Bool> as input.
    /// </summary>
    float binaryToDecimal ( List<bool> list )
    {
        int iteration = 0;
        float result = 0;

        foreach ( bool boolean in list )
        {
            result += (System.Convert.ToInt32 (boolean) * Mathf.Pow (2, iteration)); // convert bits to decimal
            iteration++;
        }

        return result;
    }

}
