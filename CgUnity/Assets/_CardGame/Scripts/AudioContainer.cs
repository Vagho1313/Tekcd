using UnityEngine;

namespace CardGame
{
    public class AudioContainer : BaseGameController
    {
        public void PlayCardFlipping()
        {
            if (Container.GameConfig.GetAudio(AudioType.CardFlipping, out AudioSource audioSource))
            {
                audioSource.Play();
            }
        }

        public void PlayMatching()
        {
            if (Container.GameConfig.GetAudio(AudioType.Matching, out AudioSource audioSource))
            {
                audioSource.Play();
            }
        }

        public void PlayMismatching()
        {
            if (Container.GameConfig.GetAudio(AudioType.Mismatching, out AudioSource audioSource))
            {
                audioSource.Play();
            }
        }

        public void PlayGameWin()
        {
            if (Container.GameConfig.GetAudio(AudioType.Win, out AudioSource audioSource))
            {
                audioSource.Play();
            }
        }
    }
}
