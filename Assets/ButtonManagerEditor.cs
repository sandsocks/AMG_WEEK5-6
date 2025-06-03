using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

[CustomEditor(typeof(BoardManager))]
public class ButtonManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        BoardManager boardManager = (BoardManager)target;
        if (GUILayout.Button("Create Board"))
        {
            boardManager.CreateBoard();
        }

        if (GUILayout.Button("Delete Board"))
        {
            boardManager.DeleteBoard();
        }
    }
    
}
