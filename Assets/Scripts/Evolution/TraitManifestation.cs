using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitManifestation
{
    // Private Container Class
    private class ManifestationSegment
    {
        public int position;
        public int length;

        public ManifestationSegment ( int pos, int len )
        {
            position = pos;
            length = len;
        }
    }

   // --- Begin of Class ---------------------------------------------------
    private Trait trait;
    private List<ManifestationSegment> segments;
    private int intensity;

    // Properties
    public int Start { get => Segments[0].position; }
    public Trait Trait { get => trait; set => trait = value; }
    private List<ManifestationSegment> Segments { get => segments; set => segments = value; }
    public int Intensity { get => intensity; set => intensity = value; }

    public TraitManifestation ( Trait given_trait, int position, int intensity )
    {
        segments = new List<ManifestationSegment> ();
        Trait = given_trait;
        Intensity = intensity;
        Segments.Add (new ManifestationSegment (position, Trait.Length));
    }

    public TraitManifestation ( Trait given_trait, int position, List<bool> genome_string )
    {
        segments = new List<ManifestationSegment> ();
        Trait = given_trait;
        Segments.Add (new ManifestationSegment (position, Trait.Length));
        updateIntensityStatus (genome_string);
    }

    /*
    public TraitManifestation (Trait given_trait, IDictionary<int, int> positions_and_lengths, List<bool> genome_string)
    {
        Trait = given_trait;
        //Segments.Add (new ManifestationSegment (position, Trait.Length));
        updateIntensityStatus (genome_string );
    }*/

    public void updateIntensityStatus ( List<bool> genome_string )
    {
        //Debug.Log ("Getting relevant genes for Trait " + Trait.Name);
        Intensity = Trait.IntensityStatus (getRelevantGenes (genome_string));
    }

    private List<bool> getRelevantGenes ( List<bool> genome_string )
    {
        List<bool> relevant_genes = new List<bool> ();
        foreach ( ManifestationSegment segment in Segments )
        {
            //Debug.Log ("genome_string.Count: " + genome_string.Count );
            //Debug.Log ("segment.length: " + segment.length);
            //Debug.Log ("segment.pos: " + segment.position );

            //if (segment.position + segment.length > genome_string.Count ) // segment is out of genome bounds
            //{
            for ( int i = 0; i < segment.length; i++ ) // progress gene per gene
            {
                //Debug.Log ("i: " + i );
                //Debug.Log ("if (" + segment.position + " + "+i+" > "+genome_string.Count+") ");
                if ( segment.position + i >= genome_string.Count ) // wrap
                {
                    //Debug.Log ("wrapping, Add genome_string[" + segment.position + " + " + i + " - " + genome_string.Count + "]");
                    relevant_genes.Add (genome_string[segment.position + i - genome_string.Count]); // start again at the beginning of genome
                }
                // still in bounds (segment starts inbounds but is too long)
                else
                {
                    //Debug.Log ("in bounds, Add genome_string[" + segment.position + " + " + i + "]");
                    relevant_genes.Add (genome_string[segment.position + i]);
                }
            }

            //}
            //else relevant_genes.AddRange (genome_string.Range (segment.position, segment.length) );
        }

        if ( relevant_genes.Count != Trait.Length )
            Debug.LogError ("Assertion failed. Relevant genes length does not match Lenght of Trait " + Trait.Name + ".");
        return relevant_genes;
    }

}
