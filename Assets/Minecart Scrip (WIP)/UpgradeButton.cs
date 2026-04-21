using UnityEngine;
using TMPro;
using System.Collections;

public class UpgradeButton : MonoBehaviour
{
    [Header("Upgrade Settings")]
    public int cost = 10;
    public string upgradeName = "Generic Upgrade";

    [Header("UI Feedback")]
    public TextMeshPro upgradeText;
    public float messageDuration = 3.2f;

    [Header("Colors")]
    public Color defaultColor = Color.white;
    public Color successColor = new Color(0.3f, 0.85f, 0.3f);
    public Color failColor = new Color(0.9f, 0.2f, 0.2f);

    private Coroutine messageCoroutine;

    void Start()
    {
        SetDefaultText();
    }

    public void TryPurchase()
    {
        if (ScoreManager.instance == null)
        {
            ShowTemporaryMessage("No Score Manager Found!", failColor);
            return;
        }

        if (ScoreManager.instance.SpendPoints(cost))
        {
            ApplyUpgrade();
            ShowTemporaryMessage("Upgrade Purchased!", successColor);
        }
        else
        {
            ShowTemporaryMessage("Not Enough Score...", failColor);
        }
    }

    void ApplyUpgrade()
    {
        // Put your actual upgrade logic here later
        // Example:
        // playerSpeed += 1;
        // miningRate *= 1.2f;
        // unlockObject.SetActive(true);
    }

    void SetDefaultText()
    {
        if (upgradeText == null) return;

        upgradeText.color = defaultColor;
        upgradeText.text = "Press to upgrade " + upgradeName + "\nCost: " + cost;
    }

    void ShowTemporaryMessage(string message, Color color)
    {
        if (upgradeText == null) return;

        if (messageCoroutine != null)
        {
            StopCoroutine(messageCoroutine);
        }

        messageCoroutine = StartCoroutine(TemporaryMessageRoutine(message, color));
    }

    IEnumerator TemporaryMessageRoutine(string message, Color color)
    {
        upgradeText.color = color;
        upgradeText.text = message;

        yield return new WaitForSeconds(messageDuration);

        SetDefaultText();
        messageCoroutine = null;
    }
}