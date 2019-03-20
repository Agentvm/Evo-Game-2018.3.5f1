using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

    Class for genetic Traits in Characters (Animals, Plants, etc.).
    Traits determine the constitution of an organism by setting the basis of assessment for a certain feature or property of the life form.
    This is, for example, it's size or the number of it's offspring.

*/
public class Trait
{
    // Variables
    private TraitTypes type;
    private int length; // when applied to a Genome, this is the number of values that are needed to cumpute the intensity of this trait
    public delegate int IntensityCheck (List<bool> genome_segment ); // delegate
    public IntensityCheck IntensityStatus; // stores a reference to a function that can compute this trait's intensity, given a certain Genome.

    // Properties
    public TraitTypes Type { get => type; }
    public string Name { get => type.ToString (); /*set => name = value;*/ }
    public int Length { get => length; /*set => length = value;*/ }


    /// <summary>
    /// Constructor. Position in Genome as well as Intensity is determined when trait is added to a genome.
    /// </summary>
    public Trait ( TraitTypes trait_type, int trait_length, IntensityCheck intensity_function )
    {
        type = trait_type;
        length = trait_length;
        IntensityStatus = intensity_function;
    }
}