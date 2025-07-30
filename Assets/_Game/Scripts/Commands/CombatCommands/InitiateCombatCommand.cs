namespace _Game.Scripts.Commands.CombatCommands
{
    using System;
    using Cysharp.Threading.Tasks;
    using Interfaces.Commands;
    using Interfaces.Systems;
    using Zenject;

    [Serializable]
    public class InitiateCombatCommand : ICommand
    {
        [Inject]
        private ICombatSystem _combatSystem;
        
        public UniTask Execute()
        {
            _combatSystem.ResolveCombat();
            return UniTask.CompletedTask;
        }
    }
}