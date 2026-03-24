using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI; // Needed for the Slider

public class Dish : MonoBehaviour
{
    private float _previousSpeed = 0;
    public float messValue = 100;
    private float _initialMess; // To calculate progress %
    
    public bool isClean;
    public bool isActive;
    
    public float accelerationSensitivity = 0.5f;

    [Header("UI Reference")]
    public Slider progressSlider;

    void Start()
    {
        _initialMess = messValue;
        UpdateUI();
    }

    void Update()
    {
        if (!isActive || isClean) return;

        Vector2 mouseDelta = Mouse.current.delta.ReadValue();
        float currentSpeed = mouseDelta.magnitude / Time.deltaTime;
        float acceleration = Mathf.Max(0, currentSpeed - _previousSpeed);

        if (acceleration > 0)
        {
            messValue -= acceleration * accelerationSensitivity * Time.deltaTime;
            UpdateUI();
        }

        _previousSpeed = currentSpeed;

        if (messValue <= 0)
        {
            messValue = 0;
            isClean = true;
        }
    }

    void UpdateUI()
    {
        if (progressSlider != null)
        {
            // Calculate 0 to 1 value for the slider
            progressSlider.value = messValue / _initialMess;
        }
    }
}