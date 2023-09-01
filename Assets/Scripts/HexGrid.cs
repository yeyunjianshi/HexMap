using System;
using TMPro;
using UnityEngine;

public class HexGrid : MonoBehaviour {
    public int width = 6;
    public int height = 6;

    public HexCell cellPrefab;
    public TextMeshProUGUI cellLabelPrefab;

    private HexCell[] cells;
    private Canvas gridCanvans;
    private HexMesh hexMesh;

    public Color defaultColor = Color.white;
    public Color touchedColor = Color.magenta;

    private void Awake() {
        gridCanvans = GetComponentInChildren<Canvas>();
        hexMesh = GetComponentInChildren<HexMesh>();

        cells = new HexCell[width * height];
        for (int z = 0, i = 0; z < height; z++)
        for (var x = 0; x < width; x++)
            CreateCell(x, z, i++);
    }

    private void Start() {
        hexMesh.Triangulate(cells);
    }

    private void CreateCell(int x, int z, int i) {
        Vector3 position;
        position.x = (x + z * .5f - z / 2) * (HexMetrics.innerRadius * 2f);
        position.y = 0f;
        position.z = z * (HexMetrics.outerRadius * 1.5f);

        var cell = cells[i] = Instantiate(cellPrefab);
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;
        cell.coordinates = HexCoordinates.FromOffsetCoordinates(x, z);
        cell.color = defaultColor;

        var label = Instantiate(cellLabelPrefab);
        label.rectTransform.SetParent(gridCanvans.transform, false);
        label.rectTransform.anchoredPosition = new Vector2(position.x, position.z);
        label.text = cell.coordinates.ToStringOnSeparateLines();
    }

    public void ColorCell(Vector3 position, Color color) {
        position = transform.InverseTransformPoint(position);
        var coordinates = HexCoordinates.FromPosition(position);
        var index = coordinates.X + coordinates.Z * width + coordinates.Z / 2;
        var cell = cells[index];
        cell.color = color;
        hexMesh.Triangulate(cells);
    }
}