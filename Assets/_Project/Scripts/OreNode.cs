using UnityEngine;

public class OreNode : MonoBehaviour
{
    public GameObject smallOrePrefab;
    public Transform spawnPoint;
    public float spawnForce = 2f;
    public float cooldownTime = 1.5f;
    public Transform oreCollectPoint;

    private float nextMineTime = 0f;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Pickaxe"))
        {
            return;
        }

        if (Time.time < nextMineTime)
        {
            return;
        }

        PickaxeStats stats = collision.gameObject.GetComponentInParent<PickaxeStats>();

        int level = 1;
        if (stats != null) {
            level = stats.level;
        }

        for (int i = 0; i < level; i++) {
            Mine();
        }

        nextMineTime = Time.time + cooldownTime;
    }

    void Mine()
    {
        GameObject ore = Instantiate(
            smallOrePrefab,
            spawnPoint.position,
            Quaternion.identity
        );

        TeleportOnGrab tele = ore.GetComponent<TeleportOnGrab>();

        if (tele == null)
        {
            tele = ore.GetComponentInChildren<TeleportOnGrab>();
        }

        if (tele != null)
        {
            tele.targetLocation = oreCollectPoint;
        }

        Rigidbody rb = ore.GetComponent<Rigidbody>();

        if (rb == null)
        {
            rb = ore.AddComponent<Rigidbody>();
        }

        rb.useGravity = true;
        rb.isKinematic = false;

        Vector3 randomDir = new Vector3(
            Random.Range(-0.5f, 0.5f),
            1f,
            Random.Range(-0.5f, 0.5f)
        );

        rb.AddForce(randomDir * spawnForce, ForceMode.Impulse);
    }
}