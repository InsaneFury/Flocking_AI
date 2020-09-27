using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [Header("SpawnerSettings")]
    public GameObject prefab;
    public Vector3 center;
    public Vector3 size;
    public int amount = 20;
    public float timeToRespawn = 5;
    private float timer = 0;

    List<GameObject> foods;

    private void Start()
    {
        foods = new List<GameObject>();
        for (int i = 0; i < amount; i++)
        {
            Vector3 pos = center + new Vector3(
            Random.Range(-size.x / 2, size.x / 2),
            Random.Range(-size.y / 2, size.y / 2),
            Random.Range(-size.z / 2, size.z / 2));
            foods.Add(Instantiate(prefab, pos, Quaternion.identity));

            foreach (GameObject food in foods)
            {
                food.SetActive(false);
            }
        }
    }
    private void Update()
    {
        timer += Time.deltaTime;
        CheckFoodEaten();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.3f);
        Gizmos.DrawCube(center, size);
    }

    public void RandomSpawn()
    {
        timer = 0;
        foreach (GameObject food in foods)
        {
            food.SetActive(false);
            Vector3 pos = center + new Vector3(
            Random.Range(-size.x / 2, size.x / 2),
            Random.Range(-size.y / 2, size.y / 2),
            Random.Range(-size.z / 2, size.z / 2));
            food.transform.position = pos;
            food.transform.rotation = prefab.transform.rotation;
            food.SetActive(true);
        }
    }

    void CheckFoodEaten()
    {
        foreach (GameObject food in foods)
        {
            if (food.activeSelf && (timer < timeToRespawn))
                return;        
        }
        RandomSpawn();
    }
}
