using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportOnGrab : MonoBehaviour
{
    public Transform targetLocation;

    public bool matchTargetRotation = true;
    public bool disableGrabAfterTeleport = true;
    public bool makeKinematicAfterTeleport = true;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private Rigidbody rb;

    private void Awake()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(OnGrabbed);
    }

    private void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrabbed);
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        if (targetLocation == null)
        {
            Debug.LogWarning("No targetLocation set!", this);
            return;
        }

        // Move instantly
        transform.position = targetLocation.position;

        if (matchTargetRotation)
            transform.rotation = targetLocation.rotation;

        if (ScoreManager.instance != null)
            ScoreManager.instance.AddPoint();

        // Stop physics
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            if (makeKinematicAfterTeleport)
                rb.isKinematic = true;
        }

        // Disable grabbing so it doesn't stick to hand
        if (disableGrabAfterTeleport)
            grabInteractable.enabled = false;
    }
}