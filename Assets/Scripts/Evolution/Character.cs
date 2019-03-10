using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class for animals, plants, etc that are subject to evolution
[RequireComponent (typeof (SpriteRenderer))]
public class Character : MonoBehaviour {

    private Genome genome;
    private SpriteRenderer sprite;
    //private float size;

    // Properties
    public Genome Genome { get => genome; }
    public List<Trait> Traits { get => genome.Traits; }
    //public float Size { get => size;}
    //public Color Color { get => color; }

    private void Start () // called after Constructor
    {
        // 1. obj.Character --> what? nope.
        // 2. like in SpaceGothicNavigation --> works
        // 3. All in Start and with function
        
        sprite = GetComponent<SpriteRenderer> ();
        setColorbyGenome ();
    }

    /*public void addTrait ( Trait new_trait )
    {
        if ( new_trait != null )
        {
            Genome.addTrait (new_trait); // random trait positions in genome
        }
    }

    public void addTraits ( List<Trait> new_traits )
    {
        if ( new_traits != null )
        {
            foreach ( Trait new_trait in new_traits )
                Genome.addTrait (new_trait); // random trait positions in genome
        }
    }*/


    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="mutated_parent_genome"></param>
    /// <param name="new_traits"></param>
    public void initialize ( Genome mutated_parent_genome, List<Trait> new_traits = null )
    {
        genome = mutated_parent_genome;
        // add traits and update their intensities
        if (new_traits != null)
        {
            foreach (Trait new_trait in new_traits)
                Genome.addTrait (new_trait); // random trait positions in genome
        }        
        //genome.updateIntensities (); // check if Traits are currently active (is done implicitly)
        

        // add abilities (request list of abilities for this Constructor?)
        //foreach ( Trait trait in genome.Traits )
        //{
        //    // Add Behaviour Script to gameobject
        //    //System.Type ability_type = System.Type.GetType(trait.Name );
        //    //gameObject.AddComponent (ability_type);
        //    // good to know
        //    // ((MySpellScript)GetComponent(mType)).Fire();
        //}

        // Start will be called next
    }

    void setColorbyGenome ()
    {
        if (genome.GenomeString.Count < 24) // vermin
        {
            sprite.color = new Color (0.1f, 0.1f, 0.1f, 0.6f);

        }
        else if (genome.GenomeString.Count < 32)
        {
            float r = binaryToDecimal (genome.GenomeString.GetRange (0, 8) ) / 256;
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

    //int binaryToDecimal ( List<bool> list )
    //{
    //    int iteration = 0;
    //    float result = 0;

    //    foreach ( bool boolean in list )
    //    {
    //        result += (System.Convert.ToInt32 (boolean) * Mathf.Pow (2, iteration)); // convert bits to decimal
    //        iteration++;
    //    }

    //    return (int)result;
    //}

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
