﻿#region Header

// Copyright (c) ShiverCube 2010
//
// All rights reserved.
//
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are
// met:
//
//    * Redistributions of source code must retain the above copyright
//      notice, this list of conditions and the following disclaimer.
//    * Neither the name of the copyright holder nor the names of its
//      contributors may be used to endorse or promote products derived from
//      this software without specific prior written permission.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
// ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE
// LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
// CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
// SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
// INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
// CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
// ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF
// THE POSSIBILITY OF SUCH DAMAGE.

#endregion Header

namespace DynamicXml
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    /// <summary>
    /// Holds a collection of XML related items mapped by node name
    /// </summary>
    internal class XmlElement : XmlItem, IXElement
    {
        #region Fields

        private XElement _xml;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the XmlElement class with the provided XElement instance
        /// </summary>
        /// <param name="xml">The XElement object to initialize this instance with</param>
        public XmlElement(XElement xml)
        {
            _xml = xml;
        }

        #endregion Constructors

        #region Methods

        #region Protected Methods

        /// <summary>
        /// Gets the items this instance contains. This method is only ever called once when required
        /// </summary>
        /// <returns>The items this instance contains identified by node names</returns>
        protected override Dictionary<string, object> GetItems()
        {
            var items = new Dictionary<string, object>();

            foreach (var group in _xml.Elements().GroupBy(e => e.Name.LocalName))
            {
                if (group.Count() == 1)
                {
                    items[group.Key] = XmlBuilder.GenerateItem(group.First());
                }
                else
                {
                    items[group.Key] = new XmlArray(group.ToArray());
                }
            }

            _xml = null;
            return items;
        }

        #endregion Protected Methods

        #endregion Methods
    }
}