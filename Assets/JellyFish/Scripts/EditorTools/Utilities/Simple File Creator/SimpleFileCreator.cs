/**
 * Created By: Ubaidullah Effendi-Emjedi
 * LinkedIn : https://www.linkedin.com/in/ubaidullah-effendi-emjedi-202494183/
 */

using System;
using System.IO;
using JetBrains.Annotations;
using UnityEngine;

namespace JellyFish.EditorTools.Utilities
{
    public static class SimpleFileCreator
    {
        /// <summary>
        /// Create a file at a path with possible data.
        /// </summary>
        /// <param name="fullPath"></param>
        /// <param name="filename"></param>
        /// <param name="extension"></param>
        /// <param name="data"></param>
        /// <param name="createFileAction"></param>
        public static void CreateFileFromBytesAt(string fullPath, string filename, string extension, [CanBeNull] byte[] data, Action<string, byte[]> createFileAction)
        {
            try
            {
                string completePath = Path.Combine(fullPath, filename, extension);

                if (Directory.Exists(fullPath))
                {
                    createFileAction.Invoke(completePath, data);
                }
                else
                {
                    Directory.CreateDirectory(fullPath);
                    createFileAction.Invoke(completePath, data);
                }
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.ToString());
            }
        }

        /// <summary>
        /// Create a File at a Path
        /// </summary>
        /// <param name="fullPath"></param>
        /// <param name="filename"></param>
        /// <param name="extension"></param>
        public static void CreateFileAt(string fullPath, string filename, string extension, Action<string> createFileAction)
        {
            try
            {
                string completePath = Path.Combine(fullPath, filename, extension);

                if (Directory.Exists(fullPath))
                {
                    createFileAction.Invoke(completePath);
                }
                else
                {
                    Directory.CreateDirectory(fullPath);
                    createFileAction.Invoke(completePath);
                }
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.ToString());
            }
        }

        /// <summary>
        /// Create a File at a Path.
        /// </summary>
        /// <param name="fullPath"></param>
        /// <param name="createFileAction"></param>
        public static void CreateFileFromBytesAt(string fullPath, byte[] data, Action<string, byte[]> createFileAction)
        {
            try
            {
                createFileAction.Invoke(fullPath, data);
            }
            catch (Exception exception)
            {
                Debug.Log(exception.ToString());
            }
        }

        /// <summary>
        /// Create a File at a Path.
        /// </summary>
        /// <param name="fullPath"></param>
        /// <param name="createFileAction"></param>
        public static void CreateFileAt(string fullPath, Action<string> createFileAction)
        {
            try
            {
                createFileAction.Invoke(fullPath);
            }
            catch (Exception exception)
            {
                Debug.Log(exception.ToString());
            }
        }
    }
}