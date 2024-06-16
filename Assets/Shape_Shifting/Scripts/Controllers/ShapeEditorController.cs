using System;
using Zenject;
using Cinemachine;
using UnityEngine;

namespace ShapeShifting
{
    public class ShapeEditorController : IInitializable, IDisposable
    {
        [Inject]
        SignalBus m_SignalBus;
        [Inject]
        BlobGroup m_BlobGroup;
        [Inject(Id = "Shape_Editor")]
        CinemachineVirtualCamera m_ShapeEditorCamera;
        [Inject(Id = "Shape_Follower")]
        CinemachineVirtualCamera m_ShapeFollowerCamera;

        public void Initialize()
        {
            subscribeSignals();
        }
        public void Dispose()
        {
            unsubscribeSignals();
        }


        private void subscribeSignals()
        {
            m_SignalBus.Subscribe<GameStartedSignal>(onGameStarted);
            m_SignalBus.Subscribe<ExitShapeEditorCommandSignal>(onExitShapeEditorCommand);
        }
        private void unsubscribeSignals()
        {
            m_SignalBus.TryUnsubscribe<GameStartedSignal>(onGameStarted);
            m_SignalBus.TryUnsubscribe<ExitShapeEditorCommandSignal>(onExitShapeEditorCommand);
        }

        private void onGameStarted()
        {
            m_SignalBus.TryFire<ShapeEditorEnteredSignal>();
            m_ShapeEditorCamera.Priority = 10;
            m_ShapeFollowerCamera.Priority = 1;
        }
        private void onExitShapeEditorCommand()
        {
            m_SignalBus.TryFire<ShapeEditorExitedSignal>();
            m_ShapeEditorCamera.Priority = 1;
            m_ShapeFollowerCamera.Priority = 10;
            m_BlobGroup.EnablePhysics();
        }

    }
}