using System.Collections.Generic;
using UnityEngine;
using System;

public class ShapeController : MonoBehaviour
{
    public List<ShapeSlot> shapes;
    public ShapeSpriteStruct shapeSprite;
    public ShapeWeightStruct shapeWeight;

    public WeightedRandom shapeWeightRandom;
    private float timer;
    
    [Range(0.1f, 1f)]
    public float spawnDuration = 0.2f;

    void Start()
    {
        //shapeWeightRandom.AddWeightedItem(shapeWeight.none);
        shapeWeightRandom.AddWeightedItem(shapeWeight.square);
        shapeWeightRandom.AddWeightedItem(shapeWeight.circle);
        shapeWeightRandom.AddWeightedItem(shapeWeight.triangle);
    }

    void Update()
    {
        UpdateSpawnTimer();
    }

    private void UpdateSpawnTimer()
    {
        timer += Time.deltaTime;

        if(timer >= spawnDuration)
        {
            timer = 0f;
            SpawnShape();
        }
    }

    public void SpawnShape()
    {
        List<ShapeSlot> availableSlots = shapes.FindAll(slot => slot.type == ShapeType.NONE);

        if (availableSlots.Count == 0) return;

        ShapeSlot selectedSlot = availableSlots[UnityEngine.Random.Range(0, availableSlots.Count)];

        ChangeShape(selectedSlot);
    }

    [ContextMenu("Reset")]
    public void ResetShape()
    {
        foreach (var item in shapes)
        {
            item.ChangeShape(ShapeType.NONE);
        }
    }

    private bool ChangeShape(ShapeSlot slot)
    {
        if (slot.type != ShapeType.NONE) return false;

        int rand = shapeWeightRandom.GetRandomWeightedIndex();

        ShapeType type = ShapeType.NONE;
        Sprite sprite = shapeSprite.none;

        if(rand <= shapeWeight.none + shapeWeight.square)
        {
            type = ShapeType.SQUARE;
            sprite = shapeSprite.square;
        }
        else if(rand <= shapeWeight.none + shapeWeight.square + shapeWeight.circle)
        {
            type = ShapeType.CIRCLE;
            sprite = shapeSprite.circle;
        }
        else if(rand <= shapeWeight.none + shapeWeight.square + shapeWeight.circle + shapeWeight.triangle)
        {
            type = ShapeType.TRIANGLE;
            sprite = shapeSprite.triangle;
        }

        slot.ChangeShape(type, sprite);
        return true;
    }
}

[Serializable]
public struct ShapeSpriteStruct
{
    public Sprite none;
    public Sprite square;
    public Sprite circle;
    public Sprite triangle;
}

[Serializable]
public struct ShapeWeightStruct
{
    public int none;
    public int square;
    public int circle;
    public int triangle;
}
