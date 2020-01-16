using SearchEngine.Models.Database.Business;
using System.Collections.Generic;

namespace SearchEngine.Business.Responses
{
    
    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class yandexsearch : Response
    {
        private yandexsearchResponse responseField;

        /// <remarks/>
        public yandexsearchResponse response
        {
            get
            {
                return this.responseField;
            }
            set
            {
                this.responseField = value;
            }
        }

        public override List<SearchResult> ToSearchResults()
        {
            var searchResults = new List<SearchResult>();

            foreach(var group in response.results.grouping.group)
            {
                var searchResult = new SearchResult();
                searchResult.Description = group.doc.title;
                searchResult.Url = group.doc.url;

                searchResults.Add(searchResult);
            }

            return searchResults;
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class yandexsearchResponse
    {
        private yandexsearchResponseResults resultsField;

        public yandexsearchResponseResults results
        {
            get
            {
                return this.resultsField;
            }
            set
            {
                this.resultsField = value;
            }
        }
    }

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class yandexsearchResponseResults
    {

        private yandexsearchResponseResultsGrouping groupingField;

        /// <remarks/>
        public yandexsearchResponseResultsGrouping grouping
        {
            get
            {
                return this.groupingField;
            }
            set
            {
                this.groupingField = value;
            }
        }
    }


    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class yandexsearchResponseResultsGrouping
    {
        private yandexsearchResponseResultsGroupingGroup[] groupField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("group")]
        public yandexsearchResponseResultsGroupingGroup[] group
        {
            get
            {
                return this.groupField;
            }
            set
            {
                this.groupField = value;
            }
        }
    }


    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class yandexsearchResponseResultsGroupingGroup
    {
        private yandexsearchResponseResultsGroupingGroupDoc docField;

        public yandexsearchResponseResultsGroupingGroupDoc doc
        {
            get
            {
                return this.docField;
            }
            set
            {
                this.docField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class yandexsearchResponseResultsGroupingGroupDoc
    {
        private string urlField;
        private string titleField;


        /// <remarks/>
        public string url
        {
            get
            {
                return this.urlField;
            }
            set
            {
                this.urlField = value;
            }
        }

        /// <remarks/>
        public string title
        {
            get
            {
                return this.titleField;
            }
            set
            {
                this.titleField = value;
            }
        }
    }   
}
