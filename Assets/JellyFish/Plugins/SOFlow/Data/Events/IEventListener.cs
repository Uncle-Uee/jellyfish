// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using System;
using System.Collections.Generic;
using UnityEngine;

namespace SOFlow.Data.Events
{
    public interface IEventListener
    {
        /// <summary>
        ///     Invokes the response when the associated game event is raised.
        /// </summary>
        /// <param name="raisedEvent"></param>
        void OnEventRaised(GameEvent raisedEvent);

        /// <summary>
        ///     Returns the game object associated with this listener.
        /// </summary>
        /// <returns></returns>
        GameObject GetGameObject();

        /// <summary>
        ///     Returns the object type associated with this listener.
        /// </summary>
        /// <returns></returns>
        Type GetObjectType();

        /// <summary>
        ///     Returns all events registered to this listener.
        /// </summary>
        /// <returns></returns>
        List<GameEvent> GetEvents();
    }
}