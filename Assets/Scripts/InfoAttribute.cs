using UnityEngine;

/// <summary>
/// This script is used to display information about the components 
/// </summary>
public class InfoAttribute : PropertyAttribute
{
    public readonly string text;
    public InfoAttribute(string text) => this.text = text;
}
