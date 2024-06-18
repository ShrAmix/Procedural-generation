using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // �������� �����������
    public float jumpForce = 10f; // ���� ������

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // ���������� ���������
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // ���������� �������
        if (Input.GetKeyDown(KeyCode.W))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // ������������ �������� ���� �� ������� ���
        if (Input.GetMouseButtonDown(1)) // ���
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
        }
    }
}
