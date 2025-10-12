using UnityEngine;

namespace Commands
{

    public partial class CommandManager : MonoBehaviour

    {
        [Command("test", "Test Command")]
        public void TestCommand()
        {
            Debug.Log("Test Command Invoked");
        }

        [Command("echo", "Write message in console")]
        public void Writemsg(string msg)
        {
            Debug.Log(msg);
        }

        [Command("addMoney", "Adds given amount of currency")]
        // public void AddMoney(int value) => CurrencyManager.Instance.AddCurrency(value); //tego nie masz po prostu

        [Command("help", "Shows help")]
        public void Help()
        {
            foreach (MethodInfo methodInfo in _commands.Values)
            {

                var attributes = methodInfo.GetCustomAttributes<CommandAttribute>().ToList();
                if (attributes.Count == 0)
                    continue;

                foreach(var attribute in attributes)
                {
                    string label = $"{Attribute.CommandName}";

                    foreach (ParameterInfo parameterInfo in methodInfoInfo.GetParameters())
                        label += $"{parameterInfo.Name}:{parameterInfo.ParameterType}>";

                    label += $" | {Attribute.CommandDescription}";

                    Debug.Log(label);
                }
            }

            
        }
    }
}
