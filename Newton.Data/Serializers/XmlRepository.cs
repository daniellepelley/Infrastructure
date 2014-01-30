using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Newton.Data
{
    /// <summary>
    /// Class responcible for serializing objects to and from Xml
    /// </summary>
    public class XmlSerializer<T> : IXmlSerializer<T>
        where T : class
    {
        #region Properties

        private List<Type> extraTypes = new List<Type>();
        /// <summary>
        /// Collection of extra types that the serializer needs to be aware of
        /// </summary>
        public List<Type> ExtraTypes
        {
            get { return extraTypes; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Class responcible for serializing objects to and from Xml
        /// </summary>
        public XmlSerializer() { }

        /// <summary>
        /// Class responcible for serializing objects to and from Xml
        /// </summary>
        public XmlSerializer(IEnumerable<Type> extraTypes)
        {
            this.extraTypes.AddRange(extraTypes);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Converts the passed object into Xml
        /// </summary>
        public string SerializeObject(T objectToSerialize)
        {
            var output = new StringBuilder();
            using (var writer = XmlWriter.Create(output))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T), extraTypes.ToArray());
                serializer.Serialize(writer, objectToSerialize);
            }
            return output.ToString();
        }

        /// <summary>
        /// Converts the passed Xml into an object of type T
        /// </summary>
        public T DeSerializeObject(string input)
        {
            T objectToSerialize;
            using (var reader = new XmlTextReader(new StringReader(input)))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T), extraTypes.ToArray());
                objectToSerialize = (T)serializer.Deserialize(reader);
            }
            return objectToSerialize;
        }

        #endregion
    }
}
