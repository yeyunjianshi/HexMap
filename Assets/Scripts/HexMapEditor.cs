using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace {
    public class HexMapEditor : MonoBehaviour {
        public Color[] colors;
        public HexGrid hexGrid;
        private Color activeColor;

        private void Awake() {
            SelectColor(0);
        }

        private void Update() {
            if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
                HandleInput();
        }

        private void HandleInput() {
            var inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(inputRay, out var hit))
                hexGrid.ColorCell(hit.point, activeColor);
        }

        public void SelectColor(int index) {
            activeColor = colors[index];
        }
    }
}