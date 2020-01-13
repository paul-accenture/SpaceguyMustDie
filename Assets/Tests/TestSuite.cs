using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class NewTestScript
    {
        // A Test behaves as an ordinary method
        [Test]
        public IEnumerator PlayerMovesWhenGoIsCalled()
        {
            GameObject playerGameObject =
                MonoBehaviour.Instantiate(Resources.Load<GameObject>("Player"));

            player player = playerGameObject.GetComponent<player>();

            player.go();
            yield return new WaitForSeconds(0.1f);

            Assert.Greater(player.GetRigidbody2D().velocity.x, 0);

            Object.Destroy(player.gameObject);
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator NewTestScriptWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
