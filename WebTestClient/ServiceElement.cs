namespace WebTestClient
{
    using System;
    using System.Configuration;

    /// <summary>
    /// <c>ServiceElement</c> is the <see cref="ConfigurationElement"/>
    /// used to configure a web service.
    /// </summary>
    public class ServiceElement : ConfigurationElement
    {
        /// <summary>The <see cref="ConfigurationProperty"/> for the address attribute.</summary>
        private static readonly ConfigurationProperty AddressProperty =
            new ConfigurationProperty("address", typeof(Uri), null, null, null, ConfigurationPropertyOptions.IsRequired);

        /// <summary>The <see cref="ConfigurationProperty"/> for the contentType attribute.</summary>
        private static readonly ConfigurationProperty ContentTypeProperty =
            new ConfigurationProperty("contentType", typeof(string), null, null, null, ConfigurationPropertyOptions.None);

        /// <summary>The <see cref="ConfigurationProperty"/> for the entityBody attribute.</summary>
        private static readonly ConfigurationProperty EntityBodyProperty =
            new ConfigurationProperty("entityBody", typeof(string), null, null, null, ConfigurationPropertyOptions.None);

        /// <summary>The <see cref="ConfigurationProperty"/> for the method attribute.</summary>
        private static readonly ConfigurationProperty MethodProperty =
            new ConfigurationProperty("method", typeof(string), null, null, null, ConfigurationPropertyOptions.IsRequired);

        /// <summary>The <see cref="ConfigurationProperty"/> for the name attribute.</summary>
        private static readonly ConfigurationProperty NameProperty =
            new ConfigurationProperty("name", typeof(string), null, null, null, ConfigurationPropertyOptions.IsKey | ConfigurationPropertyOptions.IsRequired);

        /// <summary>The collection of configuration properties for the configuration element.</summary>
        private readonly ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

        /// <summary>Initializes a new instance of the <see cref="ServiceElement"/> class.</summary>
        public ServiceElement()
        {
            this.properties.Add(ServiceElement.AddressProperty);
            this.properties.Add(ServiceElement.ContentTypeProperty);
            this.properties.Add(ServiceElement.EntityBodyProperty);
            this.properties.Add(ServiceElement.MethodProperty);
            this.properties.Add(ServiceElement.NameProperty);
        }

        /// <summary>Gets the address of the service.</summary>
        /// <value>The address of the service.</value>
        public Uri Address
        {
            get { return (Uri)base[ServiceElement.AddressProperty]; }
        }

        /// <summary>Gets the content type of the entity body for the HTTP request.</summary>
        /// <remarks>The content type is only used if the HTTP method is <c>POST</c>.</remarks>
        /// <value>The content type of the entity body for the HTTP request.</value>
        public string ContentType
        {
            get { return (string)base[ServiceElement.ContentTypeProperty]; }
        }

        /// <summary>Gets the entity body for the HTTP request.</summary>
        /// <remarks>The entity body is only used if the HTTP method is <c>POST</c>.</remarks>
        /// <value>The entity body for the HTTP request.</value>
        public string EntityBody
        {
            get { return (string)base[ServiceElement.EntityBodyProperty]; }
        }

        /// <summary>Gets the HTTP method for the HTTP request.</summary>
        /// <value>The HTTP method for the HTTP request.</value>
        public string Method
        {
            get { return (string)base[ServiceElement.MethodProperty]; }
        }

        /// <summary>Gets the name of the service.</summary>
        /// <value>The name of the service.</value>
        public string Name
        {
            get { return (string)base[ServiceElement.NameProperty]; }
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

        /// <summary>Called after deserialization. Validates the configuration settings.</summary>
        protected sealed override void PostDeserialize()
        {
            base.PostDeserialize();
            switch (this.Method)
            {
                case "GET":
                    if (!string.IsNullOrEmpty(this.ContentType))
                    {
                        throw new ConfigurationErrorsException("contentType must not be specified when method is GET");
                    }

                    if (!string.IsNullOrEmpty(this.EntityBody))
                    {
                        throw new ConfigurationErrorsException("entityBody must not be specified when method is GET");
                    }

                    break;

                case "POST":
                    if (string.IsNullOrEmpty(this.ContentType))
                    {
                        throw new ConfigurationErrorsException("contentType must be specified when method is POST");
                    }

                    if (string.IsNullOrEmpty(this.EntityBody))
                    {
                        throw new ConfigurationErrorsException("entityBody must be specified when method is POST");
                    }

                    break;

                default:
                    throw new ConfigurationErrorsException("method must be GET or POST");
            }
        }
    }
}
