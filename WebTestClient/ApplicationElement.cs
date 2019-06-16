namespace WebTestClient
{
    using System.Configuration;

    /// <summary>
    /// <c>ApplicationElement</c> is the <see cref="ConfigurationElement"/>
    /// used to configure a web service application.
    /// </summary>
    public class ApplicationElement : ConfigurationElement
    {
        /// <summary>The <see cref="ConfigurationProperty"/> for the name attribute.</summary>
        private static readonly ConfigurationProperty NameProperty =
            new ConfigurationProperty("name", typeof(string), null, null, null, ConfigurationPropertyOptions.IsKey | ConfigurationPropertyOptions.IsRequired);

        /// <summary>The <see cref="ConfigurationProperty"/> for the services default collection element.</summary>
        private static readonly ConfigurationProperty ServicesProperty =
            new ConfigurationProperty(null, typeof(ServiceElementCollection), null, null, null, ConfigurationPropertyOptions.IsDefaultCollection);

        /// <summary>The collection of configuration properties for the configuration element.</summary>
        private readonly ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

        /// <summary>Initializes a new instance of the <see cref="ApplicationElement"/> class.</summary>
        public ApplicationElement()
        {
            this.properties.Add(ApplicationElement.NameProperty);
            this.properties.Add(ApplicationElement.ServicesProperty);
        }

        /// <summary>Gets the name of the application.</summary>
        /// <value>The name of the application.</value>
        public string Name
        {
            get { return (string)base[ApplicationElement.NameProperty]; }
        }

        /// <summary>Gets the <see cref="ServiceElementCollection"/> for the web services.</summary>
        /// <value>The <see cref="ServiceElementCollection"/> for the web services.</value>
        public ServiceElementCollection Services
        {
            get { return (ServiceElementCollection)base[ApplicationElement.ServicesProperty]; }
        }

        /// <summary>Gets the collection of properties for the configuration element.</summary>
        /// <value>
        /// The <see cref="ConfigurationPropertyCollection"/>
        /// for the <see cref="ConfigurationElement"/>.
        /// </value>
        protected sealed override ConfigurationPropertyCollection Properties
        {
            get { return this.properties; }
        }
    }
}
