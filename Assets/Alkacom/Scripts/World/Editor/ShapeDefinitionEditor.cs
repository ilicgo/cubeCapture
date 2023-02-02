using UnityEditor;
using UnityEngine;

namespace Alkacom.Scripts
{
    [CustomEditor(typeof(ShapeDefinition))]
    public class ShapeDefinitionEditor : Editor
    {
        public override void OnInspectorGUI()
        {

            var lm = (ShapeDefinition) target;

            var t = lm.GetShape();
            
            if (GUILayout.Button("Reset"))
            {
                t.width = 2;
                t.height = 2;
                t.data = new int[4];
            }

            var width = EditorGUILayout.IntField("Width", t.width);
            var height = EditorGUILayout.IntField("height", t.height);
            if (width != t.width || height != t.height)
            {
                t.width = width;
                t.height = height;

                t.data = new int[width * height];

            }
            
           
            
            ShapeDrawTool.DrawGrid(t, (x,y,val) =>
            {
                if (val == 1) return "X";
             
                if (val == 0) return " ";

                return "..";
            }, (val) => new GUIStyle()
            {
                alignment = TextAnchor.MiddleCenter,
                fontStyle = FontStyle.Bold,
                normal = new GUIStyleState()
                    {background = val == 0 ? Texture2D.grayTexture : Texture2D.normalTexture}
            }, val => val == 0 ? 1 : 0);
            
            
            lm.SetDirty();
            
          
            base.OnInspectorGUI();
        }

       
    }
}