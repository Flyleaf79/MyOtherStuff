using UnityEditor;
using UnityEngine;
using System.Collections;
//using Hernan;
//
public class PearlTools : MonoBehaviour
{

    [MenuItem("Pearl Tools/TEST")]
    static void DoSomething()
    {
        Debug.Log("Test Button");
    }

    [MenuItem("Pearl Tools/Test2")]
    static void LogTest2()
    {
        Debug.Log("Test 2 validation " + Selection.activeTransform.gameObject.name);
    }

    #region MenuItem Help
    /*
     public MenuItem(string itemName, bool isValidateFunction, int priority);
     Creates a menu item and invokes the static function following it, when the menu item is selected.
     The itemName is the menu item represented like a pathname. Eg.
     "GameObject/Do Something" If isValidateFunction is true, this is a validation function and will be called before invoking the 
     menu function with the same name. Priority defines the order by which menu items are displayed in the menu bar.
    */
    #endregion

    [MenuItem("Pearl Tools/Test2", true)]
    static bool ValidateTest2()
    {
        // Return false if no transform is selected
        return Selection.activeTransform != null;
    }

    [MenuItem("Pearl Tools/Test3 %g")]
    static void Test3HotKey()
    {
        Debug.Log("Hello im test3");
    }

    //Context is the the seettings on the side when you press the rigidbody. (Gears)
    // This will double the mass of the object.

    [MenuItem("CONTEXT/Rigidbody/Double Mass")]
    static void DoubleMass(MenuCommand command)
    {
        Rigidbody body = (Rigidbody)command.context;
        body.mass = body.mass * 2;
        Debug.Log("Doubled Rigidbody's Mass to " + body.mass + " from Context Menu.");
    }

    // Add a menu item to create custom GameObjects.
    // Priority 1 ensures it is grouped with the other menu items of the same kind
    // and propagated to the hierarchy dropdown and hierarch context menus. 
    [MenuItem("Pearl Tools/Create Unit", false, 10)]
    static void CreateCustomGameObject(MenuCommand command)
    {
        // Create a custom game object
        GameObject go = new GameObject("New Unit");
        go.layer = 10;
        // Ensures it gets reparented if this was a context click (otherwise does nothing)
        GameObjectUtility.SetParentAndAlign(go, command.context as GameObject);
        // Register the creation in the undo System
        Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
    }

    [MenuItem("Pearl Tools/Create Item", false, 10)]
    static void CreateCustomItem(MenuCommand command)
    {

        GameObject go = new GameObject("New Item");
        go.tag = "Item";
        go.AddComponent<ItemStats>();

        GameObjectUtility.SetParentAndAlign(go, command.context as GameObject);
        Undo.RegisterCompleteObjectUndo(go, "Create " + go.name);
    }

    [MenuItem("Pearl Tools/Create Buff", false, 10)]
    static void CreateCustomBuff(MenuCommand command)
    {

        GameObject go = new GameObject("New Buff");
        go.tag = "Buff";
        go.AddComponent<BuffHandler>();
        Debug.Log("Dont forget to create a buff");

        GameObjectUtility.SetParentAndAlign(go, command.context as GameObject);
        Undo.RegisterCompleteObjectUndo(go, "Create " + go.name);
    }
}
