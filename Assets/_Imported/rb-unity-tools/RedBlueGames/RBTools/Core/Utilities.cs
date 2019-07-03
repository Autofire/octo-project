﻿namespace RedBlueGames.Tools
{
    using System.Collections;
    using System.IO;
    using UnityEngine;

    /// <summary>
    /// Class that stores generic Utiliities that have no other home.
    /// </summary>
    public static class Utilities
    {
        /// <summary>
        /// Opens the URL in a new window if webplayer build, otherwise uses the behavior
        /// built into Application.OpenURL which depends on the platform.
        /// </summary>
        /// <param name="url">URL to open.</param>
        public static void OpenURL(string url)
        {
            if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
				throw new System.NotImplementedException("The API used for this in WebGL has been deprecated and needs to be replaced.");
                //string evalString = string.Format("window.open('{0}')", url);
                //Application.ExternalEval(evalString);
            }
            else
            {
                Application.OpenURL(url);
            }
        }

        /// <summary>
        /// Copies the string to the OS buffer.
        /// </summary>
        /// <param name="copyString">String to copy.</param>
        public static void CopyStringToBuffer(string copyString)
        {
            TextEditor te = new TextEditor();
            te.text = copyString;
            te.SelectAll();
            te.Copy();
        }

        /// <summary>
        /// Loads a file stored at a path and returns it as a Texure2D in Unity.
        /// </summary>
        /// <returns>The file to texture2D.</returns>
        /// <param name="path">Path to file.</param>
        public static Texture2D ConvertFileToTexture2D(string path)
        {
            Texture2D texture = new Texture2D(0, 0, TextureFormat.ARGB32, false);
            byte[] readBytes;
            try
            {
                readBytes = File.ReadAllBytes(path);
            }
            catch (FileNotFoundException e)
            {
                Debug.LogError("Caught exception when trying to load texture from file: " + e.ToString());
                return null;
            }

            texture.LoadImage(readBytes);
            return texture;
        }

        /// <summary>
        /// Writes a Texture to Disk
        /// </summary>
        /// <param name="texture">Texture to write.</param>
        /// <param name="outputDirectory">Output directory.</param>
        /// <param name="filename">Output filename.</param>
        public static void WriteTextureToDisk(Texture2D texture, string outputDirectory, string filename)
        {
            byte[] bytes = texture.EncodeToPNG();
            string path = outputDirectory + filename;
            Debug.LogWarning("Writing file to disk: " + path);
            try
            {
                System.IO.File.WriteAllBytes(path, bytes);
            }
            catch (System.Exception e)
            {
                Debug.LogWarning("Failed to write. Reason: " + e.Message);
            }
        }
    }
}