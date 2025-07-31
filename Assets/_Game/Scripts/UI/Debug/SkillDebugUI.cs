using System.Text;
using _Game.Scripts.Interfaces.Systems;
using _Game.Scripts.Interfaces.Players;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Game.Scripts.UI.Debug
{
    /// <summary>
    /// Simple debug UI to display active skills in the game
    /// </summary>
    public class SkillDebugUI : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private Text skillsText;
        [SerializeField] private Button applyRandomSkillButton;
        [SerializeField] private Button clearAllSkillsButton;
        
        [Header("Settings")]
        [SerializeField] private bool updateUI = true;
        [SerializeField] private float updateInterval = 1f;
        
        [Inject]
        private ISkillSystem _skillSystem;
        
        [Inject]
        private ICombatRegister _combatRegister;
        
        private float _lastUpdateTime;

        private void Start()
        {
            SetupButtons();
        }

        private void Update()
        {
            if (updateUI && Time.time - _lastUpdateTime > updateInterval)
            {
                UpdateSkillsDisplay();
                _lastUpdateTime = Time.time;
            }
        }

        private void SetupButtons()
        {
            if (applyRandomSkillButton != null)
            {
                applyRandomSkillButton.onClick.AddListener(() => {
                    if (_skillSystem != null && _combatRegister != null)
                    {
                        var players = _combatRegister.RegisteredPlayers;
                        if (players.Count > 0)
                        {
                            // Use the first player as the skill owner for debug purposes
                            var skillOwner = players[0];
                            _skillSystem.ApplyRandomSkill(skillOwner);
                            Debug.Log($"Applied random skill via debug UI for player: {skillOwner.name}");
                        }
                        else
                        {
                            Debug.LogWarning("No registered players available for skill application");
                        }
                    }
                });
            }

            if (clearAllSkillsButton != null)
            {
                clearAllSkillsButton.onClick.AddListener(() => {
                    if (_skillSystem != null)
                    {
                        var activeSkills = _skillSystem.ActiveSkills;
                        for (int i = activeSkills.Count - 1; i >= 0; i--)
                        {
                            _skillSystem.RemoveSkill(activeSkills[i]);
                        }
                        Debug.Log("Cleared all skills via debug UI");
                    }
                });
            }
        }

        private void UpdateSkillsDisplay()
        {
            if (skillsText == null || _skillSystem == null)
                return;

            var activeSkills = _skillSystem.ActiveSkills;
            
            if (activeSkills.Count == 0)
            {
                skillsText.text = "No active skills";
                return;
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Active Skills ({activeSkills.Count}):");
            
            for (int i = 0; i < activeSkills.Count; i++)
            {
                var skill = activeSkills[i];
                sb.AppendLine($"{i + 1}. {skill.SkillName}");
            }

            skillsText.text = sb.ToString();
        }

        [ContextMenu("Apply Random Skill")]
        public void ApplyRandomSkillManual()
        {
            if (_skillSystem != null && _combatRegister != null)
            {
                var players = _combatRegister.RegisteredPlayers;
                if (players.Count > 0)
                {
                    // Use the first player as the skill owner for debug purposes
                    var skillOwner = players[0];
                    _skillSystem.ApplyRandomSkill(skillOwner);
                }
            }
        }

        [ContextMenu("Clear All Skills")]
        public void ClearAllSkillsManual()
        {
            if (_skillSystem != null)
            {
                var activeSkills = _skillSystem.ActiveSkills;
                for (int i = activeSkills.Count - 1; i >= 0; i--)
                {
                    _skillSystem.RemoveSkill(activeSkills[i]);
                }
            }
        }
    }
}