using System;
using UnityEditor;
using UnityEngine;

namespace Alkacom.Scripts
{
    public static class ShapeDrawTool
    {
        public static void DrawGrid(Shape shape, Func<int, int,int, string> renderText)
        {
            for (int yi = 0, yMax = shape.height; yi < yMax; yi++)
            {
                EditorGUILayout.BeginHorizontal();

                for (int xi = 0, xMax = shape.width; xi < xMax; xi++)
                {

                    var x = xi;
                    var y = shape.height - yi - 1;

                   

                    
                    var val = shape.Get(x, y);
                    var valStr = renderText.Invoke(x,y,val);


                    GUILayout.Button(valStr, GUILayout.Width(50), GUILayout.Height(50));
                    
                }

                EditorGUILayout.EndHorizontal();
            }
        }
        public static void DrawGrid(Shape shape, Func<int,int,int, string> renderText, Func<int, GUIStyle> renderStyle,
            Func<int, int> OnClickNewVal)
        {
            for (int yi = 0, yMax = shape.height; yi < yMax; yi++)
            {
                EditorGUILayout.BeginHorizontal();

                for (int xi = 0, xMax = shape.width; xi < xMax; xi++)
                {

                    var x = xi;
                    var y = shape.height - yi - 1;

                    if (shape.IsOutOfBound(x, y))
                    {
                        shape.data = new int[shape.width * shape.height];

                        return;
                    }

                    var val = shape.Get(x, y);

                    var valStr = renderText.Invoke(x,y,val);
                    var style = renderStyle.Invoke(val);

                    if (GUILayout.Button(valStr, style, GUILayout.Width(50), GUILayout.Height(50)))
                    {
                        shape.Set(x, y, OnClickNewVal.Invoke(val));
                    }

                    ;
                }

                EditorGUILayout.EndHorizontal();
            }
        }
    }
}