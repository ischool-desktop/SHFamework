using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Data;

namespace Framework
{
    /// <summary>
    /// ���Ѥ@�հ��Ĳv����k�إ�Xml���A�b��ƶq�j�ɡA�Ĳv���W�L�@��XmlDocument�ާ@�C
    /// </summary>
    public class XmlCreator
    {
        private XmlTextWriter _xmlWriter;
        private MemoryStream _baseStream;
        private byte[] _result;
        private bool _CompleteCreateXml = false;

        /// <summary>
        /// �إ�XmlGenerate����C
        /// </summary>
        public XmlCreator()
            : this(false)
        {
        }

        /// <summary>
        /// �إ�XmlGenerate����C
        /// </summary>
        /// <param name="doFormating">�M�w�O�_�n�榡��Xml���C</param>
        public XmlCreator(bool doFormating)
        {
            _baseStream = new MemoryStream();
            _xmlWriter = new XmlTextWriter(_baseStream, Encoding.UTF8);

            if (doFormating)
            {
                _xmlWriter.Indentation = 1;
                _xmlWriter.IndentChar = '\t';
                _xmlWriter.Formatting = Formatting.Indented;
            }
        }

        /// <summary>
        /// ���X���G�A����X���G��N����b�s�W��ơC
        /// </summary>
        /// <returns>�^��Xml���G�r��C</returns>
        public string GetAsString()
        {

            if (!IsCompleted)
                CompleteCreateXml();

            //�e���� 3 byte ���ǪF��...
            return Encoding.UTF8.GetString(_result, 3, _result.Length - 3);
        }

        /// <summary>
        /// ���X���G�A����X���G��N����b�s�W��ơC
        /// </summary>
        /// <returns>�^��Xml���G��y�C</returns>
        public Stream GetAsStream()
        {
            if (!IsCompleted)
                CompleteCreateXml();

            MemoryStream ms = new MemoryStream();
            ms.Write(_result, 0, _result.Length);
            ms.Flush();
            ms.Seek(0, SeekOrigin.Begin);

            return ms;
        }

        /// <summary>
        /// ���o���G�A�p�G�S���u�ڡv�����N�|���Ϳ��~�C
        /// ����X���G��N����b�s�W��ơC
        /// </summary>
        /// <returns>�^�ǵ��G�� XmlElement ����C</returns>
        public XmlElement GetAsXmlElement()
        {
            if (!IsCompleted)
                CompleteCreateXml();

            XmlDocument xmldoc = new XmlDocument();
            xmldoc.PreserveWhitespace = true;
            xmldoc.Load(GetAsStream());

            return xmldoc.DocumentElement;
        }

        /// <summary>
        /// ���o���G�A�p�G�S���u�ڡv�����N�|���Ϳ��~�C
        /// ����X���G��N����b�s�W��ơC
        /// </summary>
        /// <returns>�^�ǵ��G��XPathDocument����C</returns>
        public XPathDocument GetAsXPathDocument()
        {
            if (!IsCompleted)
                CompleteCreateXml();

            XPathDocument xpathdoc = new XPathDocument(GetAsStream());
            return xpathdoc;
        }

        public XmlHelper GetAsXmlHelper()
        {
            if (!IsCompleted)
                CompleteCreateXml();

            return new XmlHelper(GetAsXmlElement());
        }

        /// <summary>
        /// �N���G�x�s���ɮסC
        /// ����X���G��N����b�s�W��ơC
        /// </summary>
        /// <param name="fileName">�ɮצW��</param>
        public void SaveResult(string fileName)
        {
            if (!IsCompleted)
                CompleteCreateXml();

            FileStream sw = new FileStream(fileName, FileMode.Create);

            sw.Write(_result, 0, _result.Length);
            sw.Close();
        }

        /*Private Method*/

        /// <summary>
        /// ����Xml����X�ʧ@�C
        /// </summary>
        private void CompleteCreateXml()
        {
            _xmlWriter.Flush();

            _result = new byte[_baseStream.Length];
            _baseStream.Seek(0, SeekOrigin.Begin);
            _baseStream.Read(_result, 0, _result.Length);

            _baseStream.Close();
            _xmlWriter.Close();

            _CompleteCreateXml = true;
        }

        /// <summary>
        /// ���o�O�_�w�g�����إ�Xml���C
        /// </summary>
        private bool IsCompleted
        {
            get { return _CompleteCreateXml; }
        }

        #region XmlWriter����

        /// <summary>
        /// �إߤG�i���ƪ�Base64�s�X�C
        /// </summary>
        /// <param name="buffer">�n�g�J���G�i���ơC</param>
        /// <param name="index">�w�İϪ����ޡA�n�}�l�g�J�����ޡC</param>
        /// <param name="count">�n�g�J���줸�ռơC</param>
        public void CreateBase64(byte[] buffer, int index, int count)
        {
            _xmlWriter.WriteBase64(buffer, index, count);
        }

        /// <summary>
        /// �إߤ�r��Ʃ�CDataSection���C
        /// </summary>
        /// <param name="text">�n�[�JCDataSection����r��ơC</param>
        public void CreateCData(string text)
        {
            _xmlWriter.WriteCData(text);
        }

        /// <summary>
        /// �إ�Xml���ѡC
        /// </summary>
        /// <param name="text">���Ѥ�r</param>
        public void CreateComment(string text)
        {
            _xmlWriter.WriteComment(text);
        }

        /// <summary>
        /// �������e��CreateStartAttribute�I�s�C
        /// </summary>
        public void CreateEndAttribute()
        {
            _xmlWriter.WriteEndAttribute();
        }

        /// <summary>
        /// �������e��CreateStartElement�I�s�C
        /// </summary>
        public void CreateEndElement()
        {
            _xmlWriter.WriteEndElement();
        }

        /// <summary>
        /// �q���g�B�z��Xml�r��إ�Xml�C�ҡG&lt;Student&gt;���&lt;/Student&gt;�C
        /// </summary>
        /// <param name="data">�n�g�J��Xml�r��C</param>
        public void CreateRaw(string data)
        {
            _xmlWriter.WriteRaw(data);
        }

        /// <summary>
        /// �q���g�B�z��Xml�r���}�C�إ�Xml�C�ҡG&lt;Student&gt;���&lt;/Student&gt;�C
        /// </summary>
        /// <param name="buffer">�n�J�g���r���C�C</param>
        /// <param name="index">�}�l��m�C</param>
        /// <param name="count">Ū���ƶq�C</param>
        public void CreateRaw(char[] buffer, int index, int count)
        {
            _xmlWriter.WriteRaw(buffer, index, count);
        }

        /// <summary>
        /// �}�l�إ�Xml�ݩʡC
        /// </summary>
        /// <param name="localName">�ݩʦW�١C</param>
        public void CreateStartAttribute(string localName)
        {
            _xmlWriter.WriteStartAttribute(localName);
        }

        /// <summary>
        /// �إ߰_�l�����C�ҡG&lt;Student&gt;�C
        /// </summary>
        /// <param name="localName">�����W�١C</param>
        public void CreateStartElement(string localName)
        {
            _xmlWriter.WriteStartElement(localName);
        }

        /// <summary>
        /// �إߤ�r��ơC
        /// </summary>
        /// <param name="text">��r��ơC</param>
        public void CreateString(string text)
        {
            _xmlWriter.WriteString(text);
        }

        /// <summary>
        /// �إߧ��㪺Xml�����C
        /// </summary>
        /// <param name="localName">�����W�١C</param>
        /// <param name="value">��ƭȡC</param>
        public void CreateElement(string localName, string value)
        {
            _xmlWriter.WriteElementString(localName, value);
        }

        /// <summary>
        /// �إߧ��㪺�ݩʸ�ơC
        /// </summary>
        /// <param name="localName">�ݩʦW�١C</param>
        /// <param name="value">�ݩʭȡC</param>
        public void CreateAttribute(string localName, string value)
        {
            _xmlWriter.WriteAttributeString(localName, value);
        }

        /// <summary>
        /// �פJXmlElement������ƫإߤl������A��Ƥ��i�H�]�t�\�h�l�����C
        /// </summary>
        /// <param name="elm">�n�פJ����������C</param>
        public void CreateSubtree(XmlElement elm)
        {
            _xmlWriter.WriteNode(new XmlTextReader(new StringReader(elm.OuterXml)), true);
        }

        /// <summary>
        /// �̤����}�C�إ�Xml�C
        /// </summary>
        /// <param name="elements">�n�פJ�������}�C�C</param>
        public void CreateElements(XmlElement[] elements)
        {
            CreateSubtree(elements, null);
        }

        /// <summary>
        /// �פJ�����}�C�إߤl������C
        /// </summary>
        /// <param name="elements">�n�פJ�������}�C�C</param>
        /// <param name="rootName">�ڤ����W�١C</param>
        public void CreateSubtree(XmlElement[] elements, string rootName)
        {
            if (rootName != null) CreateStartElement(rootName);

            foreach (XmlElement elm in elements)
                CreateSubtree(elm);

            if (rootName != null) CreateEndElement();
        }

        /// <summary>
        /// �פJXmlNodeList������ƫإ�Xml�C
        /// </summary>
        /// <param name="nodes">�n�פJ��XmlNodeList����C</param>
        public void CreateElements(XmlNodeList nodes)
        {
            CreateSubtree(nodes, null);
        }

        /// <summary>
        /// �פJXmlNodeList����إߤl������C
        /// </summary>
        /// <param name="nodes">�n�פJ��XmlNodeList����</param>
        /// <param name="rootName">�ڤ����W��</param>
        public void CreateSubtree(XmlNodeList nodes, string rootName)
        {
            if (rootName != null) CreateStartElement(rootName);

            foreach (XmlNode node in nodes)
                CreateSubtree((XmlElement)node);

            if (rootName != null) CreateEndElement();
        }


        /// <summary>
        /// �פJDataTable����إߤl������C
        /// </summary>
        public void CreateSubtree(DataTable dataTable)
        {
            if (dataTable.TableName != "")
                CreateSubtree(dataTable, dataTable.TableName);
            else
                CreateSubtree(dataTable, "DataTable");
        }

        /// <summary>
        /// �פJDataTable����إߤl������C
        /// </summary>
        /// <param name="dataTable">�n�פJ��DataTable����C</param>
        /// <param name="rootName">�ڤ����W�١C</param>
        public void CreateSubtree(DataTable dataTable, string rootName)
        {
            CreateSubtree(dataTable, rootName, "RecordName");
        }

        /// <summary>
        /// �פJDataTable����إߤl������C
        /// </summary>
        /// <param name="dataTable">�n�פJ��DataTable����C</param>
        /// <param name="rootName">�ڤ����W�١C</param>
        /// <param name="recordName">�C�@���O�����ڦW�١C</param>
        public void CreateSubtree(DataTable dataTable, string rootName, string recordName)
        {
            CreateSubtree(dataTable, rootName, recordName, null);
        }

        /// <summary>
        /// �פJDataTable����إߤl������
        /// </summary>
        /// <param name="dataTable">�n�פJ��DataTable����</param>
        /// <param name="rootName">�ڤ����W��</param>
        /// <param name="recordName">�C�@���O�����ڦW��</param>
        /// <param name="fieldList">�n�[�J�����C��A�γr��(,)���j�C�ҡG�Ȥ�,�q��,����A�ǤJ Null �N�B�z�Ҧ����C</param>
        public void CreateSubtree(DataTable dataTable, string rootName, string recordName, string fieldList)
        {
            /* �M�w�O�_�B�z�Ҧ����A�٬O�ѨϥΪ̨M�w���������
             * �ϥ�fieldList�ѼƨM�w�A�p�G���ONull�N�ϥΨϥΪ̨M�w�����
             * �_���h�B�z�Ҧ����C
             * �b�B�z�ɡA�ϥ�DataColumn�}�C�x�s�n�B�z�����C*/
            DataColumn[] columnInfos = null;
            if (fieldList != null)
            {
                string[] strFieldList = fieldList.Split(',');
                columnInfos = new DataColumn[strFieldList.Length];
                for (int i = 0; i < columnInfos.Length; i++)
                    columnInfos[i] = dataTable.Columns[strFieldList[i]];
            }
            else
            {
                columnInfos = new DataColumn[dataTable.Columns.Count];
                for (int i = 0; i < dataTable.Columns.Count; i++)
                    columnInfos[i] = dataTable.Columns[i];
            }

            CreateStartElement(rootName);
            foreach (DataRow dr in dataTable.Rows)
            {
                CreateStartElement(recordName);

                //�̾�columnInfos�����M�w�n�B�z�������C
                foreach (DataColumn dc in columnInfos)
                {
                    object value = dr[dc.Ordinal];
                    if (value == null)
                        CreateElement(dc.ColumnName, "");
                    else
                    {
                        //�p�G��쪺��������ODateTime�N�N��B�z���@�w���榡�C
                        if (dc.DataType == typeof(DateTime))
                            if (value.ToString() == "")
                                CreateElement(dc.ColumnName, "");
                            else
                                CreateElement(dc.ColumnName, ((DateTime)value).ToString("yyyy/MM/dd hh:mm:ss"));
                        else
                            CreateElement(dc.ColumnName, value.ToString());
                    }
                }
                CreateEndElement();
            }
            CreateEndElement();
        }
        #endregion
    }
}
