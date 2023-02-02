using Alkacom.Sdk.Common.States;
using I2.Loc;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace Alkacom.Scripts
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    [RequireComponent(typeof(LocalizeDropdown))]
    public class UILevelRenderer : MonoBehaviour
    {
        private TextMeshProUGUI _text;
        private LocalizeDropdown _locDrop;


        [Inject]
        public void Construct(ILevelState levelState)
        {
            _text = GetComponent<TextMeshProUGUI>();
            _locDrop = GetComponent<LocalizeDropdown>();
            
            levelState.Observable().TakeUntilDestroy(this).Subscribe(_ =>
            {
                var prefix = LocalizationManager.GetTranslation(_locDrop._Terms[0]);
                _text.text = $"{prefix} {_}";
            });
        }
      
    }
}