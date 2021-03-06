﻿using SearchEngine.Models.Database.Business;
using System.Collections.Generic;

namespace SearchEngine.Business.Responses
{
    /// <summary>
    /// Presents response from google apis
    /// </summary>
    public class GoogleResponse : Response
    {
        public List<Item> Items { get; set; }

        /// <summary>
        /// Transforms Items to List<SearchResult>
        /// </summary>
        /// <returns></returns>
        public override List<SearchResult> ToSearchResults()
        {
            var searchResult = new List<SearchResult>();
            if (Items != null && Items.Count > 0)
            {
                foreach (var item in Items)
                {
                    searchResult.Add(new SearchResult
                    {
                        Description = item.Title,
                        Url = item.Link
                    });
                }
            }
            return searchResult;
        }
    }

    public class Item
    {
        public string Title { get; set; }
        public string Link { get; set; }
    }   
}
