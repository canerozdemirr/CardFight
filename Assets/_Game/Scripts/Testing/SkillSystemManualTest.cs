using System;
using System.Collections.Generic;
using _Game.Scripts.Interfaces.Skills;
using _Game.Scripts.Interfaces.Systems;
using _Game.Scripts.Interfaces.Players;
using _Game.Scripts.Skills;
using _Game.Scripts.Systems;
using UnityEngine;

namespace _Game.Scripts.Testing
{
    // Simple manual test to validate the skill system
    public class SkillSystemManualTest : MonoBehaviour
    {
        [Header("Test Configuration")]
        [SerializeField] private bool runTestOnStart = false;
        
        private void Start()
        {
            if (runTestOnStart)
            {
                RunBasicSkillTests();
            }
        }
        
        private void RunBasicSkillTests()
        {
            Debug.Log("=== Starting Skill System Manual Tests ===");
            
            try
            {
                TestSkillCreation();
                TestSkillApplication();
                Debug.Log("=== All Skill System Tests Passed! ===");
            }
            catch (Exception e)
            {
                Debug.LogError($"Skill System Test Failed: {e.Message}");
            }
        }
        
        private void TestSkillCreation()
        {
            Debug.Log("Test 1: Creating skills...");
            
            // Create a mock player (would normally be injected)
            MockCardPlayer mockPlayer = new MockCardPlayer();
            
            // Test creating different skill types
            var healthSkill = new HealthBoostSkill(mockPlayer, 25);
            var shieldSkill = new ShieldSkill(mockPlayer, 10);
            var attackSkill = new AttackBoostSkill(mockPlayer, 5);
            var defenseSkill = new DefenseBoostSkill(mockPlayer, 3);
            
            Debug.Log($"Created skills: {healthSkill.SkillName}, {shieldSkill.SkillName}, {attackSkill.SkillName}, {defenseSkill.SkillName}");
        }
        
        private void TestSkillApplication()
        {
            Debug.Log("Test 2: Applying and removing skills...");
            
            MockCardPlayer mockPlayer = new MockCardPlayer();
            var healthSkill = new HealthBoostSkill(mockPlayer, 30);
            
            int initialHealth = mockPlayer.Health.CurrentHealth;
            
            // Apply skill
            healthSkill.Apply();
            int healthAfterSkill = mockPlayer.Health.CurrentHealth;
            
            Debug.Log($"Health before: {initialHealth}, after skill: {healthAfterSkill}");
            
            // Remove skill (though health boost is permanent)
            healthSkill.Remove();
            
            Debug.Log("Skill application test completed");
        }
    }
}