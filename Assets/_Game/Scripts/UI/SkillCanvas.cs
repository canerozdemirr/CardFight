using _Game.Scripts.Events.Skill;
using _Game.Scripts.Interfaces.Events;
using _Game.Scripts.Interfaces.UI;
using GenericEventBus;
using TMPro;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.UI
{
    public class SkillCanvas : MonoBehaviour, IUIElement
    {
        [SerializeField] private TextMeshProUGUI _skillDescriptionText;

        [Inject] private GenericEventBus<IEvent> _eventBus;

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public bool IsVisible => gameObject.activeSelf;

        public void Initialize()
        {
            _eventBus.SubscribeTo<OnSkillApplied>(OnSkillApplied);
            Hide();
        }

        public void Cleanup()
        {
            _eventBus.UnsubscribeFrom<OnSkillApplied>(OnSkillApplied);
        }

        private void OnSkillApplied(ref OnSkillApplied eventData)
        {
            if (eventData.AppliedSkill == null) 
                return;
            
            _skillDescriptionText.SetText(
                $"Skill Applied: {eventData.AppliedSkill.SkillName}\n{eventData.AppliedSkill.Description}");
        }
    }
}