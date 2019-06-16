namespace WebTestClient
{
    using System.Configuration;

    /// <summary>
    /// <c>ServiceSection</c> is the <see cref="ConfigurationSection"/>
    /// used to configure one or more web service applications.
    /// </summary>
    public class ServiceSection : ConfigurationSection
    {
        /// <summary>The <see cref="ConfigurationProperty"/> for the xmlns attribute.</summary>
        private static readonly ConfigurationProperty XmlNamespaceProperty =
            new ConfigurationProperty("xmlns", typeof(string), null, null, null, ConfigurationPropertyOptions.None);

        /// <summary>The <see cref="ConfigurationProperty"/> for the applications default collection element.</summary>
        private static readonly ConfigurationProperty ApplicationsProperty =
            new ConfigurationProperty(null, typeof(ApplicationElementCollection), null, null, null, ConfigurationPropertyOptions.IsDefaultCollection);

        /// <summary>The collection of properties for the configuration section.</summary>
        private readonly ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

        /// <summary>Initializes a new instance of the <see cref="ServiceSection"/> class.</summary>
        public ServiceSection()
        {
            this.properties.Add(ServiceSection.XmlNamespaceProperty);
            this.properties.Add(ServiceSection.ApplicationsProperty);
        }

        /// <summary>Gets the <see cref="ApplicationElementCollection"/> for the web service applications.</summary>
        /// <value>The <see cref="ApplicationElementCollection"/> for the web service applications.</value>
        public ApplicationElementCollection Applications
        {
            get { return (ApplicationElementCollection)base[ServiceSection.ApplicationsProperty]; }
        }

        /// <summary>Gets the collection of properties for the configuration section.</summary>
        /// <value>
        /// The <see cref="ConfigurationPropertyCollection"/>
        /// for the <see cref="ConfigurationSection"/>.
        /// </value>
        protected sealed override ConfigurationPropertyCollection Properties
        {
            get { return this.properties; }
        }
    }
}
