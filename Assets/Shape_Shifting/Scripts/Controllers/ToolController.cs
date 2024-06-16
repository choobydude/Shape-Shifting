using System;
using System.Linq;
using UnityEngine;
using Zenject;

namespace ShapeShifting
{
    public class ToolController : IInitializable, IDisposable, ITickable
    {
        [Inject]
        ToolControllerSettings m_Settings;
        [Inject]
        SignalBus m_SignalBus;

        ToolModel m_SelectedTool;

        public void Initialize()
        {
            subscribeSignals();
        }
        public void Tick()
        {
            m_SelectedTool?.Update();
        }

        public void Dispose()
        {
            unsubscribeSignals();
        }

        private void subscribeSignals()
        {
            m_SignalBus.Subscribe<ShapeEditorEnteredSignal>(onEditingStarted);
            m_SignalBus.Subscribe<ShapeEditorExitedSignal>(onEditingEnded);

            m_SignalBus.Subscribe<SelectToolCommandSignal>(onToolSelectCommandSignal);
            m_SignalBus.Subscribe<ToolSelectedSignal>(onToolSelected);
        }
        private void unsubscribeSignals()
        {
            m_SignalBus.TryUnsubscribe<ShapeEditorEnteredSignal>(onEditingStarted);
            m_SignalBus.TryUnsubscribe<ShapeEditorExitedSignal>(onEditingEnded);

            m_SignalBus.TryUnsubscribe<SelectToolCommandSignal>(onToolSelectCommandSignal);
            m_SignalBus.TryUnsubscribe<ToolSelectedSignal>(onToolSelected);
        }

        private void onEditingStarted()
        {
            m_SelectedTool = getSelectedTool();
            if (!m_SelectedTool)
                trySelectTool(eToolType.Paint);
        }
        private void onEditingEnded()
        {
            if (m_SelectedTool)
                m_SelectedTool.Deselect();
            m_SelectedTool = null;
        }

        private void onToolSelectCommandSignal(SelectToolCommandSignal i_Signal)
        {
            trySelectTool(i_Signal.ToolType);
        }
        private void onToolSelected(ToolSelectedSignal i_Signal)
        {
            getTool(i_Signal.ToolType, out m_SelectedTool);
        }

        private void trySelectTool(eToolType i_ToolType)
        {
            if (getTool(i_ToolType, out ToolModel o_Tool))
            {
                if (o_Tool.ToolData.IsSelected)
                    return;

                deselectSelectedTool();
                o_Tool.Select();
            }
        }
        private void deselectSelectedTool()
        {
            m_Settings.Tools.FirstOrDefault(model => model.ToolData.IsSelected)?.Deselect();
        }

        private bool getTool(eToolType i_ToolType, out ToolModel i_Tool)
        {
            i_Tool = m_Settings.Tools.FirstOrDefault(x => x.ToolData.ToolType == i_ToolType);
            return i_Tool;
        }

        private ToolModel getSelectedTool()
        {
            return m_Settings.Tools.FirstOrDefault(tool => tool.ToolData.IsSelected);
        }

        public bool GetToolData(eToolType i_ToolType, out ToolData i_Data)
        {
            if (getTool(i_ToolType, out ToolModel o_Tool))
            {
                i_Data = o_Tool.ToolData;
                return true;
            }
            i_Data = default;
            return false;
        }


    }
}

