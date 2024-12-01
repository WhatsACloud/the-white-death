using UnityEngine;
using UnityEngine.UI; // For UI elements like the Flow Bar
using TMPro;          // For displaying the current Flow Level name

[System.Serializable]
public struct FlowLevel
{
    public string levelName;   // Name of the flow level, e.g., "CALM"
    public Color levelColor;   // Corresponding color for the level
    public int flowThreshold;  // Minimum flow required to reach this level
}

public class FlowManager : MonoBehaviour
{
    public static FlowManager instance;

    [Header("Flow Levels")]
    public FlowLevel[] flowLevels; // Configure this in the Inspector

    [Header("Flow Settings")]
    public int maxFlow = 300;         // Maximum flow value
    public int currentFlow = 0;      // Current flow value
    public int flowPerKill = 25;     // Flow gained per enemy kill
    public int dashCost = 10;        // Flow cost for dashing
    public float decayRate = 1f;     // Flow decay rate per second

    [Header("UI References")]
    public Slider flowSlider;
    public Image flowBarFill;
    public TextMeshProUGUI flowLevelText; // Reference to the Flow Level text

    private int currentFlowState = 0; // Index of the current flow level
    private PlayerController player; // Reference to the player for stat changes

    void Start()
    {
        instance = this;
        player = FindFirstObjectByType<PlayerController>();

        // Initialize the flow bar color and level
        UpdateFlowUI();
        ApplyFlowBuffs();
    }

    void Update()
    {
        // Decay flow over time
        if (currentFlow > 0)
        {
            currentFlow -= Mathf.FloorToInt(decayRate * Time.deltaTime);
            if (currentFlow < 0) currentFlow = 0;
        }

        // Update flow level based on current flow
        UpdateFlowState();

        // Update UI
        UpdateFlowUI();
    }

    public bool CanDash()
    {
        // Check if the player has enough flow to dash
        return currentFlow >= dashCost;
    }

    public void GainFlow(int amount)
    {
        currentFlow += amount;
        if (currentFlow > maxFlow) currentFlow = maxFlow;
    }

    private void UpdateFlowState()
    {
        int newState = 0;

        // Determine the new flow state based on thresholds
        for (int i = 0; i < flowLevels.Length; i++)
        {
            if (currentFlow >= flowLevels[i].flowThreshold)
            {
                newState = i;
            }
        }

        // Apply changes only if the flow state has changed
        if (newState != currentFlowState)
        {
            currentFlowState = newState;
            ApplyFlowBuffs();
        }
    }


    private void ApplyFlowBuffs()
    {
        // Example buffs for each flow level
        switch (currentFlowState)
        {
            case 0: // CALM
                player.SetStats(1f, 1f); // Default speed and slash range
                break;
            case 1: // FOCUSED
                player.SetStats(2f, 2f);
                break;
            case 2: // INTENSE
                player.SetStats(1.5f, 4f);
                break;
            case 3: // OVERDRIVE
                player.SetStats(2f, 8f);
                break;
        }
        MusicManager.instance.ChangeMusicForFlowState(currentFlowState);
    }

    private void UpdateFlowUI()
    {
        float actualFlow = (float)currentFlow - flowLevels[currentFlowState].flowThreshold;
        // Update the fill amount of the Flow Bar
        if (flowSlider != null)
        {
            flowSlider.value = actualFlow;

            // Change the bar's color to match the current flow level
            flowBarFill.color = flowLevels[currentFlowState].levelColor;
        }

        // Update the level text to display the current flow level name
        if (flowLevelText != null)
        {
            flowLevelText.text = flowLevels[currentFlowState].levelName;
        }
    }
}

