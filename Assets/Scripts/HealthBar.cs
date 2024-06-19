using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBarFill;
    [SerializeField] private Transform healthBarTransform;
    private Transform cameraTransform;
    private Coroutine hideCoroutine;
    private const float hideDelay = 2f; // Time after which the health bar will be hidden
    private float maxHealth;
    private float currentHealth;

    private void Start()
    {
        DisabledView();
        cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        if (!healthBarTransform.gameObject.activeSelf)
            return;

        // Make the health bar face the camera
        healthBarTransform.LookAt(healthBarTransform.position + cameraTransform.rotation * Vector3.forward, cameraTransform.rotation * Vector3.up);

        // Update health bar fill
        healthBarFill.fillAmount = currentHealth / maxHealth;
    }

    public void SetMaxHealth(float maxHealth)
    {
        this.maxHealth = maxHealth;
    }

    public void SetHealth(float currentHealth)
    {
        this.currentHealth = currentHealth;
    }

    public void DisabledView()
    {
        healthBarTransform.gameObject.SetActive(false);
    }

    public void EnableView()
    {
        healthBarTransform.gameObject.SetActive(true);
        ResetHideTimer();
    }

    public void ResetHideTimer()
    {
        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
        }
        hideCoroutine = StartCoroutine(HideAfterDelay());
    }

    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(hideDelay);
        DisabledView();
    }
}