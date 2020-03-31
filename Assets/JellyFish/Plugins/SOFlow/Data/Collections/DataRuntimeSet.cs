// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using System;
using System.Collections.Generic;
using SOFlow.Utilities;
using UltEvents;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SOFlow.Data.Collections
{
    [CreateAssetMenu(menuName = "SOFlow/Data/Collections/Data Runtime Set")]
    public class DataRuntimeSet : SOFlowScriptableObject
    {
        /// <summary>
        ///     The list of items within this set.
        /// </summary>
        private List<object> _items = new List<object>();

        /// <summary>
        ///     Event raised when data is added to the list.
        /// </summary>
        public UltEvent OnDataAddedToList = new UltEvent();

        /// <summary>
        ///     Event raised when data is removed from the list.
        /// </summary>
        public UltEvent OnDataRemovedFromList = new UltEvent();

        /// <summary>
        ///     Event raised when the list has changed.
        /// </summary>
        public UltEvent OnListChanged = new UltEvent();

        /// <summary>
        ///     Event raised when this set becomes empty.
        /// </summary>
        public UltEvent OnListEmpty = new UltEvent();

        /// <summary>
        ///     The current item count.
        /// </summary>
        public int CurrentItemCount
        {
            get;
            private set;
        }

        /// <summary>
        ///     Adds an item to this set.
        /// </summary>
        /// <param name="item"></param>
        public void Add(object item)
        {
            if(!_items.Contains(item))
            {
                _items.Add(item);

                CheckSetState();
            }
        }

        /// <summary>
        ///     Removes an item from this set.
        /// </summary>
        /// <param name="item"></param>
        public void Remove(object item)
        {
            if(_items.Remove(item)) CheckSetState();
        }

        /// <summary>
        ///     Removes all items from the this set.
        /// </summary>
        public void Clear()
        {
            _items.Clear();

            CheckSetState();
        }

        /// <summary>
        ///     Checks if the given item expression exists within this set.
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public bool Exists(Predicate<object> match)
        {
            return _items.Exists(match);
        }

        /// <summary>
        ///     Finds the first occurence of the given item expression within this set.
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public object Find(Predicate<object> match)
        {
            return _items.Find(match);
        }

        /// <summary>
        ///     Returns the item at the given index in this set.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public object GetIndex(int index)
        {
            try
            {
                return _items[index];
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        ///     Updates the item at the given index in this set.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public void UpdateIndex(int index, object value)
        {
            _items[index] = value;
        }

        /// <summary>
        ///     Copy the contents of the other set into this set.
        /// </summary>
        /// <param name="otherSet"></param>
        public void Copy(UnityRuntimeSet otherSet)
        {
            otherSet.UpdateSet(set =>
                               {
                                   foreach(Object item in set) Add(item);
                               });
        }

        /// <summary>
        ///     Duplicates the contents of the provided set into this set.
        /// </summary>
        /// <param name="otherSet"></param>
        public void Duplicate(UnityRuntimeSet otherSet)
        {
            _items.Clear();
            Copy(otherSet);
        }

        /// <summary>
        ///     Replaces the contents of this set with the provided data.
        /// </summary>
        /// <param name="value"></param>
        public void Replace(List<object> value)
        {
            _items = value;

            CheckSetState();
        }

        /// <summary>
        ///     Returns the items as the specified type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetItems<T>()
        {
            return _items.ConvertAll(item => (T)item);
        }

        /// <summary>
        ///     Updates the set using the given action.
        /// </summary>
        /// <param name="action"></param>
        public void UpdateSet(Action<List<object>> action)
        {
            action.Invoke(_items);

            CheckSetState();
        }

        /// <summary>
        ///     Checks the current state of the set.
        /// </summary>
        private void CheckSetState()
        {
            int lastItemCount = _items.Count;

            if(CurrentItemCount < lastItemCount)
            {
                OnDataRemovedFromList.Invoke();
                OnListChanged.Invoke();

                if(lastItemCount <= 0) OnListEmpty.Invoke();
            }
            else if(CurrentItemCount > lastItemCount)
            {
                OnDataAddedToList.Invoke();
                OnListChanged.Invoke();
            }

            CurrentItemCount = lastItemCount;
        }
    }
}