﻿/*
 *
 * Created By: Ubaidullah Effendi-Emjedi, Uee
 * Alias: Uee
 * Modified By:
 *
 * Last Modified: 09 November 2019
 *
 *
 * This software is released under the terms of the
 * GNU license. See https://www.gnu.org/licenses/#GPL
 * for more information.
 *
 *
 * Important
 * Whatever Artwork is used in conjunction with this tool and is extracted using this tool has no 
 * affiliation with this tool or its creator.
 * 
 * The Artwork(s) belong to their respective creators (the individuals that made them) 
 * and the user of this tool should adhere to the respective licenses stipulated by the 
 * owners/creators of the Artwork.
 *
 */

using System;
using System.IO;
using UnityEngine;
using Object = UnityEngine.Object;
#if UNITY_EDITOR
using UnityEditor;

// ReSharper disable once CheckNamespace
namespace JellyFish.EditorTools.SpriteExtractor
{
    [Serializable]
    public class SpriteExtractor
    {
        #region VARIALBES

        private Object[]  _subSprites;       // Object Array of sub sprites from the Sprite Sheet.
        private Texture2D _subTexture;       // Required to store a single Sub Sprite Texture
        private Rect      _subTextureRect;   // Required Rect to get the Dimensions of the Sub Sprite Texture
        private Texture2D _extractedTexture; // The extracted Sub Sprite that is saved as a .png
        private byte[]    data;

        public Texture2D SpriteSheet; // Sprite Sheet With Sub Sprites

        #endregion


        #region PROPERTIES

        public string SpriteSheetPath { set; get; } // Sprite Sheet Path
        public string SavePath        { set; get; } // Path to Where the Extracted Sub Sprites should be saved
        public string Extension       { set; get; } // Extension of the Original Image.

        #endregion

        /// <summary>
        ///     Extract the Sub Sprites of a Sprite Sheet using the Metadata generated by using the Unity Sprite Editor.
        /// </summary>
        public void ExtractSprites()
        {
            // Get The meta Data of the Sub Sprite Artwork.
            _subSprites = AssetDatabase.LoadAllAssetRepresentationsAtPath(SpriteSheetPath);

            for (int i = 0; i < _subSprites.Length; i++)
            {
                _subTexture     = ((Sprite) _subSprites[i]).texture;
                _subTextureRect = ((Sprite) _subSprites[i]).textureRect;

                _extractedTexture = _subTexture.CropTexture2D((int) _subTextureRect.x, (int) _subTextureRect.y,
                                                              (int) _subTextureRect.width,
                                                              (int) _subTextureRect.height);

                data = _extractedTexture.EncodeToPNG();
                File.WriteAllBytes(SavePath.Combine("/", ((Sprite) _subSprites[i]).name, ".png"), data);
            }

            AssetDatabase.Refresh();
        }
    }
}
#endif