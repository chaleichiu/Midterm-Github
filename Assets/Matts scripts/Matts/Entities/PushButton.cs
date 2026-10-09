using UnityEngine;
using UnityEngine.Events;

public class PushButton : MonoBehaviour, ISelectable
{
    [SerializeField] private Material defaultColour;
    [SerializeField] private Material hoverColour;
    [SerializeField] private MeshRenderer meshRender;

    public UnityEvent onPush; // Fill in the inspector

    public void OnHoverEnter()
    {
        meshRender.material = hoverColour;
    }

    public void OnHoverExit()
    {
        meshRender.material = defaultColour;
    }

    public void OnSelect()
    {
        onPush?.Invoke();
    }
}
