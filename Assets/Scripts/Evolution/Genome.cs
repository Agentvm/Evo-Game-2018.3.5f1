using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


// Class that contains "genetic" information
//                   ________________________________________________
// Genome String     |1010100010101011010100110100100000100001000010|
// Genes 1, 0
//
// Genome.Traits > TraitManifestation > ManifestationSegment
public class Genome {
    
    // class 
    private class ManifestationSegment {
        public int position;
        public int length; 

        public ManifestationSegment (int pos, int len )
        {
            position = pos;
            length = len;
        }
    }
    
    // class
    private class TraitManifestation {
        
        private Trait trait;
        private List<ManifestationSegment> segments;
        private int intensity;   
        
        // Properties
        public int Start { get => Segments[0].position; }
        public Trait Trait { get => trait; set => trait = value; }
        private List<ManifestationSegment> Segments { get => segments; set => segments = value; }
        public int Intensity { get => intensity; set => intensity = value; }

        public TraitManifestation (Trait given_trait, int position, int intensity)
        {
            segments = new List<ManifestationSegment> ();
            Trait = given_trait;
            Intensity = intensity;
            Segments.Add (new ManifestationSegment (position, Trait.Length));
        }
        
        public TraitManifestation (Trait given_trait, int position, List<bool> genome_string)
        {
            segments = new List<ManifestationSegment> ();
            Trait = given_trait;
            Segments.Add (new ManifestationSegment (position, Trait.Length ));
            updateIntensityStatus (genome_string );
        }
        
        /*
        public TraitManifestation (Trait given_trait, IDictionary<int, int> positions_and_lengths, List<bool> genome_string)
        {
            Trait = given_trait;
            //Segments.Add (new ManifestationSegment (position, Trait.Length));
            updateIntensityStatus (genome_string );
        }*/
        
        public void updateIntensityStatus (List<bool> genome_string )
        {
            Debug.Log ("hello");

            if (getRelevantGenes (genome_string).Count != Trait.Length )
                Debug.LogError ("Assertion failes. Relevant genes length does not match Lenght of Trait " + Trait.Name + ".");
            Intensity = Trait.IntensityStatus (getRelevantGenes (genome_string ));

            Debug.Log ("Ciao");
        }
        
        private List<bool> getRelevantGenes (List<bool> genome_string )
        {
            List<bool> relevant_genes = new List<bool> ();
            foreach (ManifestationSegment segment in Segments )
            {
                //if (segment.position + segment.length > genome_string.Count ) // segment is out of genome bounds
                //{
                    for (int i = 0; i < segment.length; i++) // progress gene per gene
                    {
                        if (segment.position + i > genome_string.Count) // wrap
                        {
                            relevant_genes.Add (genome_string[segment.position + i - genome_string.Count]); // start again at the beginning of genome
                        }
                        // still in bounds (segment starts inbounds but is too long)
                        else relevant_genes.Add (genome_string[segment.position + i]);
                    }
                //}
                //else relevant_genes.AddRange (genome_string.Range (segment.position, segment.length) );
            }
            return relevant_genes;
        }
        
        
    }

    // Variables
    private List<bool> genome_string;
    private List<TraitManifestation> trait_manifestations = new List<TraitManifestation> ();
    private List<int> past_mutations = new List<int> (); // rather fits in species
    private float mutation_scale = 1.0f;

    
    // Properties
    //public List<TraitManifestation> Trait_Manifestations { get => trait_manifestations; /*set => traits = value;*/ }
    public List<bool> GenomeString { get => genome_string;/* set => genome_string = value;*/ }
    public int Length {get => GenomeString.Count;}
    public List<Trait> Traits { get => getTraits (); }
    public List<int> PastMutations { get => past_mutations;/* set => genome_string = value;*/ }
    public float MutationScale { get => mutation_scale; set => mutation_scale = value; }

    
    /// Constructor
    public Genome (int length = 30)
    {
        // Fill Genome with random values
        genome_string = new List<bool> (30);
        for ( int i = 0; i < length; i++ ) GenomeString.Add (Random.value > 0.5f);
    }    
    
    
    // Functions
    /// <summary>
    /// Update the Intensity of every Trait in this genome (to see if, and to which degree it is active)
    /// </summary>
    public void updateIntensities ()
    {
        foreach (TraitManifestation manifestation in trait_manifestations)
            manifestation.updateIntensityStatus (genome_string );
    }
    
    public void mutate ()
    {
        for (int i = 0; i < genome_string.Count; i++)
        {
            // Flipping bit with Chance of ((1/GenomeLength) * MutationScale) | (Formula is inverted)
            if (Random.Range(1, Mathf.Ceil (genome_string.Count * (1/MutationScale))) == 1 )
            {
                genome_string[i] = !genome_string[i];
                PastMutations.Add (i);
            }
        }
    }
    
    public void addTrait ( Trait trait )
    {
        // if trait is already in this genome, abort
        foreach (TraitManifestation manifestation in trait_manifestations) if (manifestation.Trait.Name == trait.Name) return;
        
        // the trait is added at a random position in the genome
        int pos = Random.Range (0, GenomeString.Count);
        trait_manifestations.Add (new TraitManifestation (trait, pos, genome_string) );
    }
    
    public void addTrait ( Trait trait, int position_in_genome )
    {
        // if trait is already in this genome, abort
        foreach ( TraitManifestation manifestation in trait_manifestations ) if ( manifestation.Trait.Name == trait.Name ) return;

        trait_manifestations.Add (new TraitManifestation (trait, position_in_genome, genome_string ));
    }

    private List<Trait> getTraits ()
    {
        List<Trait> all_traits = new List<Trait> ();
        foreach (TraitManifestation manifestation in trait_manifestations )
        {
            all_traits.Add (manifestation.Trait);
        }

        return all_traits;
    }
    /*
    /// <summary>
    /// 
    /// </summary>
    public void addTrait ( Trait trait, string linked_trait, int link_relative_position = 0 )
    {
        // später
        if ( position_in_genome < 0 ) trait.GenomePosition = Random.Range (0, GenomeString.Count); // genomes are circular
        traits.Add (trait);
    }
    
    /// <summary>
    /// Add a bunch of correlated traits which can't be separated.
    /// </summary>
    /// <param name="traits_and_their_relative_positions">A Dictionary which contains all the traits to be added to the genom alongside their respective positions to eachother. The first integer describes the offset of the whole complex int the genome. Pass -1 for random placement.</param>
    public void addTraitStructure ( IDictionary<Trait, int> traits_and_their_relative_positions )
    {
        // ToDo: each Traits needs its corresponding position
        //if ( traits_and_their_relative_positions. = -1 ) Random.Range (0, genome.Count); // genomes are circular

        foreach ( Trait t in traits_and_their_relative_positions.Keys )
        {
            t.Genome = this;
            traits.Add (t);
        }
    }
    */

}
