using System;
using UnityEngine;
using Zenject;

namespace WhackAMole
{
    public abstract class LoggerBase : IInitializable, IDisposable
    {
        #region Fields
        [Inject]
        protected SignalBus SignalBus;
        private string m_LoggerName;
        private string m_LogHtmlStringRGB;

        #endregion

        #region Constructors

        public LoggerBase(string i_LoggerName, Color i_LogColor)
        {
            m_LoggerName = i_LoggerName;
            m_LogHtmlStringRGB = ColorUtility.ToHtmlStringRGB(i_LogColor);
        }

        #endregion

        #region Lifecycle Methods

        public virtual void Initialize()
        {

        }
        public virtual void Dispose()
        {

        }

        #endregion

        #region Log Methods

        protected void Log(string i_Message)
        {
            Debug.Log($"<color=#{m_LogHtmlStringRGB}>[{m_LoggerName}]</color> {i_Message}");
        }

        #endregion
    }
}

