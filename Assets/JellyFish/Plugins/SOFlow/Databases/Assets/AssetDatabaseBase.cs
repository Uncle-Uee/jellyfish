// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using System.Collections.Generic;
using SOFlow.Utilities;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SOFlow.Databases
{
    public class AssetDatabaseBase<TData, TSerializableData> : SOFlowScriptableObject
        where TData : IAssetDatabaseData
        where TSerializableData : AssetDatabaseSerializableData<TData>, new()
    {
        /// <summary>
        ///     The list of all data registered to this database.
        /// </summary>
        [HideInInspector]
        public List<TSerializableData> SerializableData = new List<TSerializableData>();

        /// <summary>
        ///     Gets the asset of the given ID from the database.
        /// </summary>
        /// <param name="dataID"></param>
        /// <returns></returns>
        public virtual TData GetDataFromDatabase(string dataID)
        {
            TSerializableData serializableData = SerializableData.Find(data => data.DataID == dataID);

            return serializableData == null ? default : serializableData.DataAsset;
        }

        /// <summary>
        ///     Adds the given data to the database if it is not yet registered within the database.
        /// </summary>
        /// <param name="data"></param>
        public virtual void AddDataToDatabase(TData data)
        {
            string assetID = data?.GetAssetID();

            if(!SerializableData.Exists(_data => _data.DataID == assetID))
            {
                SerializableData.Add(new TSerializableData
                                     {
                                         DataID = assetID, DataAsset = data
                                     });

#if UNITY_EDITOR
                EditorUtility.SetDirty(this);
                AssetDatabase.SaveAssets();
#endif
            }
            else
            {
                Debug.LogWarning($"[{name}]: Data has already been added to the database.\n" +
                                 $"ID: {data?.GetAssetID()}");
            }

            ValidateDatabaseAssets();
        }

        /// <summary>
        ///     Removes the given data from the database.
        /// </summary>
        /// <param name="data"></param>
        public virtual void RemoveDataFromDatabase(TData data)
        {
            string assetID = data?.GetAssetID();

            SerializableData.Remove(SerializableData.Find(_data => _data.DataID == assetID));
            ValidateDatabaseAssets();
        }

        /// <summary>
        ///     Ensures all currently registered data have associated assets.
        /// </summary>
        public virtual void ValidateDatabaseAssets()
        {
            List<TSerializableData> invalidData = new List<TSerializableData>();

            foreach(TSerializableData data in SerializableData)
                try
                {
                    if(data.DataAsset == null || data.DataID != data.DataAsset?.GetAssetID()) invalidData.Add(data);
                }
                catch
                {
                    invalidData.Add(data);
                }

            foreach(TSerializableData data in invalidData) SerializableData.Remove(data);

#if UNITY_EDITOR
            if(invalidData.Count > 0)
            {
                EditorUtility.SetDirty(this);
                AssetDatabase.SaveAssets();
            }
#endif
        }

        /// <summary>
        ///     Checks if the given data is currently registered to the database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual bool IsDataRegistered(TData data)
        {
            string dataID = data?.GetAssetID();

            return SerializableData.Exists(_data => _data.DataID == dataID);
        }
    }
}