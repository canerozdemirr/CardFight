using System.Collections;
using _Game.Scripts.Interfaces.Systems;
using _Game.Scripts.Interfaces.Skills;
using _Game.Scripts.Skills;
using _Game.Scripts.Testing;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.Gameplay
{
    /// <summary>
    /// Demonstration component showing how to use the Skill System
    /// This can be attached to a GameObject to test skill functionality
    /// </summary>
    public class SkillSystemDemo : MonoBehaviour
    {
        [Header("Demo Configuration")]
        [SerializeField] private bool autoStartDemo = false;
        [SerializeField] private float skillApplicationInterval = 3f;
        
        [Inject]
        private ISkillSystem _skillSystem;
        
        private MockCardPlayer _demoPlayer;
        private Coroutine _demoCoroutine;

        private void Start()
        {
            if (autoStartDemo)
            {
                StartDemo();
            }
        }

        [ContextMenu("Start Skill Demo")]
        public void StartDemo()
        {
            if (_demoCoroutine != null)
            {
                StopCoroutine(_demoCoroutine);
            }
            
            // Create a demo player
            _demoPlayer = new MockCardPlayer(100);
            
            Debug.Log("=== Starting Skill System Demo ===");
            Debug.Log($"Demo Player Initial Health: {_demoPlayer.Health.CurrentHealth}/{_demoPlayer.Health.MaxHealth}");
            
            _demoCoroutine = StartCoroutine(RunSkillDemo());
        }

        [ContextMenu("Stop Skill Demo")]
        public void StopDemo()
        {
            if (_demoCoroutine != null)
            {
                StopCoroutine(_demoCoroutine);
                _demoCoroutine = null;
            }
            
            Debug.Log("=== Skill System Demo Stopped ===");
        }

        private IEnumerator RunSkillDemo()
        {
            yield return new WaitForSeconds(1f);
            
            // Demonstrate different skill types
            yield return StartCoroutine(DemoHealthBoost());
            yield return new WaitForSeconds(skillApplicationInterval);
            
            yield return StartCoroutine(DemoShield());
            yield return new WaitForSeconds(skillApplicationInterval);
            
            yield return StartCoroutine(DemoAttackBoost());
            yield return new WaitForSeconds(skillApplicationInterval);
            
            yield return StartCoroutine(DemoDefenseBoost());
            yield return new WaitForSeconds(skillApplicationInterval);
            
            yield return StartCoroutine(DemoRandomSkillPicking());
            
            Debug.Log("=== Skill System Demo Complete ===");
        }

        private IEnumerator DemoHealthBoost()
        {
            Debug.Log("--- Demonstrating Health Boost Skill ---");
            
            var skill = new HealthBoostSkill(_demoPlayer, 25);
            LogPlayerStats("Before Health Boost");
            
            skill.Apply();
            LogPlayerStats("After Health Boost");
            
            yield return null;
        }

        private IEnumerator DemoShield()
        {
            Debug.Log("--- Demonstrating Shield Skill ---");
            
            var skill = new ShieldSkill(_demoPlayer, 20);
            LogPlayerStats("Before Shield");
            
            skill.Apply();
            LogPlayerStats("After Shield Applied");
            
            yield return new WaitForSeconds(1f);
            
            skill.Remove();
            LogPlayerStats("After Shield Removed");
            
            yield return null;
        }

        private IEnumerator DemoAttackBoost()
        {
            Debug.Log("--- Demonstrating Attack Boost Skill ---");
            
            var skill = new AttackBoostSkill(_demoPlayer, 15);
            skill.Apply();
            
            // Simulate card being played
            yield return new WaitForSeconds(0.5f);
            _demoPlayer.OnCardPlayed?.Invoke(null);
            
            yield return null;
        }

        private IEnumerator DemoDefenseBoost()
        {
            Debug.Log("--- Demonstrating Defense Boost Skill ---");
            
            var skill = new DefenseBoostSkill(_demoPlayer, 12);
            skill.Apply();
            
            // Simulate card being played
            yield return new WaitForSeconds(0.5f);
            _demoPlayer.OnCardPlayed?.Invoke(null);
            
            yield return null;
        }

        private IEnumerator DemoRandomSkillPicking()
        {
            Debug.Log("--- Demonstrating Random Skill Picking ---");
            
            if (_skillSystem != null)
            {
                for (int i = 0; i < 3; i++)
                {
                    var randomSkill = _skillSystem.PickRandomSkill();
                    if (randomSkill != null)
                    {
                        Debug.Log($"Picked random skill: {randomSkill.SkillName} - {randomSkill.Description}");
                        _skillSystem.AddSkill(randomSkill);
                    }
                    yield return new WaitForSeconds(1f);
                }
                
                Debug.Log($"Active skills count: {_skillSystem.ActiveSkills.Count}");
            }
            else
            {
                Debug.LogWarning("SkillSystem not injected - make sure this object is instantiated through Zenject");
            }
            
            yield return null;
        }

        private void LogPlayerStats(string label)
        {
            Debug.Log($"{label}: Health = {_demoPlayer.Health.CurrentHealth}/{_demoPlayer.Health.MaxHealth}");
        }

        private void OnDestroy()
        {
            StopDemo();
        }
    }
}