using UnityEditor;
using UnityEngine;

namespace EraSoren.Editor.InputSystem
{
    [CustomPropertyDrawer(typeof(KeyCode))]
    public class KeyCodeDrawer : PropertyDrawer
    {
        private static readonly int KeyListenerHash = "KeyListener".GetHashCode();

        /// <summary>
        /// Used to convert Alpha numbers to mouse buttons
        /// </summary>
        private const int AlphaOffset = KeyCode.Mouse0 - KeyCode.Alpha0;

        private bool _isListening;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            KeyListener(position, property, label);
            EditorGUI.EndProperty();

            /*
            
            var lastValue = (KeyCode) property.intValue;
            
            var buttonText = _isListening ? "Listening..." : lastValue.ToString();
            if (GUI.Button(SecondHalf(position), buttonText))
            {
                _isListening = !_isListening;
            }

            var currentEvent = Event.current;

            if (_isListening)
            {
                if (currentEvent.isKey && currentEvent.type == EventType.KeyDown && currentEvent.keyCode != KeyCode.None)
                {
                    var newValue = currentEvent.keyCode;
                    var offset = 0;

                    if (currentEvent.shift)
                    {
                        if(newValue >= KeyCode.Alpha0 && newValue <= KeyCode.Alpha6)
                            offset = AlphaOffset;
                    }

                    _isListening = false;

                    property.intValue = (int) newValue + offset;
                    EditorUtility.SetDirty(property.serializedObject.targetObject);
                }
            }
            */
        }

        private static void KeyListener(Rect position, SerializedProperty property, GUIContent label)
        {
            //Generate a control ID
            int controlID = GUIUtility.GetControlID(KeyListenerHash, FocusType.Keyboard, position);

            //Create a state that holds information about the current state of the control
            //that would otherwise be lost during a repaint event.
            var state = (KeyScannerInfoState) GUIUtility.GetStateObject(
                typeof(KeyScannerInfoState),
                controlID);

            //Create the style for the value field.
            //This way, we can tell if we are "recording" a keyboard event.
            var style = new GUIStyle(GUI.skin.box); //Create a copy of the default box style
            style.padding = new RectOffset(); //Reset the padding to zero
            style.margin = new RectOffset(); //Reset the margin to zero

            if (state.IsScanning)
            {
                style.active.textColor = Color.red;
                style.normal.textColor = Color.red;
            }

            //Create the content for our style
            //This is what will be displayed in the value field
            GUIContent content = new GUIContent(state.IsScanning ? "Listening..." 
                                                    : ((KeyCode) property.intValue).ToString());

            //if(label != GUIContent.none)
            position = EditorGUI.PrefixLabel(position, controlID, label);


            //The current event being processed by the IMGUI system
            var current = Event.current;
            //Gets the current event type for this specific control
            var eventType = current.GetTypeForControl(controlID);

            switch (eventType)
            {
                case EventType.MouseDown:
                    //Is the cursor within the value rect?
                    if (position.Contains(current.mousePosition))
                    {
                        //Check if no other controls have hotcontrol OR if we are scanning for input
                        if (GUIUtility.hotControl == 0 || state.IsScanning)
                        {
                            //Set hotcontrol to our ID, so other controls know not to listen to events
                            GUIUtility.hotControl = controlID;
                            //Do the same for keyboard control
                            GUIUtility.keyboardControl = controlID;
                            //Consume this event, causing other GUI elements to ignore it.
                            current.Use();
                        }
                    }

                    break;

                case EventType.MouseUp:
                    if (GUIUtility.hotControl == controlID)
                    {
                        //Is the cursor within the value rect?
                        if (position.Contains(current.mousePosition))
                        {
                            //Toggle the scan state
                            state.IsScanning = !state.IsScanning;
                        }
                        else
                        {
                            //If we click somewhere else, disable recording
                            state.IsScanning = false;
                        }

                        //If we are no longer scanning, give back the controls
                        if (!state.IsScanning)
                        {
                            //release hot control so other controls can use it now
                            GUIUtility.keyboardControl = 0;
                            GUIUtility.hotControl = 0;

                            //We changed the input values, so notify the IMGUI that something changed
                            GUI.changed = true;
                        }

                        //Consume the event
                        current.Use();
                    }

                    break;

                case EventType.KeyDown:
                    //If we are not scanning, stop here
                    if (!state.IsScanning) return;
                    //If the current key is not a keyboard key OR the current key is none, stop here
                    if (!current.isKey || current.keyCode == KeyCode.None
                    || current.keyCode == KeyCode.LeftShift) return;

                    var newKey = current.keyCode;
                    var offset = 0;

                    if (current.shift)
                    {
                        // Pressing shift and alpha numbers sets mouse keys
                        if (newKey >= KeyCode.Alpha0 && newKey <= KeyCode.Alpha6)
                            offset = AlphaOffset;

                        // Pressing shift and escape sets none
                        if (newKey == KeyCode.Escape)
                            newKey = KeyCode.None;
                    }

                    state.IsScanning = false;

                    //release hot control so other controls can use it now
                    GUIUtility.keyboardControl = 0;
                    GUIUtility.hotControl = 0;

                    //set the values to the properties
                    property.intValue = (int) newKey + offset;
                    //We changed the input values, so notify the IMGUI that something changed
                    GUI.changed = true;

                    //Consume the event
                    current.Use();

                    EditorUtility.SetDirty(property.serializedObject.targetObject);

                    break;
                case EventType.Repaint:
                    //Draw the style that we made earlier.
                    style.Draw(position, content, controlID, state.IsScanning);
                    break;
            }
        }


        private class KeyScannerInfoState
        {
            public bool IsScanning;
            public KeyCode KeyCode;

            public override string ToString()
            {
                return KeyCode.ToString();
            }

            public void ResetState()
            {
                KeyCode = KeyCode.None;
            }
        }
    }
}