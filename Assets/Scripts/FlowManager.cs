using UnityEngine;

public class FlowManager : MonoBehaviour
{
    public static FlowManager instance;

    public int maxFlow = 100;         // Maximum flow value
    public int currentFlow = 0;      // Current flow value
    public int flowPerKill = 25;     // Flow gained per enemy kill
    public int dashCost = 10;        // Flow cost for dashing

    public float decayRate = 1f;     // Flow decay rate per second

    public int currentFlowState = 0; // Current flow state (0-3)
    private PlayerController player; // Reference to the player for stat changes

    void Start()
    {
        instance = this;
        player = FindFirstObjectByType<PlayerController>();
    }

    public bool CanDash()
    {
        // Check if the player has enough flow to dash
        return currentFlow >= dashCost;
    }

    void Update()
    {
        // Decay flow over time
        if (currentFlow > 0)
        {
            currentFlow -= Mathf.FloorToInt(decayRate * Time.deltaTime);
            if (currentFlow < 0) currentFlow = 0;
        }

        // Update flow state based on current flow
        UpdateFlowState();
    }

    public void GainFlow(int amount)
    {
        currentFlow += amount;
        if (currentFlow > maxFlow) currentFlow = maxFlow;
    }

    public void ConsumeFlowForDash()
    {
        // Deduct flow for dashing
        if (currentFlow >= dashCost)
        {
            currentFlow -= dashCost;
        }
    }

    private void UpdateFlowState()
    {
        int newState = 0;

        if (currentFlow >= maxFlow * 0.75f) newState = 3;
        else if (currentFlow >= maxFlow * 0.5f) newState = 2;
        else if (currentFlow >= maxFlow * 0.25f) newState = 1;

        if (newState != currentFlowState)
        {
            currentFlowState = newState;
            ApplyFlowBuffs();
        }
    }

    private void ApplyFlowBuffs()
    {
        switch (currentFlowState)
        {
            case 0:
                player.SetStats(1f, 1f); // Default speed and slash range
                break;
            case 1:
                player.SetStats(1.2f, 1.1f); // Small boost
                break;
            case 2:
                player.SetStats(1.5f, 1.25f); // Moderate boost
                break;
            case 3:
                player.SetStats(2f, 1.5f); // Maximum buffs
                break;
        }
    }
}

