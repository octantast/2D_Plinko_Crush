using UnityEngine;

namespace rIAEugth.vseioAW.Game
{
    public class PlimkoAuer : MonoBehaviour
    {
        public void InitData()
        {
            UniWebView.SetAllowInlinePlay(true);

            var spaceAudios = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
            foreach (var audioSource in spaceAudios)
            {
                audioSource.Stop();
            }

            Screen.autorotateToPortrait = true;
            Screen.autorotateToPortraitUpsideDown = true;
            Screen.orientation = ScreenOrientation.AutoRotation;
        }
    }
}