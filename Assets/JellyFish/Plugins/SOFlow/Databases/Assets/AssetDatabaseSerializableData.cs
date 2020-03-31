// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using System;

namespace SOFlow.Databases
{
    [Serializable]
    public class AssetDatabaseSerializableData<TData> where TData : IAssetDatabaseData
    {
        /// <summary>
        ///     The data asset reference.
        /// </summary>
        public TData DataAsset;

        /// <summary>
        ///     The data ID.
        /// </summary>
        public string DataID;

        public AssetDatabaseSerializableData(string dataId, TData dataAsset)
        {
            DataID    = dataId;
            DataAsset = dataAsset;
        }

        protected AssetDatabaseSerializableData()
        {
        }
    }
}