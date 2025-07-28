using System.Collections.Generic;
using _Game.Scripts.Interfaces.Commands;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.Utilities.SubSystems
{
    [Serializable]
    public class CommandExecutor : ICommandExecutor
    {
        private readonly Queue<ICommand> _commandQueue = new();
        private readonly DiContainer _container;

        public CommandExecutor()
        {
            
        }
        public CommandExecutor(DiContainer container)
        {
            _container = container;
        }
        
        public void Enqueue(ICommand command)
        {
            if (command == null)
            {
                Debug.LogError("Cannot enqueue null command");
                return;
            }
            
            _container.Inject(command);
            _commandQueue.Enqueue(command);
        }

        public async UniTask ExecuteCommands()
        {
            if (_commandQueue.Count == 0)
            {
                Debug.LogWarning("No commands to execute");
                return;
            }

            int executedCommands = 0;
            int totalCommands = _commandQueue.Count;
            
            try
            {
                while (_commandQueue.Count > 0)
                {
                    var command = _commandQueue.Dequeue();
                    
                    if (command == null)
                    {
                        Debug.LogError($"Null command found at index {executedCommands}. Skipping...");
                        executedCommands++;
                        continue;
                    }

                    try
                    {
                        await command.Execute();
                        executedCommands++;
                    }
                    catch (Exception commandException)
                    {
                        Debug.LogError($"Command execution failed at index {executedCommands}: {commandException.Message}");
                        Debug.LogException(commandException);
                        executedCommands++;
                    }
                }
                
                Debug.Log($"Successfully executed {executedCommands}/{totalCommands} commands");
            }
            catch (Exception generalException)
            {
                Debug.LogError($"Critical error in command execution pipeline: {generalException.Message}");
                Debug.LogException(generalException);
                _commandQueue.Clear();
                throw;
            }
        }
    }
}
