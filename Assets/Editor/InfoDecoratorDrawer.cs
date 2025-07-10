#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;


/// <summary>
/// Decorator class for displaying information above others fields in the Unity Editor.
/// </summary>
[CustomPropertyDrawer(typeof(InfoAttribute))]
public class InfoDecoratorDrawer : DecoratorDrawer
{
    InfoAttribute Info => (InfoAttribute)attribute;

    public override float GetHeight()
    {
        int lineCount = Info.text.Split('\n').Length - 1;
        int fontSize = EditorStyles.label.fontSize;
        float height = EditorGUIUtility.singleLineHeight * (fontSize/11f);
        if (lineCount == 0)
            return height;
        return height * lineCount + 4f;
    }

    public override void OnGUI(Rect position)
    {
        EditorGUI.HelpBox(position, "", MessageType.Info);
        var textStyle = new GUIStyle(EditorStyles.label)
        {
            alignment = TextAnchor.MiddleCenter,
            fontSize = EditorStyles.label.fontSize,
            richText = true,
            wordWrap = true
        };
        textStyle.padding.top = 2;

        Rect textRect = new Rect(
            position.x + 4,
            position.y + 4,
            position.width - 8,
            position.height - 8
            );
        EditorGUI.LabelField(textRect, Info.text, textStyle);
    }
}
#endif