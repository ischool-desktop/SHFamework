﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using Framework.Feature;
using FISCA.Presentation;

namespace Framework.Legacy
{
    public class PreferenceProvider : IPreferenceProvider
    {
        private bool _PreferenceChanged = false;
        private XmlElement BackupElement;

        public PreferenceProvider()
        {
            RootElement = Personal.GetPreference().GetContent().BaseElement;
            BackupElement = (XmlElement)new XmlDocument().ImportNode(RootElement, true);
            Application.Idle += delegate
            {
                if ( _PreferenceChanged )
                {
                    this.SaveToServer();
                    _PreferenceChanged = false;
                }
            };
        }

        private XmlElement RootElement;
        public XmlElement this[string Key]
        {
            get
            {
                XmlElement element = (XmlElement)RootElement.SelectSingleNode(Key);
                if (element == null)
                {
                    return null;
                }
                else
                    return (XmlElement)element;
            }
            set
            {
                if (value.Name != Key)
                    throw new Exception("ElementName與Key不相同");
                XmlElement element = (XmlElement)RootElement.SelectSingleNode(Key);
                if ( element != null )
                {
                    XmlElement element2 = (XmlElement)BackupElement.SelectSingleNode(Key);
                    if ( element2 == null || ( element2 .OuterXml!=value.OuterXml) )
                    {
                        XmlElement e = value;
                        if ( element.OwnerDocument != value.OwnerDocument )
                            e = (XmlElement)element.OwnerDocument.ImportNode(value, true);
                        RootElement.ReplaceChild(e, element);
                        if ( element2 != null )
                        {
                            e = (XmlElement)BackupElement.OwnerDocument.ImportNode(value, true);
                            BackupElement.ReplaceChild(e, element2);
                        }
                        else
                        {
                            BackupElement.AppendChild(BackupElement.OwnerDocument.ImportNode(value, true));
                        }
                        _PreferenceChanged = true;
                    }
                }
                else
                {
                    RootElement.AppendChild(RootElement.OwnerDocument.ImportNode(value, true));
                    BackupElement.AppendChild(BackupElement.OwnerDocument.ImportNode(value, true));
                    _PreferenceChanged = true;
                }
            }
        }

        public void SaveToServer()
        {
            Personal.SetPreference(RootElement);
        }
    }
}
