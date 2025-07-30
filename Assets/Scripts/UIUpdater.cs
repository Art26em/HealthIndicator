using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerController))]

public class UIUpdater : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private TMP_Text healthStatus;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Image healthBarImage;
    [SerializeField] private Color fullHealthColor;
    [SerializeField] private Color emptyHealthColor;
    [SerializeField] private float animationSpeed = 30f;
    [SerializeField] private float colorChangeSmooth = 0.8f;
    
    private Coroutine _updateCoroutine;
    private float _playerHealth;
    private float _playerMaxHealth;
    private readonly float _valueChangeTreshold = 0.01f;
    
    private void Awake()
    {
        InitializeView();    
    }

    private void InitializeView()
    {
        _playerHealth = player.GetHealth();
        _playerMaxHealth = player.GetMaxHealth();
       
        healthBar.maxValue = _playerMaxHealth;
        healthBar.value = _playerHealth;
        
        healthBarImage.color = Color.Lerp(
            healthBarImage.color,
            healthBar.value < _playerHealth ? fullHealthColor : emptyHealthColor, 
            colorChangeSmooth);
        
        healthStatus.SetText("Health: " + _playerHealth);
    }
    
    private void OnEnable()
    {
        player.OnPlayerGetHeal += PlayerGetHealHandler;
        player.OnPlayerTakeDamage += PlayerTakeDamageHandler;
    }

    private void OnDisable()
    {
        player.OnPlayerGetHeal -= PlayerGetHealHandler;
        player.OnPlayerTakeDamage -= PlayerTakeDamageHandler;
    }
    
    private void PlayerGetHealHandler(float heal)
    {
        _playerHealth += heal;
        UpdateUI();
    }

    private void PlayerTakeDamageHandler(float damage)
    {
        _playerHealth -= damage;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (_updateCoroutine != null)
        {
            StopCoroutine(_updateCoroutine);
        }
        StartCoroutine(HealthViewUpdate());
    }
    
    private IEnumerator HealthViewUpdate()
    {
        _playerHealth = Mathf.Clamp(_playerHealth, 0, _playerMaxHealth);
        while (Mathf.Abs(healthBar.value - _playerHealth) > _valueChangeTreshold)
        {
            healthBarImage.color = Color.Lerp(
                healthBarImage.color,
                healthBar.value < _playerHealth ? fullHealthColor : emptyHealthColor, 
                Time.deltaTime * colorChangeSmooth);

            healthBar.value = Mathf.MoveTowards(
                healthBar.value, 
                _playerHealth, 
                animationSpeed * Time.deltaTime);
            
            healthStatus.SetText("Health: " + _playerHealth);
            
            yield return null;
        }
    }
    
}
