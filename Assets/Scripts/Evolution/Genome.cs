using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// Class that contains "genetic" information
//                   ________________________________________________
// Genome String     |1010100010101011010100110100100000100001000010|
// Genes 1, 0
//
// Genome.Traits > TraitManifestation > ManifestationSegment
public class Genome {
    
    // Variables
    private List<bool> genome_string;
    private List<TraitManifestation> trait_manifestations = new List<TraitManifestation> ();
    //private List<int> past_mutations = new List<int> (); // rather fits in species
    private float mutation_scale = 1.0f; // how likely is this genome altered when mutating

    
    // Properties
    public List<bool> GenomeString { get => genome_string;/* set => genome_string = value;*/ }
    public int Length {get => GenomeString.Count;}
    //public List<Trait> Traits { get => getTraits (); } // currently not necessary
    //public List<int> PastMutations { get => past_mutations;/* set => genome_string = value;*/ }
    public float MutationScale { get => mutation_scale; set => mutation_scale = value; }


    /// <summary>
    /// Constructor for a randomized genome.
    /// </summary>
    public Genome (int length = 30)
    {
        // Fill Genome with random values
        genome_string = new List<bool> (30);
        for ( int i = 0; i < length; i++ ) genome_string.Add (Random.value > 0.5f);
    }    
    
    
    // Functions

    /// <summary>
    /// Update the Intensity of every Trait in this genome (to see if, and to which degree it is active)
    /// </summary>
    public void updateIntensities ()
    {
        foreach ( TraitManifestation manifestation in trait_manifestations )
        {
            manifestation.updateIntensityStatus (genome_string);
        }
    }

    
    /// <summary>
    /// Alters the genome_string. This is set to occur before birth. 
    /// </summary>
    public void mutate ()
    {
        for (int i = 0; i < genome_string.Count; i++)
        {
            // Flipping bit with Chance of ((1/GenomeLength) * MutationScale) | (Formula is inverted)
            if (Random.Range(1, Mathf.Ceil (genome_string.Count * (1/MutationScale))) == 1 )
            {
                genome_string[i] = !genome_string[i];
                //if (!PastMutations.Contains (i))
                //    PastMutations.Add (i);
            }
        }
    }
    

    /// <summary>
    /// Add a Trait to this Genome.
    /// </summary>
    public void manifestTrait ( Trait trait )
    {
        // if trait is already in this genome, abort
        foreach (TraitManifestation manifestation in trait_manifestations) if (manifestation.Name == trait.Name) return;
        
        // the trait is added at a random position in the genome
        int pos = Random.Range (0, GenomeString.Count);
        trait_manifestations.Add (new TraitManifestation (trait, pos, genome_string) );
    }


    /// <summary>
    /// Add a Trait to this Genome at a certain position.
    /// </summary>
    public void manifestTrait ( Trait trait, int position_in_genome )
    {
        // if trait is already in this genome, abort
        foreach ( TraitManifestation manifestation in trait_manifestations ) if ( manifestation.Name == trait.Name ) return;

        trait_manifestations.Add (new TraitManifestation (trait, position_in_genome, genome_string ));
    }

    /* Simple overlapping
    public void manifestInterwovenTrait ( Trait new_trait, TraitTypes type_of_trait_in_genome, int number_of_overlapping_digits )
    {
        // checks
        if ( GetManifestation (new_trait.Type) != null ) return; // check if the trait to be added is already manifested
        TraitManifestation trait_in_genome = GetManifestation (type_of_trait_in_genome ); // get the manifested trait that the new one should overlap with
        if ( trait_in_genome == null ) return;

        // compute the offset with length of TraitManifestation in genome and overlapping length
        int offset = (trait_in_genome.Segments[0].length - number_of_overlapping_digits);
        if ( Random.value > 0.5f ) offset *= -1; // flip the value to overlap the first or last digits

        // add the new Trait at a position relative to the specified trait already in genome
        int position = trait_in_genome.Segments[0].position + offset;
        trait_manifestations.Add (new TraitManifestation (new_trait, position, genome_string ));
    }*/

    //Draft for interwoven Trait addition on segmented traits
    public void manifestInterwovenTrait (Trait new_trait, TraitTypes type_of_trait_in_genome, int number_of_overlapping_digits )
    {
        // checks
        if ( GetManifestation (new_trait.Type) != null ) return; // check if the trait to be added is already manifested
        TraitManifestation trait_manifested_in_genome = GetManifestation (type_of_trait_in_genome ); // get the manifested trait that the new one should overlap with
        if ( trait_manifested_in_genome == null ) return;

        // get a dict<int, int> that shows the positions and lengths of overlapping space
        Dictionary<int, int> overlapping_segments = designateOverlappingSegments (trait_manifested_in_genome.Segments, number_of_overlapping_digits );

        if ( overlapping_segments.Count == 0 ) return;
        else if ( overlapping_segments.Count == 1 ) // just one segment means that the trait is coherent, e.g. at one stretch
        {
            // Find the position before the begin of the overlapping space, so that the number ob overlapping digits is overlapping_segments[0].
            int position = overlapping_segments.ElementAt(0).Key - (new_trait.Length - overlapping_segments[0] );
            trait_manifestations.Add (new TraitManifestation (new_trait, position, genome_string));
        }
        /*else
        {
            // take all determined overlapping segments
            Dictionary<int, int> new_trait_manifestation_space = new Dictionary<int, int> (overlapping_segments);

            // find more segments that do not overlap

            // scatter random segments in genome
        }*/
    }

    private int intDictOverallLength (Dictionary<int, int> dict)
    {
        int length = 0;
        foreach ( int key in dict.Keys ) length += dict[key];
        return length;
    }

    private Dictionary<int, int> inverseGenome (Dictionary <int, int> segments )
    {
        // add one segment of the length of this Genome string
        Dictionary<int, int> inverse_segments = new Dictionary<int, int> ();
        //inverse_segments.Add (0, this.genome_string.Count);

        // remove the segments given from inverse_segments, splitting it into several new segments 
        foreach (int key in segments.Keys)
        {
            // check for overlappings
            // if the start or end points of a range in inverse_genome are between the currently processed segments' end and begin points
            // or if the sements' start (and end) points are in the range of any inverse_genome range
        }

        return inverse_segments;
    }

    bool isInRange (Dictionary<int, int> basis, KeyValuePair<int, int> element)
    {
        foreach (KeyValuePair<int, int> basis_pair in basis)
        {

        }


        return false;
    }

    private Dictionary<int, int> designateOverlappingSegments ( Dictionary<int, int> dict, int length )
    {
        Dictionary<int, int> returned_segments = new Dictionary<int, int> ();

        while ( length > 0 && dict.Count > 0 )
        {
            // pick a random key, then remove it from dictionary
            int key = dict.ElementAt (Random.Range (0, dict.Count)).Key;
            int value = dict[key];
            dict.Remove (key);

            if ( length > value ) // length is the number of digits that overlap and is here compared to the length of the next segment
            {
                returned_segments.Add (key, value);
                length -= value; // length is diminished by segment length
            }
            else if ( length == value )
            {
                returned_segments.Add (key, value);
                length -= value; // length is diminished by segment length
                return returned_segments;
            }
            else if ( length < value ) // the chosen segment is longer than it needs to be, only a part of it should be designated "overlapping"
            {
                returned_segments.Add (key, length);
                length = 0; // length is diminished to zero
                return returned_segments;
            }
        }

        Debug.LogWarning ("There are too few segments to overlap.");
        return returned_segments;
    }

    public TraitManifestation GetManifestation (TraitTypes requested_trait_type)
    {
        bool found = false;
        foreach ( TraitManifestation manifestation in this.trait_manifestations )
        {
            // TraitManifestations obviously have the same name as the Trait they correspond to
            if ( requested_trait_type.ToString () == manifestation.Name )
                return manifestation;
        }

        if ( !found ) Debug.LogWarning ("The Trait " + requested_trait_type.ToString () + " is not manifested in " + this.GetType ().ToString () + ".");
        return null;
    }

    /*
    /// <summary>
    /// Returns a Dictionary of Names and respective TraitManifestations. With these, all trait Attributes are accessible (Intensity, Lenght and Name of the Trait)
    /// </summary>
    public Dictionary<string, TraitManifestation> getTraitManifestations ( List<Trait> requested_traits )
    {
        Dictionary<string, TraitManifestation> trait_names_and_manifestations = new Dictionary<string, TraitManifestation> { };

        foreach ( Trait requested_trait in requested_traits )
        {
            bool found = false;
            foreach ( TraitManifestation manifestation in trait_manifestations )
            {
                if ( manifestation.Name == requested_trait.Name )
                {
                    trait_names_and_manifestations.Add (manifestation.Name, manifestation);
                    found = true;
                    break; // the inner foreach
                }
            }

            if ( !found ) Debug.LogWarning ("The Trait " + requested_trait.Name + " could not be found in " + this.GetType ().ToString () + ".");
        }

        return trait_names_and_manifestations;
    }

    */
    /*
    /// <summary>
    /// Adds a Trait relative to another Trait that has aready been added. The latter is specified by string.
    /// </summary>
    public void addTrait ( Trait trait, string linked_trait, int link_relative_position = 0 )
    {
        // später
        if ( position_in_genome < 0 ) trait.GenomePosition = Random.Range (0, GenomeString.Count); // genomes are circular
        traits.Add (trait);
    }
    
    /// <summary>
    /// Add a bunch of correlated traits which can't be separated. These should depend on eachother like rate of growth and need for nutrition.
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


    // currently not necessary
    //private List<Trait> getTraits ()
    //{
    //    List<Trait> all_traits = new List<Trait> ();
    //    foreach ( TraitManifestation manifestation in trait_manifestations )
    //    {
    //        all_traits.Add (manifestation.Trait);
    //    }

    //    return all_traits;
    //}

    // currently not necessary
    //public Dictionary<string, int> getTraitIntensities ( List<string> requested_trait_names_raw )
    //{
    //    List<string> requested_trait_names = requested_trait_names_raw.Distinct ().ToList (); // remove duplicates

    //    Dictionary<string, int> trait_names_and_intensities = new Dictionary<string, int> { };
    //    foreach ( string requested_trait_name in requested_trait_names )
    //    {
    //        bool found = false;
    //        foreach ( TraitManifestation manifestation in trait_manifestations )
    //        {
    //            if ( manifestation.Name == requested_trait_name )
    //            {
    //                trait_names_and_intensities.Add (manifestation.Name, manifestation.Intensity);
    //                found = true;
    //                break; // the inner foreach
    //            }
    //        }

    //        if ( !found ) Debug.LogWarning ("The Trait " + requested_trait_name + " could not be found in " + this.GetType ().ToString () + ".");
    //    }

    //    return trait_names_and_intensities;
    //}
}
