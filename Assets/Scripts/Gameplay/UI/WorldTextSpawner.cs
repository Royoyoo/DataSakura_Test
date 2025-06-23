using System;
using Core.Configs;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Core.AssetsSystem;

namespace Gameplay.UI
{
    public class WorldTextSpawner : IDisposable
    {
        private readonly Vector3 worldOffset = new(0f, 0.2f, -1.0f);
        private readonly Vector3 worldRotation = new(90f, 0f, 0f);

        private readonly UiConfig uiConfig;
        private readonly AssetsManager assetManager;


        public WorldTextSpawner(UiConfig uiConfig, AssetsManager assetManager)
        {
            this.uiConfig = uiConfig;
            this.assetManager = assetManager;

            DOTween.defaultTimeScaleIndependent = false;
        }


        public void Dispose()
        {
            DOTween.Kill(this, true);
        }


        public void SpawnWorldText(string test, Transform targetTransform)
        {
            if (targetTransform == null || !targetTransform.gameObject.activeSelf)
            {
                return;
            }

            TMP_Text textObject =
                assetManager.CreateObject<TMP_Text>(uiConfig.WorldTextReference, position: targetTransform.position);

            textObject.text = test;

            textObject.transform.position += worldOffset;
            textObject.transform.rotation = Quaternion.Euler(worldRotation);

            Tween tween = DOVirtual
                          .DelayedCall(1f, null)
                          .SetId(this);

            tween.OnComplete(() => assetManager.DestroyObject(textObject.gameObject));

            tween.OnUpdate(() =>
            {
                if (targetTransform == null || !targetTransform.gameObject.activeSelf)
                {
                    tween.Kill(true);

                    return;
                }

                if (textObject != null)
                {
                    textObject.transform.position = targetTransform.position + worldOffset;
                }
            });
        }
    }
}
