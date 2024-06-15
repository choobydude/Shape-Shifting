using ShapeShifting;
using System;
using System.Linq;
using UnityEditor.iOS;
using Zenject;

public class ToolController : IInitializable, IDisposable
{
    [Inject]
    ToolControllerSettings m_Settings;
    [Inject]
    SignalBus m_SignalBus;

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
        m_SignalBus.Subscribe<SelectToolCommandSignal>(onToolSelectCommandSignal);
    }
    private void unsubscribeSignals()
    {
        m_SignalBus.TryUnsubscribe<SelectToolCommandSignal>(onToolSelectCommandSignal);
    }

    private void onToolSelectCommandSignal(SelectToolCommandSignal i_Signal)
    {
        if(getTool(i_Signal.ToolType, out ToolModel o_Tool))
            o_Tool.Select();
    }


    private bool getTool(eToolType i_ToolType, out ToolModel i_Tool)
    {
        i_Tool = m_Settings.Tools.FirstOrDefault(x => x.ToolData.ToolType == i_ToolType);
        return i_Tool;
    }

    public bool GetToolData(eToolType i_ToolType, out ToolData i_Data)
    {
        if(getTool(i_ToolType,out ToolModel o_Tool))
        {
            i_Data = o_Tool.ToolData;
            return true;
        }
        i_Data = default;
        return false;
    }
}
