using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitField
{
    private string _name = "";
    float _intensity;
    float _length;

    public string Name { get => _name; }
    public float Intensity { get => _intensity; }
    public float Length { get => _length; }

    public TraitField ( TraitTypes traitType, Character character )
    {
        _name = traitType.ToString ();
        _intensity = character.Genome.GetManifestation (traitType).Intensity;
        _length = character.Genome.GetManifestation (traitType).Length;
    }
}
