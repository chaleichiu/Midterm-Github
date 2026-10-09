using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
/// <summary>
/// This is the obvserver pattern, and this is subscribing to the Actions of the Health class
/// </summary>
public class UIManager : MonoBehaviour
{
    [SerializeField] private Health playerHealth;

    [Header("UI elements")]
    public TMP_Text txtHealth;
    public GameObject gameOverText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameOverText.SetActive(false);
    }

    private void OnEnable()
    {
        playerHealth.OnHealthUpdate += OnHealthUpdate;
        playerHealth.OnDeath += OnDeath;
    }
    private void OnDestroy()
    {
        playerHealth.OnHealthUpdate -= OnHealthUpdate;
        playerHealth.OnDeath -= OnDeath;
    }
    void OnHealthUpdate(float health)
    {
        txtHealth.text = "Health :" + Mathf.Floor(health).ToString();
    }
    void OnDeath()
    {
        gameOverText.SetActive(true);
    }
}
