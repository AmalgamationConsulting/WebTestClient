namespace WebTestClient
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Configuration;

    /// <summary>
    /// <c>ApplicationElementCollection</c> is the <see cref="ConfigurationElementCollection"/>
    /// for a collection of <see cref="ApplicationElement"/> configuration elements.
    /// </summary>
    public class ApplicationElementCollection : ConfigurationElementCollection, IEnumerable<ApplicationElement>
    {
        /// <summary>The collection of properties for the configuration element collection.</summary>
        private readonly ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

        /// <summary>Initializes a new instance of the <see cref="ApplicationElementCollection"/> class.</summary>
        public ApplicationElementCollection()
        {
            this.AddElementName = "application";
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

        /// <summary>Gets the <see cref="ApplicationElement"/> in the collection at the specified index.</summary>
        /// <param name="index">The index of the <see cref="ApplicationElement"/>.</param>
        /// <returns>The <see cref="ApplicationElement"/> in the collection at the specified index.</returns>
        public ApplicationElement this[int index]
        {
            get { return (ApplicationElement)BaseGet(index); }
        }

        /// <summary>Gets the <see cref="ApplicationElement"/> in the collection with the specified name.</summary>
        /// <param name="name">The name of the <see cref="ApplicationElement"/>.</param>
        /// <returns>The <see cref="ApplicationElement"/> in the collection with the specified name.</returns>
        public new ApplicationElement this[string name]
        {
            get { return (ApplicationElement)BaseGet(name); }
        }

        /// <summary>Returns an enumerator that iterates through the collection of <see cref="ApplicationElement"/> configuration elements.</summary>
        /// <returns>An enumerator that iterates through the collection of <see cref="ApplicationElement"/> configuration elements.</returns>
        public new IEnumerator<ApplicationElement> GetEnumerator()
        {
            IEnumerator enumerator = base.GetEnumerator();
            while (enumerator.MoveNext())
            {
                yield return (ApplicationElement)enumerator.Current;
            }
        }

        /// <summary>Creates a new <see cref="ApplicationElement"/>.</summary>
        /// <returns>A new <see cref="ApplicationElement"/>.</returns>
        protected sealed override ConfigurationElement CreateNewElement()
        {
            return new ApplicationElement();
        }

        /// <summary>Gets the key for <paramref name="element"/>.</summary>
        /// <param name="element">The <see cref="ConfigurationElement"/> for which the key is returned.</param>
        /// <returns>The key for <paramref name="element"/>.</returns>
        protected sealed override object GetElementKey(ConfigurationElement element)
        {
            return ((ApplicationElement)element).Name;
        }
    }
}
