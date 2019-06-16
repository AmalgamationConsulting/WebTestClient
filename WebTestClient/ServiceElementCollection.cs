namespace WebTestClient
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Configuration;

    /// <summary>
    /// <c>ServiceElementCollection</c> is the <see cref="ConfigurationElementCollection"/>
    /// for a collection of <see cref="ServiceElement"/> configuration elements.
    /// </summary>
    public class ServiceElementCollection : ConfigurationElementCollection, IEnumerable<ServiceElement>
    {
        /// <summary>The collection of properties for the configuration element collection.</summary>
        private readonly ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

        /// <summary>Initializes a new instance of the <see cref="ServiceElementCollection"/> class.</summary>
        public ServiceElementCollection()
        {
            this.AddElementName = "service";
        }

        /// <summary>Gets the collection of properties for the configuration element collection.</summary>
        /// <value>
        /// The <see cref="ConfigurationPropertyCollection"/>
        /// for the <see cref="ConfigurationElementCollection"/>.
        /// </value>
        protected sealed override ConfigurationPropertyCollection Properties
        {
            get { return this.properties; }
        }

        /// <summary>Gets the <see cref="ServiceElement"/> in the collection at the specified index.</summary>
        /// <param name="index">The index of the <see cref="ServiceElement"/>.</param>
        /// <returns>The <see cref="ServiceElement"/> in the collection at the specified index.</returns>
        public ServiceElement this[int index]
        {
            get { return (ServiceElement)BaseGet(index); }
        }

        /// <summary>Gets the <see cref="ServiceElement"/> in the collection with the specified name.</summary>
        /// <param name="name">The name of the <see cref="ServiceElement"/>.</param>
        /// <returns>The <see cref="ServiceElement"/> in the collection with the specified name.</returns>
        public new ServiceElement this[string name]
        {
            get { return (ServiceElement)BaseGet(name); }
        }

        /// <summary>Returns an enumerator that iterates through the collection of <see cref="ServiceElement"/> configuration elements.</summary>
        /// <returns>An enumerator that iterates through the collection of <see cref="ServiceElement"/> configuration elements.</returns>
        public new IEnumerator<ServiceElement> GetEnumerator()
        {
            IEnumerator enumerator = base.GetEnumerator();
            while (enumerator.MoveNext())
            {
                yield return (ServiceElement)enumerator.Current;
            }
        }

        /// <summary>Creates a new <see cref="ServiceElement"/>.</summary>
        /// <returns>A new <see cref="ServiceElement"/>.</returns>
        protected sealed override ConfigurationElement CreateNewElement()
        {
            return new ServiceElement();
        }

        /// <summary>Gets the key for <paramref name="element"/>.</summary>
        /// <param name="element">The <see cref="ConfigurationElement"/> for which the key is returned.</param>
        /// <returns>The key for <paramref name="element"/>.</returns>
        protected sealed override object GetElementKey(ConfigurationElement element)
        {
            return ((ServiceElement)element).Name;
        }
    }
}
