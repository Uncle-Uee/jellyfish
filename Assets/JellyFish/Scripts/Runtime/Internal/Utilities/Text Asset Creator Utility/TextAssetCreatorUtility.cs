/**
 * Created By: Ubaidullah Effendi-Emjedi
 * LinkedIn : https://www.linkedin.com/in/ubaidullah-effendi-emjedi-202494183/
 */
using System.IO;
using System.Text;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace JellyFish.Internal.Utilities
{
    public static class TextAssetCreatorUtility
    {
        #region TEXT ASSET CREATOR METHODS

        /// <summary>
        /// Create and Return a Text Asset.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="filename"></param>
        /// <param name="extension"></param>
        /// <param name="encoding"></param>
        /// <param name="text"></param>
        /// <param name="append"></param>
        /// <returns></returns>
        public static TextAsset CreateTextAsset(string   path,      string filename, string extension, Encoding encoding
                                                , string text = "", bool   append = false)
        {
            using (StreamWriter writer = new StreamWriter($"{path}\\{filename}.{extension}", append, encoding))
            {
                writer.WriteLine(text);
            }

            return Resources.Load<TextAsset>($"{path}\\{filename}.{extension}");
        }

        /// <summary>
        /// Create and Return a Text Asset.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="encoding"></param>
        /// <param name="text"></param>
        /// <param name="append"></param>
        /// <returns></returns>
        public static TextAsset CreateTextAsset(string path, Encoding encoding, string text = "", bool append = false)
        {
            using (StreamWriter writer = new StreamWriter(path, append, encoding))
            {
                writer.WriteLine(text);
            }

            return Resources.Load<TextAsset>(path);
        }

        #endregion
    }
}