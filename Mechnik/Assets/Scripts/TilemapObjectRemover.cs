using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapObjectRemover : MonoBehaviour
{
    public Tilemap tilemap; // Ссылка на Tilemap
    public float radius = 1f; // Радиус удаления объектов

    void Update()
    {
        // Проверяем нажатие левой кнопки мыши
        if (Input.GetMouseButtonDown(0)) // ЛКМ
        {
            // Получаем мировые координаты курсора
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPosition = tilemap.WorldToCell(mouseWorldPosition);

            // Удаляем объекты в указанном радиусе от курсора
            RemoveObjectsInRadius(cellPosition);
        }
    }

    // Удаляет объекты в указанном радиусе от заданной позиции в Tilemap
    void RemoveObjectsInRadius(Vector3Int centerPosition)
    {
        // Проходим по всем клеткам в квадрате с заданным радиусом
        for (int x = -Mathf.CeilToInt(radius); x <= Mathf.CeilToInt(radius); x++)
        {
            for (int y = -Mathf.CeilToInt(radius); y <= Mathf.CeilToInt(radius); y++)
            {
                // Проверяем, находится ли текущая клетка внутри круга с заданным радиусом
                if (new Vector2(x, y).magnitude <= radius)
                {
                    // Получаем позицию клетки
                    Vector3Int currentCellPosition = new Vector3Int(centerPosition.x + x, centerPosition.y + y, centerPosition.z);

                    // Удаляем объект из Tilemap в текущей клетке
                    tilemap.SetTile(currentCellPosition, null);
                }
            }
        }
    }
}
