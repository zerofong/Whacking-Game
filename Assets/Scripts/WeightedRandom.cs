using UnityEngine;
using System;
using System.Collections.Generic;

public class WeightedRandom: MonoBehaviour
{
    private List<int> weightedIndices;
    private System.Random random;

    private void Start()
    {
        weightedIndices = new List<int>();
        random = new System.Random();
    }
    public void AddWeightedItem(int weight)
    {
        for (int i = 0; i < weight; i++)
        {
            weightedIndices.Add(weightedIndices.Count);
        }
    }

    // Get a random index from the weighted list
    public int GetRandomWeightedIndex()
    {
        if (weightedIndices.Count == 0)
        {
            throw new InvalidOperationException("Weighted list is empty.");
        }

        int randomIndex = random.Next(weightedIndices.Count);
        return weightedIndices[randomIndex];
    }
}