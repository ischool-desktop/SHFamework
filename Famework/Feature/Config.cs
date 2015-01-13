using System;
using System.Collections.Generic;
using System.Text;
using FISCA.DSAUtil;
using System.ComponentModel;
using System.Xml;

namespace Framework.Feature
{
    public class Config
    {
        //[AutoRetryOnWebException()]
        [AutoRetryOnWebException()]
        public static DSResponse GetNationalityList()
        {
            string serviceName = "Nationality";
            if (DataCacheManager.Get(serviceName) == null)
            {
                DSRequest request = new DSRequest();
                DSXmlHelper helper = new DSXmlHelper("GetNationalityListRequest");
                helper.AddElement("Fields");
                helper.AddElement("Fields", "All");
                request.SetContent(helper);
                DSResponse dsrsp = DSAServices.CallService("SmartSchool.Config.GetNationalityList", request);
                DataCacheManager.Add(serviceName, dsrsp);
            }
            return DataCacheManager.Get(serviceName);
        }

        public const string LIST_SCHOOL_LOCATION = "GetSchoolLocationList";
        //[AutoRetryOnWebException()]
        [AutoRetryOnWebException()]
        public static DSResponse GetSchoolLocationList()
        {
            if (DataCacheManager.Get(LIST_SCHOOL_LOCATION) == null)
            {
                DSXmlHelper helper = new DSXmlHelper("GetSchoolLocationListRequest");
                helper.AddElement("Field");
                helper.AddElement("Field", "All");
                DSResponse dsrsp = DSAServices.CallService("SmartSchool.Config.GetSchoolLocationList", new DSRequest(helper));
                DataCacheManager.Add(LIST_SCHOOL_LOCATION, dsrsp);
            }
            return DataCacheManager.Get(LIST_SCHOOL_LOCATION);
        }

        //[AutoRetryOnWebException()]
        [AutoRetryOnWebException()]
        public static List<string> GetCountyList()
        {
            string serviceName = "CountyTownBelong";
            if (DataCacheManager.Get(serviceName) == null)
            {
                DSRequest request = new DSRequest();
                DSXmlHelper helper = new DSXmlHelper("Request");
                helper.AddElement("Fields");
                helper.AddElement("Fields", "All");
                request.SetContent(helper);
                DSResponse dsrsp = DSAServices.CallService("SmartSchool.Config.GetCountyTownList", request);
                DataCacheManager.Add(serviceName, dsrsp);

                //BackgroundWorker bgw = new BackgroundWorker();
                //bgw.DoWork += new DoWorkEventHandler(bgw_DoWork);                
                //bgw.RunWorkerAsync(dsrsp.GetContent());
            }
            DSResponse rsp = DataCacheManager.Get(serviceName);
            List<string> countyList = new List<string>();
            foreach (XmlNode node in rsp.GetContent().GetElements("Town"))
            {
                string county = node.Attributes["County"].Value;
                if (!countyList.Contains(county))
                    countyList.Add(county);
            }
            return countyList;
        }

        //[AutoRetryOnWebException()]
        [AutoRetryOnWebException()]
        public static DSResponse GetUpdateCodeSynopsis()
        {
            string serviceName = "SmartSchool.Config.GetUpdateCodeSynopsis";
            if (DataCacheManager.Get(serviceName) == null)
            {
                DSRequest request = new DSRequest();
                DSXmlHelper helper = new DSXmlHelper("GetCountyListRequest");
                helper.AddElement("Field");
                helper.AddElement("Field", "���ʥN����Ӫ�");
                request.SetContent(helper);
                DSResponse dsrsp = DSAServices.CallService("SmartSchool.Config.GetUpdateCodeSynopsis", request);
                DataCacheManager.Add(serviceName, dsrsp);
            }
            return DataCacheManager.Get(serviceName);
        }

        //[AutoRetryOnWebException()]
        [AutoRetryOnWebException()]
        public static XmlElement[] GetTownList(string county)
        {
            DSResponse dsrsp = DataCacheManager.Get("CountyTownBelong");

            if (dsrsp == null)
                return new XmlElement[0];
            else
                return dsrsp.GetContent().GetElements("Town[@County='" + county + "']");
        }

        //[AutoRetryOnWebException()]
        [AutoRetryOnWebException()]
        public static DSResponse GetRelationship()
        {
            string serviceName = "Relationship";
            if (DataCacheManager.Get(serviceName) == null)
            {
                DSRequest request = new DSRequest();
                DSXmlHelper helper = new DSXmlHelper("GetRelationshipListRequest");
                helper.AddElement("Fields");
                helper.AddElement("Fields", "All");
                request.SetContent(helper);
                DSResponse dsrsp = DSAServices.CallService("SmartSchool.Config.GetRelationshipList", request);
                DataCacheManager.Add(serviceName, dsrsp);
            }
            return DataCacheManager.Get(serviceName);
        }

        //[AutoRetryOnWebException()]
        [AutoRetryOnWebException()]
        public static DSResponse GetJobList()
        {
            string serviceName = "JobList";
            if (DataCacheManager.Get(serviceName) == null)
            {
                DSRequest request = new DSRequest();
                DSXmlHelper helper = new DSXmlHelper("GetJobListRequest");
                helper.AddElement("Fields");
                helper.AddElement("Fields", "All");
                request.SetContent(helper);
                DSResponse dsrsp = DSAServices.CallService("SmartSchool.Config.GetJobList", request);
                DataCacheManager.Add(serviceName, dsrsp);
            }
            return DataCacheManager.Get(serviceName);
        }

        //[AutoRetryOnWebException()]
        [AutoRetryOnWebException()]
        public static DSResponse GetEduDegreeList()
        {
            string serviceName = "EduDegreeList";
            if (DataCacheManager.Get(serviceName) == null)
            {
                DSRequest request = new DSRequest();
                DSXmlHelper helper = new DSXmlHelper("GetEducationDegreeListRequest");
                helper.AddElement("Fields");
                helper.AddElement("Fields", "All");
                request.SetContent(helper);
                DSResponse dsrsp = DSAServices.CallService("SmartSchool.Config.GetEducationDegreeList", request);
                DataCacheManager.Add(serviceName, dsrsp);
            }
            return DataCacheManager.Get(serviceName);
        }

        //public static DSResponse SendSMS(string number, string message)
        //{
        //    DSRequest request = new DSRequest();
        //    DSXmlHelper helper = new DSXmlHelper("Request");
        //    helper.AddElement(".", "Message", message);
        //    helper.AddElement(".", "Mobile", number);
        //    request.SetContent(helper);
        //    return DSAServices.CallService("SmartSchool.Config.SendMessage", request);
        //}

        //[AutoRetryOnWebException()]
        [AutoRetryOnWebException()]
        public static KeyValuePair<string, string> FindTownByZipCode(string zipCode)
        {
            DSResponse dsrsp = DataCacheManager.Get("CountyTownBelong");
            XmlNode node = dsrsp.GetContent().GetElement("Town[@Code='" + zipCode + "']");
            KeyValuePair<string, string> kvp = new KeyValuePair<string, string>();
            if (node != null)
            {
                kvp = new KeyValuePair<string, string>(node.Attributes["County"].Value, node.Attributes["Name"].Value);
            }
            return kvp;
        }

        //[AutoRetryOnWebException()]
        [AutoRetryOnWebException()]
        public static DSResponse GetUpdateCodeList()
        {
            string serviceName = "UpdateCode";
            if (DataCacheManager.Get(serviceName) == null)
            {
                DSXmlHelper helper = new DSXmlHelper("GetUpdateCodeResponse");
                DataCacheManager.Add(serviceName, new DSResponse(helper));
            }
            return DataCacheManager.Get(serviceName);
        }

        //[AutoRetryOnWebException()]
        [AutoRetryOnWebException()]
        public static DSResponse GetUpdateTypeList(string p)
        {
            string serviceName = "UpdateType";
            if (DataCacheManager.Get(serviceName) == null)
            {
                DSXmlHelper helper = new DSXmlHelper("GetUpdateTypeResponse");
                DataCacheManager.Add(serviceName, new DSResponse(helper));
            }
            return DataCacheManager.Get(serviceName);
        }

        //[AutoRetryOnWebException()]
        [AutoRetryOnWebException()]
        public static DSResponse GetDepartment()
        {
            string serviceName = "GetDepartment";
            if (DataCacheManager.Get(serviceName) == null)
            {
                DSRequest request = new DSRequest();
                DSXmlHelper helper = new DSXmlHelper("GetDepartmentListRequest");
                helper.AddElement("Field");
                helper.AddElement("Field", "All");
                request.SetContent(helper);
                DSResponse dsrsp = DSAServices.CallService("SmartSchool.Config.GetDepartment", request);
                DataCacheManager.Add(serviceName, dsrsp);
            }
            return DataCacheManager.Get(serviceName);
        }

        public const string LIST_ABSENCE = "GetAbsenceList";
        public const string LIST_ABSENCE_NAME = "���O��Ӫ�";
        //[AutoRetryOnWebException()]
        [AutoRetryOnWebException()]
        public static DSResponse GetAbsenceList()
        {
            string serviceName = "GetAbsenceList";
            if (DataCacheManager.Get(serviceName) == null)
            {
                DSRequest request = new DSRequest();
                DSXmlHelper helper = new DSXmlHelper("GetAbsenceListRequest");
                helper.AddElement("Field");
                helper.AddElement("Field", "All");
                request.SetContent(helper);
                DSResponse dsrsp = DSAServices.CallService("SmartSchool.Others.GetAbsenceList", request);
                DataCacheManager.Add(serviceName, dsrsp);
            }
            return DataCacheManager.Get(serviceName);
        }

        public const string LIST_PERIODS = "GetPeriodList";
        public const string LIST_PERIODS_NAME = "�`����Ӫ�";
        //[AutoRetryOnWebException()]
        [AutoRetryOnWebException()]
        public static DSResponse GetPeriodList()
        {
            string serviceName = LIST_PERIODS;
            if (DataCacheManager.Get(serviceName) == null)
            {
                DSRequest request = new DSRequest();
                DSXmlHelper helper = new DSXmlHelper("GetPeriodListRequest");
                helper.AddElement("Field");
                helper.AddElement("Field", "All");
                request.SetContent(helper);
                DSResponse dsrsp = DSAServices.CallService("SmartSchool.Others.GetPeriodList", request);
                DataCacheManager.Add(serviceName, dsrsp);
            }
            return DataCacheManager.Get(serviceName);
        }

        //[AutoRetryOnWebException()]
        [AutoRetryOnWebException()]
        public static DSResponse GetDisciplineReasonList()
        {
            string serviceName = "SmartSchool.Config.GetDisciplineReasonList";

            // �����֨�
            //if (DataCacheManager.Get(serviceName) == null)
            //{

            DSRequest request = new DSRequest();
            DSXmlHelper helper = new DSXmlHelper("GetDisciplineReasonListRequest");
            helper.AddElement("Field");
            helper.AddElement("Field", "All");
            request.SetContent(helper);
            DSResponse dsrsp = DSAServices.CallService(serviceName, request);
            return dsrsp;

            //DataCacheManager.Add(serviceName, dsrsp);
            //}
            //return DataCacheManager.Get(serviceName);
        }

        //[AutoRetryOnWebException()]
        [AutoRetryOnWebException()]
        public static void SetDisciplineReasonList(XmlElement content)
        {
            DSXmlHelper helper = new DSXmlHelper("SetDisciplineReasonRequest");
            helper.AddElement(".", content);
            DSAServices.CallService("SmartSchool.Config.SetDisciplineReasonList", new DSRequest(helper));
        }

        //[AutoRetryOnWebException()]
        [AutoRetryOnWebException()]
        public static DSResponse GetScoreEntryList()
        {
            string serviceName = "GetScoreEntryList";
            if (DataCacheManager.Get(serviceName) == null)
            {
                DSRequest request = new DSRequest();
                DSXmlHelper helper = new DSXmlHelper("GetScoreEntryList");
                helper.AddElement("Field");
                helper.AddElement("Field", "All");
                request.SetContent(helper);
                DSResponse dsrsp = DSAServices.CallService("SmartSchool.Config.GetScoreEntryList", request);
                DataCacheManager.Add(serviceName, dsrsp);
            }
            return DataCacheManager.Get(serviceName);
        }

        //[AutoRetryOnWebException()]
        [AutoRetryOnWebException()]
        public static XmlElement GetSchoolInfo()
        {
            string serviceName = "SmartSchool.Config.GetSchoolInfo";
            DSXmlHelper helper = new DSXmlHelper("GetSchoolInfoRequest");
            helper.AddElement("All");
            DSResponse dsrsp = DSAServices.CallService(serviceName, new DSRequest(helper));
            return dsrsp.GetContent().BaseElement;
        }

        //[AutoRetryOnWebException()]
        [AutoRetryOnWebException()]
        public static void SetSchoolInfo(XmlElement schoolInfo)
        {
            string serviceName = "SmartSchool.Config.SetSchoolInfo";
            //CurrentUser user = CurrentUser.Instance;
            DSAServices.CallService(serviceName, new DSRequest(schoolInfo));
        }

        [AutoRetryOnWebException()]
        public static XmlElement GetSystemConfig()
        {
            string serviceName = "SmartSchool.Config.GetSystemConfig";
            DSXmlHelper helper = new DSXmlHelper("GetSystemConfigRequest");
            helper.AddElement("All");
            DSResponse dsrsp = DSAServices.CallService(serviceName, new DSRequest(helper));
            return dsrsp.GetContent().BaseElement;
        }

        [AutoRetryOnWebException()]
        public static void SetSystemConfig(XmlElement config)
        {
            string serviceName = "SmartSchool.Config.SetSystemConfig";
            //CurrentUser user = CurrentUser.Instance;
            DSAServices.CallService(serviceName, new DSRequest(config));
        }

        [AutoRetryOnWebException()]
        public static DSResponse GetMoralCommentCodeList()
        {
            DSXmlHelper helper = new DSXmlHelper("Request");
            helper.AddElement("Field");
            helper.AddElement("Field", "Content");
            return DSAServices.CallService("SmartSchool.Config.GetMoralCommentCodeList", new DSRequest(helper));
        }

        [AutoRetryOnWebException()]
        public static void SetMoralCommentCodeList(XmlElement content)
        {
            DSXmlHelper helper = new DSXmlHelper("SetMoralCommentCodeRequest");
            helper.AddElement(".", content);
            DSAServices.CallService("SmartSchool.Config.SetMoralCommentCodeList", new DSRequest(helper));
        }

        [AutoRetryOnWebException()]
        public static DSResponse GetWordCommentList()
        {
            DSXmlHelper helper = new DSXmlHelper("Request");
            helper.AddElement("Field");
            helper.AddElement("Field", "Content");
            return DSAServices.CallService("SmartSchool.Config.GetWordCommentList", new DSRequest(helper));
        }

        [AutoRetryOnWebException()]
        public static void SetWordCommentList(XmlElement content)
        {
            DSXmlHelper helper = new DSXmlHelper("SetWordCommentRequest");
            helper.AddElement(".", content);
            DSAServices.CallService("SmartSchool.Config.SetWordCommentList", new DSRequest(helper));
        }

        [AutoRetryOnWebException()]
        public static DSResponse GetSubjectChineseToEnglishList()
        {
            DSXmlHelper helper = new DSXmlHelper("Request");
            helper.AddElement("Field");
            helper.AddElement("Field", "Content");
            return DSAServices.CallService("SmartSchool.Config.GetSubjectChineseToEnglishList", new DSRequest(helper));
        }

        [AutoRetryOnWebException()]
        public static void SetSubjectChineseToEnglishList(XmlElement content)
        {
            DSXmlHelper helper = new DSXmlHelper("SetSubjectChineseToEnglishRequest");
            helper.AddElement(".", content);
            DSAServices.CallService("SmartSchool.Config.SetSubjectChineseToEnglishList", new DSRequest(helper));
        }

        [AutoRetryOnWebException()]
        public static void Reset(string serviceName)
        {
            DataCacheManager.Remove(serviceName);
        }

        [AutoRetryOnWebException()]
        public static void Update(DSRequest request)
        {
            DSAServices.CallService("SmartSchool.Config.UpdateList", request);
        }

        public const string LIST_DEGREE = "GetDegreeList";
        public const string LIST_DEGREE_NAME = "�w�ʵ��Ĺ�Ӫ�";
        [AutoRetryOnWebException()]
        public static DSResponse GetDegreeList()
        {
            string serviceName = LIST_DEGREE;
            if (DataCacheManager.Get(serviceName) == null)
            {
                DSRequest request = new DSRequest();
                DSXmlHelper helper = new DSXmlHelper(LIST_DEGREE);
                helper.AddElement("Field");
                helper.AddElement("Field", "All");
                request.SetContent(helper);
                DSResponse dsrsp = DSAServices.CallService("SmartSchool.Config.GetDegreeList", request);
                DataCacheManager.Add(serviceName, dsrsp);
            }
            return DataCacheManager.Get(serviceName);
        }

        [AutoRetryOnWebException()]
        public static DSResponse GetMDReduce()
        {
            return DSAServices.CallService("SmartSchool.Config.GetMDReduce", new DSRequest());
        }

        [AutoRetryOnWebException()]
        public static DSResponse GetMoralDiffItemList()
        {
            return DSAServices.CallService("SmartSchool.Config.GetMoralDiffItemList", new DSRequest());
        }

        [AutoRetryOnWebException()]
        public static void SetMoralDiffItemList(params string[] diffItems)
        {
            DSXmlHelper helper = new DSXmlHelper("Request");
            helper.AddElement("Content");
            foreach (string var in diffItems)
            {

                helper.AddElement("Content", "DiffItem").SetAttribute("Name", var);
            }
            DSAServices.CallService("SmartSchool.Config.SetMoralDiffItemList", new DSRequest(helper));
        }

        [AutoRetryOnWebException()]
        public static XmlElement GetMoralUploadConfig()
        {
            return DSAServices.CallService("SmartSchool.Config.GetMoralUploadConfig", new DSRequest()).GetContent().BaseElement;
        }

        [AutoRetryOnWebException()]
        public static void SetMoralUploadConfig(XmlElement config)
        {
            DSAServices.CallService("SmartSchool.Config.SetMoralUploadConfig", new DSRequest(config));
        }

        [AutoRetryOnWebException()]
        public static decimal GetSupervisedRatingRange()
        {
            DSResponse rsp = DSAServices.CallService("SmartSchool.Config.GetSupervisedRatingRange", new DSRequest());
            XmlElement range = rsp.GetContent().GetElement("RatingRange");

            if (range == null || range.InnerText == string.Empty)
                return 32767;
            else
                return decimal.Parse(range.InnerText);
        }

        [AutoRetryOnWebException()]
        public static DSXmlHelper GetSchoolConfig()
        {
            DSResponse dsrsp = DSAServices.CallService("SmartSchool.Config.GetSchoolConfig", new DSRequest());
            return dsrsp.GetContent();
        }

        [AutoRetryOnWebException()]
        public static void SetSchoolConfig(XmlElement config)
        {
            DSXmlHelper helper = new DSXmlHelper("Request");
            helper.AddElement(".", config);
            DSAServices.CallService("SmartSchool.Config.SetSchoolConfig", new DSRequest(helper));
        }

        //public static void SetModulePreference(DSXmlHelper request)
        //{
        //    FeatureBase.CallService("SmartSchool.Config.SetModulePreference", new DSRequest(request));
        //}

        //public static DSXmlHelper GetModulePreference()
        //{
        //    return FeatureBase.CallService("SmartSchool.Config.GetModulePreference", new DSRequest()).GetContent();
        //}
    }

    public static class DataCacheManager
    {
        private static Dictionary<string, DSResponse> _dataManager = new Dictionary<string, DSResponse>();

        public static void Add(string name, DSResponse dsrsp)
        {
            lock (_dataManager)
            {
                if (!_dataManager.ContainsKey(name))
                    _dataManager.Add(name, dsrsp);
                else
                    _dataManager[name] = dsrsp;
            }
        }

        public static DSResponse Get(string name)
        {
            lock (_dataManager)
            {
                if (_dataManager.ContainsKey(name))
                    return _dataManager[name];
                return null;
            }
        }

        public static bool Contains(string serviceName)
        {
            return _dataManager.ContainsKey(serviceName);
        }

        public static void Remove(string serviceName)
        {
            if (_dataManager.ContainsKey(serviceName))
            {
                _dataManager.Remove(serviceName);
            }
        }

        public static void Set(string name, DSResponse dsrsp)
        {
            _dataManager[name] = dsrsp;
        }
    }
}
