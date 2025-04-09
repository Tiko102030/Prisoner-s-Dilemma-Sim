using UnityEngine;

public class Bot : MonoBehaviour
{
    public int energy = 100;
    public bool isKind;
    public float speed = 5f;

    [SerializeField] Material kindMaterial;
    [SerializeField] Material meanMaterial;
    MeshRenderer meshRenderer;
    Rigidbody rb;

    public float angleChangeInterval = 2f;
    public float randomMovementAngle = 0;



    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();

        energy = 100;
        angleChangeInterval = Random.Range(1f, 3f);

        DetermineSide();

        InvokeRepeating("SetRandomAngle", 0, angleChangeInterval);
    }

    void Update()
    {
        Mathf.Clamp(randomMovementAngle, 0f, 360f);

        DoRandomMovement();

        if (energy <= 0f)
        {
            Die();
        }
        if (energy > 150f)
        {
            Reproduce();
        }    
    }

    public void Reproduce()
    {
        energy -= 50;

        Instantiate(gameObject, new Vector3(transform.position.x, transform.position.y + 10f, transform.position.z), Quaternion.identity);
    }

    void DetermineSide()
    {
        if (Random.value < 0.5f)
        {
            isKind = true;

            meshRenderer.material = kindMaterial;
        }
        else
        {
            isKind = false;

            meshRenderer.material = meanMaterial;
        }
    }

    void DoRandomMovement()
    {
        transform.rotation = Quaternion.Euler(0, randomMovementAngle, 0);

        rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
    }

    void SetRandomAngle()
    {
        randomMovementAngle = Random.value * 360;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Bot>())
        {
            DetermineInteractionResult(collision.gameObject.GetComponent<Bot>());
        }
    }

    public void DetermineInteractionResult(Bot otherBot)
    {
        if (isKind && otherBot.isKind) // Both kind
        {
            energy += 20; // Default 20
        }
        else if (!isKind && !otherBot.isKind) // Both NOT kind
        {
            energy -= 20; // Default -20
        }
        else if (isKind && !otherBot.isKind) // This kind, other NOT kind
        {
            energy -= 40; // Default -40
        }
        else if (!isKind && otherBot.isKind) // This NOT kind, other kind
        {
            energy += 20; // Default 10
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
