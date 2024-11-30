using UnityEngine;
using UnityEngine.UI;

public class FlowBarUI : MonoBehaviour
{
    public Slider flowSlider;
    private FlowManager flowManager;

    void Start()
    {
        flowManager = FindFirstObjectByType<FlowManager>();
        flowSlider.maxValue = flowManager.maxFlow;
    }

    void Update()
    {
        flowSlider.value = flowManager.currentFlow;
    }
}

