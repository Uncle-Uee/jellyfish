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
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace JellyFish.EditorTools.JsonEditor
{
    [Serializable]
    public class SimpleJsonList
    {
        /// <summary>
        ///     The entries.
        /// </summary>
        public List<SimpleJsonEntry> Entries = new List<SimpleJsonEntry>();


        /// <summary>
        /// Expand a List.
        /// </summary>
        public bool IsExpanded = true;

        /// <summary>
        ///     Returns the json representation of this object.
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            string json = "{";

            for (int i = 0; i < Entries.Count; i++)
            {
                SimpleJsonEntry entry = Entries[i];

                json += $"\"{entry.Key}\":";

                if (entry.Type == SimpleJsonEntry.JsonTypes.STRING)
                    json += $"\"{entry.Value}\"";
                else if (entry.Type == SimpleJsonEntry.JsonTypes.BOOL)
                    json += $"{entry.Value.ToLower()}";
                else if (entry.Type == SimpleJsonEntry.JsonTypes.LIST)
                    json += entry.List.ToJson();
                else
                    json += $"{entry.Value}";

                if (i < Entries.Count - 1) json += ",";
            }

            json += "}";

            return json;
        }
    }
}