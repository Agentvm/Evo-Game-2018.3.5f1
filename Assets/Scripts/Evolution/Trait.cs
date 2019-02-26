using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base Class for genetic Traits and Abilities in Characters (Animals, Plants, etc.)
public class Trait
{
    // variables
    private string name;
    private int length;
    public delegate int IntensityCheck (List<bool> genome_segment );
    public IntensityCheck IntensityStatus;
    //protected int? genome_position;
    //protected int intensity; // describes if trait is active and to which extent it is
    //private Genome genome;

    // Properties
    public string Name
    {
        get { return name; }
        //set { name = value; }
    }

    public int Length
    {
        get { return length; }
        //set { length = value; }
    }

    //public int? GenomePosition
    //{
    //    get { return genome_position; }
    //    set { genome_position = value; }
    //}

    //public int Intensity
    //{
    //    get { return intensity; }
    //    set { intensity = value; }
    //}

    //public Genome Genome { get => genome; set => genome = value; }

    /// <summary>
    /// Checks if the requirements for this trait are fulfilled in the given genome.
    /// </summary>
    /*virtual public int updateRequirementStatus ( List<bool> genome_string )
    {
        if ( genome_string.Count != this.Length ) Debug.LogError ("Trait " + name + " has received a genome of Size " + genome_string.Count + ", while it's Lenght is " + this.Length + ". You better check this.");

        int number_true_results = 0;
        foreach ( bool b in genome_string ) if ( b == true ) number_true_results++; // count the genes of the relevant genome part

        return (int)( number_true_results > this.Length / 2 ); // update the status of the trait
    }*/

    /// <summary>
    /// Constructor. Position in genome is determined when trait is added to a genome.
    /// </summary>
    public Trait ( string trait_name_given, int trait_length, IntensityCheck intensity_function )
    {
        name = trait_name_given;
        length = trait_length;
        IntensityStatus = intensity_function;
        //Intensity = trait_intensity;
    }
}