/*
 * **************************************************************************
 * Developer : Pratap Singh, Manish (mpratap@Cyboriercompany.com)              *
 * Date : 23/10/2012                                                        *
 * Copyright  © 2012 Cyborier Sistemas, India Pvt Ltd. - All Rights Reserved   *
 * Unauthorized copying of this file, via any medium is strictly prohibited *
 * Proprietary and confidential.                                             *
 * **************************************************************************
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
namespace Cyborier.Shared.Serialization
{
    /// <summary>
    /// This class will be the base class for the all XMLSerializable Class
    /// </summary>
    public abstract class XMLSerializableObject
    {
        #region Variables
        protected string fileName;
        #endregion

        #region Constructor

        /// <summary>
        /// Create new instance of XML serializationObjerct
        /// </summary>
        /// <param name="FileName"></param>
        public XMLSerializableObject(string FileName)
            : this()
        {
            this.fileName = FileName;
        }

        public XMLSerializableObject()
        {
            // TODO: Complete member initialization
        }
        #endregion

        #region Serialize/Deserialize

        /// <summary>
        /// Serialzie insnance to the FileName
        /// </summary>
        /// <exception cref="Exception">thorws if any thing wrong happen</exception>
        public void Serialize()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(this.GetType());
                using (TextWriter textWriter = new StreamWriter(fileName))
                {
                    serializer.Serialize(textWriter, this);
                    textWriter.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        /// <summary>
        /// Deserialize from any file.
        /// </summary>
        /// <param name="objectType">type of object which want to deserialize</param>
        /// <param name="fileName">full path of the file</param>
        /// <returns>deserialized object</returns>
        public static object Deserialize(Type objectType, string fileName)
        {
            try
            {
                object deserializedObj;
                XmlSerializer deserializer = new XmlSerializer(objectType);
                using (TextReader reader = new StreamReader(fileName))
                {
                    deserializedObj = deserializer.Deserialize(reader);
                }

                return deserializedObj;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        #endregion
		[XmlIgnore]
        public string FileFullPath { get { return fileName; } set { fileName = value; } }

    }
}
