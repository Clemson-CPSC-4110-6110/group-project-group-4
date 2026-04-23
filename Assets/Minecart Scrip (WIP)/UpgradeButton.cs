using UnityEngine;
using TMPro;
using System.Collections;

public class UpgradeButton : MonoBehaviour
{
    [Header("Upgrade Settings")]
    public int cost = 5;
    public string upgradeName = "Pickaxe";
    public PickaxeStats pickaxeStats;

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

        if (pickaxeStats == null)
        {
            ShowTemporaryMessage("No Pickaxe Stats Found!", failColor);
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
        pickaxeStats.UpgradeLevel();

        // Double the cost for the next purchase
        cost *= 2;
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