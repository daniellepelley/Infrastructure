using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Newton.DA
{
    /// <summary>
    /// Interface responcible for serializing objects to and from Xml
    /// </summary>
    public interface IXmlSerializer<T>
    {
        #region Properties

        /// <summary>
        /// Collection of extra types that the serializer needs to be aware of
        /// </summary>
        List<Type> ExtraTypes { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Converts the passed object into Xml
        /// </summary>
        string SerializeObject(T objectToSerialize);

        /// <summary>
        /// Converts the passed Xml into an object of type T
        /// </summary>
        T DeSerializeObject(string filename);

        #endregion
    }
}
