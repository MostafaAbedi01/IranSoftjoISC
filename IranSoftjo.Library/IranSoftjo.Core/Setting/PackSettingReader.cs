using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mehr.Setting
{
    public class PackSettingReader
    {
        public string SettingKeyPrefix { get; private set; }

        public PackSettingReader(string settingKeyPrefix = null)
        {
            this.SettingKeyPrefix = settingKeyPrefix;
        }

        public PackSettingReader(Type settingKeyTypePrefix)
            : this(settingKeyTypePrefix.FullName)
        { }

        public string Get(string settingKey, string defaultValue)
        {
            return Get(settingKey) ?? defaultValue;
        }

        public TValue Get<TValue>(string settingKey, TValue defaultValueOnNotFound = default(TValue), bool throwExceptionOnNotFound = false)
        {
            try
            {
                var valueString = Get(settingKey);
                if (valueString != null)
                {
                    if (typeof(TValue) == typeof(Guid))
                        return (TValue)Convert.ChangeType(Guid.Parse(valueString), typeof(Guid));
                    else if (typeof(TValue) == typeof(Type))
                        return (TValue)(object)Type.GetType(valueString, throwExceptionOnNotFound);
                    else if (typeof(TValue).IsEnum)
                        return (TValue)(object)Enum.Parse(typeof(TValue), valueString);
                    else if (typeof(TValue) == typeof(int[]))
                        return (TValue)(object)valueString.Split(',').Select(s => int.Parse(s)).ToArray();
                    else if (typeof(TValue) == typeof(TimeSpan))
                        return (TValue)(object)TimeSpan.Parse(valueString);
                    else
                        return (TValue)Convert.ChangeType(valueString, typeof(TValue));
                }
                else if (throwExceptionOnNotFound)
                    throw new InvalidOperationException("Setting key '" + settingKey + "' value not found!");
                else
                    return defaultValueOnNotFound;
            }
            catch (Exception)
            {
                if (throwExceptionOnNotFound)
                    throw;
                else
                    return defaultValueOnNotFound;
            }
        }

        private string Get(string settingKey)
        {
            var settingProvider = ServiceLocator.ResolveOnCurrentInstance<ISettingProvider, ConfigurationSettingProvider>();
            if (settingProvider != null)
                return settingProvider[SettingKeyPrefix, settingKey];

            return null;
        }

        public string GetFullKey(string settingKey)
        {
            if (!string.IsNullOrEmpty(SettingKeyPrefix))
                settingKey = SettingKeyPrefix + '.' + settingKey;
            return settingKey;
        }
    }
}
