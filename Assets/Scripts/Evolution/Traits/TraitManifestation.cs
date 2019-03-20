using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// --- Container Class -------------------------------------------------
public class ManifestationSegment
{
    public int position;
    public int length;

    /// <summary>
    /// Constructor takes position and length of the segment
    /// </summary>
    public ManifestationSegment ( int given_position, int given_length )
    {
        position = given_position;
        length = given_length;
    }
}


/*
Contains the position/distribution of the Trait in it's respective Genome. Computes Trait Intensity in this certain Genome. 
 */
public class TraitManifestation
{
    private Trait trait;
    private List<ManifestationSegment> segments; // the Trait can manifest itself in multiple segments. Their length combined matches Trait.Length
    private int intensity; // because Intensity is not to change during the lifetime of a TraitManifestation, it is an attribute here

    // Properties
    public float Intensity { get => (float)intensity; } // expose trait Attributes so these can be easily read by Ability Scripts
    public float Length { get => (float)trait.Length; }
    public string Name { get => trait.Name; }
    public List<ManifestationSegment> Segments { get => segments;}

    
    /// <summary>
    /// Construct a single segment manifestation, assigning an Intensity.
    /// </summary>
    public TraitManifestation ( Trait given_trait, int position, int given_intensity )
    {
        segments = new List<ManifestationSegment> ();
        trait = given_trait;
        segments.Add (new ManifestationSegment (position, trait.Length)); // splitting of segments possible
        intensity = given_intensity;
    }
    

    /// <summary>
    /// Construct a single segment manifestation by giving a genome_string. It is used to compute Intensity.
    /// </summary>
    public TraitManifestation ( Trait given_trait, int position, List<bool> genome_string )
    {
        segments = new List<ManifestationSegment> ();
        trait = given_trait;
        segments.Add (new ManifestationSegment (position, trait.Length)); // splitting of segments possible
        updateIntensityStatus (genome_string );
    }

    /// <summary>
    /// Construct a single segment manifestation by giving a genome_string. It is used to compute Intensity.
    /// </summary>
    public TraitManifestation ( Trait given_trait, Dictionary<int, int> positions_and_lengths, List<bool> genome_string )
    {
        // length check
        int overall_length = 0;
        foreach ( int length in positions_and_lengths.Values ) overall_length += length;
        if ( overall_length != given_trait.Length ) return;

        segments = new List<ManifestationSegment> ();
        trait = given_trait;

        // generate Segments
        foreach (int position_key in positions_and_lengths.Keys)
        {
            segments.Add (new ManifestationSegment (position_key, positions_and_lengths[position_key])); // splitting of segments
        }
        
        updateIntensityStatus (genome_string);
    }


    // Draft for multiple Segments
    /*
    public TraitManifestation (Trait given_trait, Dictionary<int, int> positions_and_lengths, List<bool> genome_string)
    {
        Trait = given_trait;
        //Segments.Add (new ManifestationSegment (position, Trait.Length));
        updateIntensityStatus (genome_string );
    }*/


    /// <summary>
    /// Call the Intensity Function of the Trait, setting the Intensity attribute.
    /// </summary>
    public void updateIntensityStatus ( List<bool> genome_string )
    {
        //Debug.Log ("Getting relevant genes for Trait " + Trait.Name);
        intensity = trait.IntensityStatus (getRelevantGenes (genome_string));
    }


    /// <summary>
    /// Given a GenomeString and all ManifestationSegments of this TraitManifestation, extract the values that are indicated by the segments.
    /// </summary>
    private List<bool> getRelevantGenes ( List<bool> genome_string ) // should this take a Genome ?
    {
        List<bool> relevant_genes = new List<bool> ();
        foreach ( ManifestationSegment segment in segments )
        {

            for ( int i = 0; i < segment.length; i++ ) // progress gene per gene (bool per bool)
            {

                if ( segment.position + i >= genome_string.Count ) // wrap
                {
                    //Debug.Log ("wrapping, Add genome_string[" + segment.position + " + " + i + " - " + genome_string.Count + "]");
                    relevant_genes.Add (genome_string[segment.position + i - genome_string.Count]); // start again at the beginning of genome
                }
                else // still in bounds
                {
                    //Debug.Log ("in bounds, Add genome_string[" + segment.position + " + " + i + "]");
                    relevant_genes.Add (genome_string[segment.position + i]);
                }
            }

        }

        if ( relevant_genes.Count != Length )
            Debug.LogError ("Assertion failed. Relevant genes length does not match Lenght of Trait " + Name + ".");
        return relevant_genes;
    }

}
