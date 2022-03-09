using System.Diagnostics;

namespace MagicalFlowers.Common
{
    /// <summary>
    /// ログ出力用のクラス
    /// ビルド時にDebug.Logを呼び出さないようにするためのもの
    /// </summary>
    public static class DebugLogger
    {
        /// <summary>
        /// Unityのコンソールにログを出力する関数
        /// </summary>
        [Conditional("UNITY_EDITOR")]
        public static void Log(object message)
        {
            UnityEngine.Debug.Log(message);
        }
    }
}