using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
public class EditorCreateTemplateScript : EditorWindow
{
    string fileName = "Assets/Scripts/Core/Editor/GenerateTemplateCode/ScriptTemplate/TemplateScriptPool.txt";

    string refClassName = "[@ClassName]";
    string refParameterName = "[$obj]";
    string refFunctionName = "[$Obj]";

    string objectName = "ObjectName";
    string className;
    string sourceData;

    [MenuItem("EditorTools/CreateTemplateScript")]
    static void Init()
    {
        EditorCreateTemplateScript window = (EditorCreateTemplateScript)EditorWindow.GetWindow(typeof(EditorCreateTemplateScript));
    }
    private void OnGUI()
    {

        GUILayout.Label("Create Pool Script");
        objectName = EditorGUILayout.TextField("ClassName", objectName);

        if (GUILayout.Button("Create"))
        {
            if(objectName == string.Empty)
            {
                EditorUtility.DisplayDialog("Warning", "ClassName and ParameterName can't be empty", "OK");
            }else
            {
                try
                {
                    sourceData = File.ReadAllText(Application.absoluteURL + fileName);
         
                    string newdata = Replace(sourceData);

                    var path = EditorUtility.SaveFilePanel("Save script", "", string.Format("{0}.cs", className), "cs");
                    if (path.Length != 0)
                    {
                        File.WriteAllText(path, newdata);
                    }
                }catch(System.Exception ex)
                {
                    EditorUtility.DisplayDialog("Error", ex.ToString(), "OK");
                }
            }

        }

        this.Repaint();
    }

    string Replace(string source)
    {
        string re = "";
        char firstUpper = char.ToUpper(objectName[0]);
        char firstLower = char.ToLower(objectName[0]);

        className = objectName.Length > 1 ? string.Concat(firstUpper, objectName.Substring(1)) : firstUpper.ToString();
        //string functionName = objectName.Length > 1 ? string.Concat(firstUpper, objectName.Substring(1)) : firstUpper.ToString();
        string parameterName = objectName.Length > 1 ? string.Concat(firstLower, objectName.Substring(1)) : firstLower.ToString();

        re = source.Replace(refClassName, className).Replace(refFunctionName,className).Replace(refParameterName, parameterName);

        return re; 
    }

}
