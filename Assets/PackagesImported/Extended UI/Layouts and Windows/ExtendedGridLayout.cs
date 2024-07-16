using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace ExtendedUI
{
    /// <summary>
    /// Unity's built-in layout grid doesn't behave like the other LayoutGroup
    /// elements, making it quite unintuitive and hard to use. We are making our
    /// own, based on this video: https://www.youtube.com/watch?v=CGsEJToeXmA
    /// </summary>
    
    public class ExtendedGridLayout : LayoutGroup
    {
        public enum FitType
        {
            Uniform,
            PrioritizeWidth,
            PrioritizeHeight,
            FixedRows,
            FixedColumns
        }

        public FitType fitType = FitType.Uniform;
        public int rows = 1;
        public int columns = 1;
        public Vector2 cellSize;
        public Vector2 spacing;
        public bool fitX;
        public bool fitY;
        
        protected new IEnumerator Start()
        {
            // UI Layouts need one extra frame to calculate their size
            yield return new WaitForEndOfFrame();
            CalculateLayoutInputHorizontal();
        }

        public override void CalculateLayoutInputHorizontal()
        {
            base.CalculateLayoutInputHorizontal();
            SetRowsAndColumnsBasedOnFit();
            CalculateChildrenPosition();
        }

        private void SetRowsAndColumnsBasedOnFit()
        {
            if (fitType == FitType.PrioritizeWidth
                || fitType == FitType.PrioritizeHeight
                || fitType == FitType.Uniform)
            {
                float squareRoot = Mathf.Sqrt(transform.childCount);
                rows = columns = Mathf.CeilToInt(squareRoot);
            }
            
            if (fitType == FitType.PrioritizeWidth || fitType == FitType.FixedColumns)
            {
                rows = Mathf.CeilToInt(transform.childCount / (float)columns);
            }
            if (fitType == FitType.PrioritizeHeight || fitType == FitType.FixedRows)
            {
                columns = Mathf.CeilToInt(transform.childCount / (float)rows);
            }
        }

        private void CalculateChildrenPosition()
        {
            // Get the container size
            var rect = rectTransform.rect;
            float parentWidth = rect.width;
            float parentHeight = rect.height;

            // Calculate cell size
            float cellWidth = (parentWidth / columns)
                              - ((spacing.x/columns) * (columns - 1))
                              - (padding.left/(float)columns) 
                              - (padding.right/(float)columns);
            float cellHeight = parentHeight / rows
                               - ((spacing.y / rows) * (rows - 1))
                               - (padding.top/(float)columns) 
                               - (padding.bottom/(float)columns);
            cellSize.x = fitX ? cellWidth : cellSize.x;
            cellSize.y = fitY ? cellHeight : cellSize.y;

            // Reposition all children
            for (int i = 0; i < rectChildren.Count; i++)
            {
                // Calculate row and column
                var rowCount = i / columns;
                var columnCount = i % columns;

                var child = rectChildren[i];

                // Calculate positions
                var xPos = padding.left + (columnCount * (cellSize.x + spacing.x));
                var yPos = padding.top + (rowCount * (cellSize.y + spacing.y));

                SetChildAlongAxis(child, 0, xPos, cellSize.x);
                SetChildAlongAxis(child, 1, yPos, cellSize.y);
            }
        }

        public override void CalculateLayoutInputVertical()
        {
            //throw new System.NotImplementedException();
        }

        public override void SetLayoutHorizontal()
        {
            //throw new System.NotImplementedException();
        }

        public override void SetLayoutVertical()
        {
            //throw new System.NotImplementedException();
        }
    }
}
