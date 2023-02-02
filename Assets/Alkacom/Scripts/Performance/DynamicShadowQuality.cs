using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Alkacom.Game
{
    public class DynamicShadowQuality : MonoBehaviour
    {
        [SerializeField] private UniversalRenderPipelineAsset data;

        [SerializeField] private float targetFPS = 60;

        [SerializeField] private float collectionSec = 5.0f;
        [SerializeField] private float percentTolerence = 0.8f;
        [SerializeField] private int startQuality = 2;
        private float _timeleft;
        private float _accum;
        private int _frames;
        private int _qualityIndex;

        // Start is called before the first frame update
        void Start()
        {
            _qualityIndex = 0;
            
            _timeleft = collectionSec;
            Application.targetFrameRate = 60;
            
            UpdateShadowQuality();
        }

        // Update is called once per frame
        void Update()
        {
            _timeleft -= Time.deltaTime;
            _accum += Time.timeScale/Time.deltaTime;
            ++_frames;

            if (_timeleft <= 0.0)
            {
                var fps = (_accum / _frames);
                _timeleft = _accum;
                _accum = 0;
                _frames = 0;

                if (fps * percentTolerence < targetFPS)
                {
                    DecreaseQualtiy();
                }
            }
        }

        private void DecreaseQualtiy()
        {
            _qualityIndex++;
            UpdateShadowQuality();
        }

        private void UpdateShadowQuality()
        {
        //    data.mainLightShadowmapResolution = ;
        }
    }

}