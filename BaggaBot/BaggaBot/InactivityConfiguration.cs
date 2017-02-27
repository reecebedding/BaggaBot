using System.Configuration;
using System;

namespace BaggaBot
{
    public class InactivityConfiguration : ConfigurationSection
    {
        private static readonly ConfigurationProperty displayMessageAttribute =
             new ConfigurationProperty("DisplayMessage", typeof(ChannelCollection), null, ConfigurationPropertyOptions.IsRequired);

        private static readonly ConfigurationProperty intervalAttribute =
             new ConfigurationProperty("Interval", typeof(ChannelCollection), null, ConfigurationPropertyOptions.IsRequired);

        private static readonly ConfigurationProperty channelsElement =
             new ConfigurationProperty("Channels", typeof(ChannelCollection), null, ConfigurationPropertyOptions.IsRequired);

        public InactivityConfiguration()
        {
            base.Properties.Add(channelsElement);
            base.Properties.Add(displayMessageAttribute);
        }

        [ConfigurationProperty("Interval", IsRequired = true)]
        public UInt32 Interval
        {
            get { return (UInt32)this[intervalAttribute]; }
        }

        [ConfigurationProperty("DisplayMessage", IsRequired = true)]
        public string DisplayMessage
        {
            get { return (string)this[displayMessageAttribute]; }
        }

        [ConfigurationProperty("Channels", IsRequired = true)]
        public ChannelCollection Channels
        {
            get { return (ChannelCollection)this[channelsElement]; }
        }
    }

    [ConfigurationCollection(typeof(ChannelElement), AddItemName = "Channel",
     CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public class ChannelCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ChannelElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ChannelElement)element).Name;
        }

        new public ChannelElement this[string name]
        {
            get { return (ChannelElement)BaseGet(name); }
        }
    }

    public class ChannelElement : ConfigurationElement
    {

        private static readonly ConfigurationProperty channelName =
            new ConfigurationProperty("Name", typeof(string), string.Empty, ConfigurationPropertyOptions.IsRequired);

        // Holds the Value attribute value of Message.
        private static readonly ConfigurationProperty channelId =
            new ConfigurationProperty("Id", typeof(string), string.Empty, ConfigurationPropertyOptions.IsRequired);

        public ChannelElement()
        {
            base.Properties.Add(channelName);
            base.Properties.Add(channelId);
        }

        [ConfigurationProperty("Name", IsRequired = true)]
        public string Name
        {
            get { return (string)this[channelName]; }
        }

        [ConfigurationProperty("Id", IsRequired = true)]
        public ulong Id
        {
            get { return (UInt64)this[channelId]; }
        }
    }
}