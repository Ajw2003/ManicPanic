using UnityEngine;

public class DishWashing : MonoBehaviour
{
    public Dish dishPrefab;
    public Dish[] dishes;

    [Header("Spawn Settings")]
    public int minDishes = 3;
    public int maxDishes = 6;
    public float sinkRadius = 2.0f; // Area size to scatter dishes
    public float spawnHeightOffset = 0.05f; // Small Y-gap so they don't Z-fight

    private int _currentDishIndex = 0;
    private bool _allClean;
    private bool _started;

    void Start()
    {
        InitializeLevel();
    }

    void InitializeLevel()
    {
        int count = Random.Range(minDishes, maxDishes + 1);
        dishes = new Dish[count];

        for (int i = 0; i < count; i++)
        {
            // 1. Generate a random position in a circle on the XZ plane
            Vector2 randomCircle = Random.insideUnitCircle * sinkRadius;
            Vector3 spawnPos = transform.position + new Vector3(randomCircle.x, i * spawnHeightOffset, randomCircle.y);

            // 2. Instantiate and Randomize Rotation so they look "thrown in"
            Quaternion randomRot = Quaternion.Euler(0, Random.Range(0, 360), 0);
            
            dishes[i] = Instantiate(dishPrefab, spawnPos, randomRot, this.transform);
            dishes[i].messValue = Random.Range(50f, 100f);
            dishes[i].isActive = (i == 0); // Only the first dish is active
        }
        _started = true;
    }

    void Update()
    {
        if (!_started || _allClean) return;

        if (dishes[_currentDishIndex].isClean)
        {
            // Optional: Add a "fading" effect or just destroy
            Destroy(dishes[_currentDishIndex].gameObject);
            _currentDishIndex++;

            if (_currentDishIndex < dishes.Length)
            {
                dishes[_currentDishIndex].isActive = true;
                Debug.Log($"Next dish ready!");
            }
            else
            {
                _allClean = true;
                Debug.Log("The sink is empty!");
            }
        }
    }
}