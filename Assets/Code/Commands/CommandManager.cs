using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Commands
{
    public partial class CommandManager : MonoBehaviour
    {
        private string _input;

        private bool _show;
        private int _yPos = 0;

        private Dictionary<string, MethodInfo> _commands = new();

        private void Awake()
        {

            InputSystem.actions.FindAction("enter").started += OnInvokeCommand;

            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (Assembly assembly in assemblies)
            {
                foreach(MethodInfo methodInfo in assembly.GetTypes().SelectMany(t => t.GetMethods()))
                {
                    var attributes = methodInfo.GetCustomAttributes<CommandAttribute>().ToList();

                    foreach(CommandAttribute attribute in attributes)
                    {
                        Debug.Log($"{attribute.CommandName} | { methodInfo.Name}");
                        _commands.Add(attribute.CommandName, methodInfo);
                    }
                }
            }
        }

        private void OnInvokeCommand(InputAction.CallbackContext obj)
        {
            if (!_show) return;
            Debug.Log("HandleCommands");

            var tokens = _input.Split(' ');
            var parameterTokens = tokens.Skip(1).ToArray();

            if(tokens.Length == 0)
            return; 

            if (!_commands.TryGetValue(tokens[0], out var methodInfo))
            {
                Debug.Log($"Command \"{tokens[0]}\" doesnt exist.");
                return;
                    
            }

            ParameterInfo[] parameterInfos = methodInfo.GetParameters();

            if(parameterInfos.Length != parameterTokens.Length)
            {
                Debug.LogError($"Error while handling command \"{tokens[0]}\". Expected {parameterInfos.Length} paramateres, got {parameterTokens.Length}");
                return;
            }

            List<object> invocationParams = new();
            for (int i = 0; i <parameterInfos.Length; i++)
            {
                var parameterInfo = parameterInfos[i];
                invocationParams.Add(Convert.ChangeType(parameterTokens[i], parameterInfo.ParameterType));
            }

            methodInfo.Invoke(this, invocationParams.ToArray());

            _input = "";
        }

        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P)) ;
            _show = true;
        }

        private void OnGUI()
        {
            if (!_show) return;

            GUI.Box(new Rect(0, _yPos, Screen.width, 30), "");
            var originalBackgroundColor = GUI.backgroundColor;
            GUI.backgroundColor = new Color(0f, 0f, 0f);

            _input = GUI.TextField(new Rect(10f, _yPos + 5f, Screen.width - 100f, 20f), _input);

            GUI.backgroundColor = originalBackgroundColor;
            if (GUI.Button(new Rect(Screen.width - 100f, _yPos + 5f, 100f, 20f), "Close"))
                _show = false;

        }
    }
}
