// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using System.Collections.Generic;
using SOFlow.Utilities;
using UltEvents;
using UnityEngine;

namespace SOFlow.Data.Collections
{
    [CreateAssetMenu(menuName = "SOFlow/Data/Collections/Unity Runtime Set")]
    public class UnityRuntimeSet : SOFlowScriptableObject
    {
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
        ///     The list of items within this set.
        /// </summary>
        [SerializeField]
        private List<Object> _items = new List<Object>();

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
        public void Add(Object item)
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
        public void Remove(Object item)
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
        public bool Exists(System.Predicate<Object> match)
        {
            return _items.Exists(match);
        }

        /// <summary>
        ///     Finds the first occurence of the given item expression within this set.
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public Object Find(System.Predicate<Object> match)
        {
            return _items.Find(match);
        }

        /// <summary>
        ///     Returns the item at the given index in this set.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Object GetIndex(int index)
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
        public void UpdateIndex(int index, Object value)
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
        public void Replace(List<Object> value)
        {
            _items = value;

            CheckSetState();
        }

        /// <summary>
        ///     Returns the items as the specified type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetItems<T>() where T : Object
        {
            return _items.ConvertAll(item => (T)item);
        }

        /// <summary>
        ///     Enables or disables all supported Unity components within the set.
        /// </summary>
        /// <param name="active"></param>
        public void SetComponentsActiveState(bool active)
        {
            foreach(Object item in _items)
                try
                {
                    ((MonoBehaviour)item).enabled = active;
                }
                catch
                {
                    // Ignore if the item is not supported.
                }
        }

        /// <summary>
        ///     Updates the set using the given action.
        /// </summary>
        /// <param name="action"></param>
        public void UpdateSet(System.Action<List<Object>> action)
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