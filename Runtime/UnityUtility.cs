using System.Collections;
using UnityEngine;

public static class UnityUtility  {
	public static void WaitCoroutine(IEnumerator coroutineFunc) {
		while (coroutineFunc.MoveNext ()) {
            if (null == coroutineFunc.Current)
                continue;
            
			IEnumerator yieldedFunc = coroutineFunc.Current as IEnumerator;
            if (null == yieldedFunc) {
                Debug.LogError("Can't cast to IEnumerator: " + coroutineFunc.Current.ToString() );
                return;
            }
			WaitCoroutine (yieldedFunc);
		}
	}
}
