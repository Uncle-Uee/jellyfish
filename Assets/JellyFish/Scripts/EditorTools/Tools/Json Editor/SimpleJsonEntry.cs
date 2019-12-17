/*
 *
 * Created By: Kearan Peterson
 * Alias: BluMalice
 * Modified By: Ubaidullah Effendi-Emjedi, Uee
 *
 * Last Modified: 09 November 2019
 *
 */


using System;

// ReSharper disable once CheckNamespace
namespace JellyFish.EditorTools.JsonEditor
{
    [Serializable]
    public class SimpleJsonEntry
    {
        public enum JsonTypes
        {
            STRING = 0,
            INT    = 1,
            FLOAT  = 2,
            BOOL   = 3,
            LIST   = 4
        }

        /// <summary>
        ///     The key.
        /// </summary>
        public string Key = "";

        /// <summary>
        ///     The list.
        /// </summary>
        public SimpleJsonList List = new SimpleJsonList();

        /// <summary>
        ///     The type.
        /// </summary>
        public JsonTypes Type = JsonTypes.STRING;

        /// <summary>
        ///     The value.
        /// </summary>
        public string Value = "";
    }
}