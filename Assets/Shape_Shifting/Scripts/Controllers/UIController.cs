using Zenject;
using System;

namespace ShapeShifting
{
    public class UIController : IInitializable, IDisposable
    {
        [Inject]
        MainPage m_MainPage;
        [Inject]
        GamePage m_GamePage;
        [Inject]
        WinPage m_WinPage;
        [Inject]
        LosePage m_LosePage;
        [Inject]
        LevelBrowserPage m_LevelBrowserPage;

        public void Initialize()
        {
            setupPages();
        }

        public void Dispose()
        {
            
        }

        private void setupPages()
        {
            m_MainPage.Setup();
            m_GamePage.Setup();
            m_WinPage.Setup();
            m_LosePage.Setup();
            m_LevelBrowserPage.Setup();
        }

    }
}