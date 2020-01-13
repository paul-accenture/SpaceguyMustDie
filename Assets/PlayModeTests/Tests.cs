using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class Tests
    {
        // A Test behaves as an ordinary method
        [UnityTest]
        public IEnumerator PlayerFallsFromGravity()
        {
            GameObject playerGameObject =
                MonoBehaviour.Instantiate(Resources.Load<GameObject>("Player"));

            GameObject gameObject =
                MonoBehaviour.Instantiate(Resources.Load<GameObject>("gameState"));

            GameObject maintTextObject =
                MonoBehaviour.Instantiate(Resources.Load<GameObject>("MainText"));

            GameObject powersTextObject =
                MonoBehaviour.Instantiate(Resources.Load<GameObject>("powersText"));

            GameObject imageObject =
                MonoBehaviour.Instantiate(Resources.Load<GameObject>("Image"));

            GameObject textObject =
                MonoBehaviour.Instantiate(Resources.Load<GameObject>("Text"));

            GameObject overlayObject =
                MonoBehaviour.Instantiate(Resources.Load<GameObject>("overlay"));

            player player = playerGameObject.GetComponent<player>();

            yield return new WaitForSeconds(1f);

            Assert.Less(player.GetRigidbody2D().velocity.y, 0);

            Object.Destroy(player.gameObject);
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator TestsWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
