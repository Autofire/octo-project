﻿namespace RedBlueGames.Tools
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// Exposes AnimClipBuilder to the Editor, allowing quick creation of an Anim Clip from a spritesheet
    /// </summary>
    public class AnimClipBuilderTool
    {
        private const string CreateClipFromSpritesMenuPath = RBToolsMenuPaths.AnimationUtilitiesBase +
                                                             RBToolsMenuPaths.AnimationClipSubmenu +
                                                             "Create Clip from Sprites";

        /// <summary>
        /// Create an anim clip from a selected spritesheet
        /// </summary>
        [MenuItem(CreateClipFromSpritesMenuPath)]
        public static void CreateAnimClip()
        {
            Texture2D selectedTexture = (Texture2D)Selection.activeObject;
            Sprite[] sprites = GetSpritesFromTexture(selectedTexture);

            string filename = selectedTexture.name + ".anim";
            AnimationClip clip = AnimationClipUtility.CreateClip(sprites, filename);

            string fullClipPath = GetSavePathFromTexture(selectedTexture, filename);

            try
            {
                SaveClip(clip, fullClipPath, false);
            }
            catch (System.IO.IOException)
            {
                var errorMessage = string.Format(
                                       "This will overwrite the existing clip, {0}. Are you sure you want to create the clip?",
                                       filename);
                if (EditorUtility.DisplayDialog("Warning: File Exists", errorMessage, "Yes", "No"))
                {
                    SaveClip(clip, fullClipPath, true);
                }
            }
        }

        private static Sprite[] GetSpritesFromTexture(Texture2D texture)
        {
            string path = AssetDatabase.GetAssetPath(texture);
            if (string.IsNullOrEmpty(path))
            {
                Debug.LogWarning("Can't find sprites from Texture at path: " + path);
                return null;
            }

            // Load all the sprites from the texture path
            // (Note, this does not load them in the order they appear in editor so they must be sorted)
            Sprite[] spriteArray = AssetDatabase.LoadAllAssetsAtPath(path).OfType<Sprite>().ToArray();

            // Sort the spritelist in editor order
            List<Sprite> spriteList = new List<Sprite>(spriteArray);
            spriteList.Sort(delegate(Sprite x, Sprite y)
                {
                    return EditorUtility.NaturalCompare(x.name, y.name);
                });

            return spriteList.ToArray();
        }

        private static string GetSavePathFromTexture(Texture2D selectedTexture, string filename)
        {
            string texturePath = AssetDatabase.GetAssetPath(selectedTexture);
            string textureDirectory = System.IO.Path.GetDirectoryName(texturePath) + System.IO.Path.DirectorySeparatorChar;
            string fullClipPath = textureDirectory + filename;

            return fullClipPath;
        }

        private static void SaveClip(AnimationClip clip, string path, bool allowOverride)
        {
            if (!allowOverride && System.IO.File.Exists(path))
            {
                throw new System.IO.IOException("Clip already exists at path");
            }
            else
            {
                AssetDatabase.CreateAsset(clip, path);
            }
        }

        [MenuItem(CreateClipFromSpritesMenuPath, true)]
        private static bool ValidateSelect()
        {
            if (!IsSelectionValidTexture())
            {
                return false;
            }

            if (Selection.objects.Length > 1)
            {
                return false;
            }

            return true;
        }

        private static bool IsSelectionValidTexture()
        {
            if (Selection.activeObject == null)
            {
                return false;
            }

            return Selection.activeObject.GetType() == typeof(Texture2D);
        }
    }
}