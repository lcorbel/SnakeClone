using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawnerScript : MonoBehaviour
{
    public Vector3 randomPosition;
    public GameObject food;
    public BoxCollider2D gridArea;

    public void Start()
    {
        spawnRandomFood(food);
    }

    public void spawnRandomFood(GameObject food)
    {
        randomPosition = new Vector3(Mathf.Round(Random.Range(gridArea.bounds.min.x, gridArea.bounds.max.x)), Mathf.Round(Random.Range(gridArea.bounds.min.y, gridArea.bounds.max.y)), 0.0f);
        Instantiate(food, randomPosition, Quaternion.identity);
    }
}
