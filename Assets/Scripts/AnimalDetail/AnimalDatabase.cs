using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Animal Database")]
public class AnimalDatabase : ScriptableObject
{
    public List<AnimalData> animals;
}