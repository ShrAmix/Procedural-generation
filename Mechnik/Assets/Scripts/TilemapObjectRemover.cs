using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapObjectRemover : MonoBehaviour
{
    public Tilemap tilemap; // ������ �� Tilemap
    public float radius = 1f; // ������ �������� ��������

    void Update()
    {
        // ��������� ������� ����� ������ ����
        if (Input.GetMouseButtonDown(0)) // ���
        {
            // �������� ������� ���������� �������
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPosition = tilemap.WorldToCell(mouseWorldPosition);

            // ������� ������� � ��������� ������� �� �������
            RemoveObjectsInRadius(cellPosition);
        }
    }

    // ������� ������� � ��������� ������� �� �������� ������� � Tilemap
    void RemoveObjectsInRadius(Vector3Int centerPosition)
    {
        // �������� �� ���� ������� � �������� � �������� ��������
        for (int x = -Mathf.CeilToInt(radius); x <= Mathf.CeilToInt(radius); x++)
        {
            for (int y = -Mathf.CeilToInt(radius); y <= Mathf.CeilToInt(radius); y++)
            {
                // ���������, ��������� �� ������� ������ ������ ����� � �������� ��������
                if (new Vector2(x, y).magnitude <= radius)
                {
                    // �������� ������� ������
                    Vector3Int currentCellPosition = new Vector3Int(centerPosition.x + x, centerPosition.y + y, centerPosition.z);

                    // ������� ������ �� Tilemap � ������� ������
                    tilemap.SetTile(currentCellPosition, null);
                }
            }
        }
    }
}
