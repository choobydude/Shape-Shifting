using System;
using System.Linq;
using UnityEngine;
using Zenject;

namespace WhackAMole
{
    public class ResourceController
    {
        #region Fields

        [Inject]
        ResourceControllerSettings m_Settings;
        #endregion


        #region Interaction Methods

        private bool getResource(eResourceType i_ResourceType, out ResourceModel i_Resource)
        {
            i_Resource = m_Settings.Resources.FirstOrDefault(x => x.Type == i_ResourceType);
            return i_Resource;
        }
        public bool GetResourceIconAndValue(eResourceType i_ResourceType, out Sprite o_Icon, out int o_Value)
        {
            if(getResource(i_ResourceType, out ResourceModel o_Resource))
            {
                o_Icon = o_Resource.Icon;
                o_Value = o_Resource.Value;
                return true;
            }
            o_Icon = default;
            o_Value = default;
            return false;
        }
        public bool GetResourceValue(eResourceType i_ResourceType, out int o_Value)
        {
            if (getResource(i_ResourceType, out ResourceModel o_Resource))
            {
                o_Value = o_Resource.Value;
                return true;
            }
            o_Value = default;
            return false;
        }
        public bool HasEnoughResources(eResourceType i_ResourceType, int i_Amount)
        {
            if(getResource(i_ResourceType, out ResourceModel o_Resource))
                return o_Resource.HasAmount(i_Amount);
            return false;
        }
        public void AddResource(eResourceType i_ResourceType, int i_Amount)
        {
            if (getResource(i_ResourceType, out ResourceModel o_Resource))
                o_Resource.Add(i_Amount);
        }
        public void SpendResource(eResourceType i_ResourceType, int i_Amount)
        {
            if (getResource(i_ResourceType, out ResourceModel o_Resource))
                o_Resource.Remove(i_Amount);
        }
        #endregion
    }
}
