using UnityEngine;

public class Bot : MonoBehaviour
{
    public float energy = 100f;
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
        Instantiate(gameObject, new Vector3(transform.position.x, transform.position.y + 100f, transform.position.z), Quaternion.identity);
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
        Debug.Log("Random Angle set");
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
            energy += 20f;
        }
        else if (!isKind && !otherBot.isKind) // Both NOT kind
        {
            energy -= 20f;
        }
        else if (isKind && !otherBot.isKind) // This kind, other NOT kind
        {
            energy -= 50f;
        }
        else if (!isKind && otherBot.isKind) // This NOT kind, other kind
        {
            energy += 10f;
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
