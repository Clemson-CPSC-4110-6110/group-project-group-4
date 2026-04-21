using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class PickaxeImpactVFX : MonoBehaviour
{
    [Header("References")]
    public XRGrabInteractable pickaxeGrabInteractable;
    public GameObject impactVFXPrefab;
    public Rigidbody pickaxeRigidbody;

    [Header("Rules")]
    public float impactCooldown = 0.2f;
    public LayerMask validSurfaceLayers = ~0;
    public bool requireHeld = true;
    public float minimumSwingSpeed = 1.2f;
    public Transform tipSpawnPoint;
    private float nextImpactTime = 0f;

    private void OnTriggerEnter(Collider other)
    {
        if (Time.time < nextImpactTime)
            return;

        if (((1 << other.gameObject.layer) & validSurfaceLayers) == 0)
            return;

        if (requireHeld && !IsHeld())
            return;

        if (!MeetsSwingSpeed())
            return;

        Vector3 spawnPosition = tipSpawnPoint != null ? tipSpawnPoint.position : transform.position;
        Vector3 spawnNormal = -transform.forward;

        SpawnImpact(spawnPosition, spawnNormal);

        nextImpactTime = Time.time + impactCooldown;
    }

    private bool IsHeld()
    {
        if (pickaxeGrabInteractable == null)
            return false;

        return pickaxeGrabInteractable.isSelected;
    }

    private bool MeetsSwingSpeed()
    {
        if (pickaxeRigidbody == null)
            return true;

        return pickaxeRigidbody.linearVelocity.magnitude >= minimumSwingSpeed;
    }

    private void SpawnImpact(Vector3 position, Vector3 normal)
    {
        if (impactVFXPrefab == null)
            return;

        Quaternion rotation = Quaternion.LookRotation(normal);
        GameObject vfx = Instantiate(impactVFXPrefab, position, rotation);

        ParticleSystem ps = vfx.GetComponentInChildren<ParticleSystem>();
        if (ps != null)
        {
            ps.Play();
            float lifetime = ps.main.duration + ps.main.startLifetime.constantMax;
            Destroy(vfx, lifetime + 0.5f);
        }
        else
        {
            Destroy(vfx, 2f);
        }
    }
}