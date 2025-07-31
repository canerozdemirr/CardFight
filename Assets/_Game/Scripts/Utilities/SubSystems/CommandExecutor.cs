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
        
        private int _totalCommandsExecuted;
        private int _currentCommandIndex;

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

            _currentCommandIndex = 0;
            _totalCommandsExecuted = _commandQueue.Count;

            try
            {
                while (_commandQueue.Count > 0)
                {
                    ICommand command = _commandQueue.Dequeue();

                    if (command == null)
                    {
                        Debug.LogError($"Null command found at index {_currentCommandIndex}. Skipping...");
                        _currentCommandIndex++;
                        continue;
                    }

                    try
                    {
                        await command.Execute();
                        _currentCommandIndex++;
                    }
                    catch (Exception commandException)
                    {
                        Debug.LogError(
                            $"Command execution failed at index {_currentCommandIndex}: {commandException.Message}");
                        Debug.LogException(commandException);
                        _currentCommandIndex++;
                    }
                }

                Debug.Log($"Successfully executed {_currentCommandIndex}/{_totalCommandsExecuted} commands");
            }
            catch (Exception generalException)
            {
                Debug.LogError($"Critical error in command execution pipeline: {generalException.Message}");
                Debug.LogException(generalException);
                _commandQueue.Clear();
                throw;
            }
            finally
            {
                _currentCommandIndex = 0;
                _totalCommandsExecuted = 0;
            }
        }
    }
}
